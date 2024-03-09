using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace MoocDownloader.Utilities.Browsers;

public class ChromiumUtility
{
    private const string CookiesPath = @"User Data\{{PROFILE}}\Network\Cookies";
    private const string LocalStatePath = @"User Data\Local State";

    private const string EdgePath = @"Microsoft\Edge";
    private const string ChromePath = @"Google\Chrome";

    private const string DefaultProfileName = "Default";

    private static string LocalDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    private static byte[] GetDecryptedKey(string localState)
    {
        var localStateData = File.ReadAllText(localState);
        var localStateValue = JsonNode.Parse(localStateData);
        var encryptedKey = localStateValue?["os_crypt"]?["encrypted_key"]?.GetValue<string>();

        if (string.IsNullOrEmpty(encryptedKey))
        {
            return Array.Empty<byte>();
        }

        var encryptedValue = Convert.FromBase64String(encryptedKey).Skip(5).ToArray();

        return ProtectedData.Unprotect(encryptedValue, null, DataProtectionScope.CurrentUser);
    }

    private static string DecryptCookieValue(byte[] encryptedValue, byte[] decryptedKey)
    {
        // 03 - 15 -> nonce
        // 15 - .. -> cipher
        var nonce = encryptedValue[3..15];
        var cipher = encryptedValue[15..];

        var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
        var aeaParameters = new AeadParameters(new KeyParameter(decryptedKey), 128, nonce, null);

        gcmBlockCipher.Init(false, aeaParameters);

        var plainValue = new byte[gcmBlockCipher.GetOutputSize(cipher.Length)];
        var plainLength = gcmBlockCipher.ProcessBytes(cipher, 0, cipher.Length, plainValue, 0);

        gcmBlockCipher.DoFinal(plainValue, plainLength);

        return Encoding.UTF8.GetString(plainValue);
    }

    private static void DecryptCookies(List<ChromiumCookie> cookies, byte[] decryptedKey)
    {
        foreach (var cookie in cookies)
        {
            if (cookie.EncryptedValue == null) continue;

            cookie.Value = DecryptCookieValue(cookie.EncryptedValue, decryptedKey);
            cookie.Expires = CookieTimeStampToUnixTime(cookie.Expires);
        }
    }

    private static List<ChromiumCookie> GetCookies(string cookiesPath, string[] domains)
    {
        using var connection = new SQLiteConnection(cookiesPath, SQLiteOpenFlags.ReadOnly);

        var chromeCookies = new List<ChromiumCookie>();
        var cookieQuery = connection.Table<ChromiumCookie>();

        foreach (var domain in domains)
        {
            var list = cookieQuery.Where(cookie => cookie.Domain != null && cookie.Domain.Contains(domain)).ToList();
            chromeCookies.AddRange(list);
        }

        return chromeCookies;
    }

    /// <summary>
    /// Chrome's cookies time stamp's epoch starts 1601-01-01T00:00:00Z (WHY???)
    /// So, it's 11644473600 seconds before the UNIX epoch
    /// </summary>
    /// <param name="cookieTimeStamp">Chrome's cookies time stamp.</param>
    /// <returns>UNIX time stamp.</returns>
    private static long CookieTimeStampToUnixTime(long cookieTimeStamp)
    {
        if (cookieTimeStamp <= 0)
            return 0;

        return (cookieTimeStamp / 1000000) - 11644473600;
    }

    /// <summary>
    /// Get the profile list of user in the browser.
    /// </summary>
    /// <returns>The profile list.</returns>
    private static List<string> GetProfilePathList()
    {
        var list = new List<string>
        {
            CookiesPath.Replace("{{PROFILE}}", DefaultProfileName)
        };

        for (var i = 1; i < 10; i++)
        {
            list.Add(CookiesPath.Replace("{{PROFILE}}", $"Profile {i}"));
        }

        return list;
    }

    public static List<ChromiumCookie> ImportCookiesFromEdge(string[] domains)
    {
        // Path of Edge browser.
        var edgeCookiesPath = GetProfilePathList()
            .Select(cookiesPath => Path.Combine(LocalDataPath, EdgePath, cookiesPath))
            .FirstOrDefault(File.Exists);

        if (string.IsNullOrEmpty(edgeCookiesPath))
        {
            throw new FileNotFoundException();
        }

        var edgeLocalStatePath = Path.Combine(LocalDataPath, EdgePath, LocalStatePath);

        if (!File.Exists(edgeLocalStatePath))
        {
            throw new FileNotFoundException();
        }

        var decryptedKey = GetDecryptedKey(edgeLocalStatePath);
        var browserCookies = GetCookies(edgeCookiesPath, domains);

        DecryptCookies(browserCookies, decryptedKey);

        return browserCookies;
    }

    public static List<ChromiumCookie> ImportCookiesFromChrome(string[] domains)
    {
        // Path of Chrome browser.
        var chromeCookiesPath = GetProfilePathList()
            .Select(cookiesPath => Path.Combine(LocalDataPath, ChromePath, cookiesPath))
            .FirstOrDefault(File.Exists);

        if (string.IsNullOrEmpty(chromeCookiesPath))
        {
            throw new FileNotFoundException();
        }

        var chromeLocalStatePath = Path.Combine(LocalDataPath, ChromePath, LocalStatePath);

        if (!File.Exists(chromeLocalStatePath))
        {
            throw new FileNotFoundException();
        }

        var decryptedKey = GetDecryptedKey(chromeLocalStatePath);
        var browserCookies = GetCookies(chromeCookiesPath, domains);

        DecryptCookies(browserCookies, decryptedKey);

        return browserCookies;
    }
}