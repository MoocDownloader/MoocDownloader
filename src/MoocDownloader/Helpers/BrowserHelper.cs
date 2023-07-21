using MoocDownloader.Models.Credentials;
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

namespace MoocDownloader.Helpers;

public class BrowserHelper
{
    private const string CookiesPath = @"User Data\Default\Network\Cookies";
    private const string LocalStatePath = @"User Data\Local State";

    private const string EdgePath = @"AppData\Local\Microsoft\Edge";
    private const string ChromePath = @"AppData\Local\Google\Chrome";

    public static string UserProfilePath => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    public static byte[] GetDecryptedKey(string localState)
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

    public static string DecryptCookieValue(byte[] encryptedValue, byte[] decryptedKey)
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

    public static void DecryptCookies(List<BrowserCookie> cookies, byte[] decryptedKey)
    {
        foreach (var cookie in cookies)
        {
            if (cookie.EncryptedValue == null) continue;

            cookie.Value = DecryptCookieValue(cookie.EncryptedValue, decryptedKey);
        }
    }

    public static List<BrowserCookie> ReadBrowserCookies(string cookiesPath, string[] domains)
    {
        using var connection = new SQLiteConnection(cookiesPath, SQLiteOpenFlags.ReadOnly);

        var browserCookies = new List<BrowserCookie>();
        var cookieQuery = connection.Table<BrowserCookie>();

        foreach (var domain in domains)
        {
            var list = cookieQuery.Where(cookie => cookie.Host != null && cookie.Host.Contains(domain)).ToList();
            browserCookies.AddRange(list);
        }

        return browserCookies;
    }

    public static List<BrowserCookie> ImportCookiesFromEdge(string[] domains)
    {
        // Path of Edge browser.
        var edgeCookiesPath = Path.Combine(UserProfilePath, EdgePath, CookiesPath);
        var edgeLocalStatePath = Path.Combine(UserProfilePath, EdgePath, LocalStatePath);

        if (!File.Exists(edgeCookiesPath) || !File.Exists(edgeLocalStatePath))
        {
            throw new FileNotFoundException();
        }

        var decryptedKey = GetDecryptedKey(edgeLocalStatePath);
        var browserCookies = ReadBrowserCookies(edgeCookiesPath, domains);

        DecryptCookies(browserCookies, decryptedKey);

        return browserCookies;
    }

    public static List<BrowserCookie> ImportCookiesFromChrome(string[] domains)
    {
        // Path of Chrome browser.
        var chromeCookiesPath = Path.Combine(UserProfilePath, ChromePath, CookiesPath);
        var chromeLocalStatePath = Path.Combine(UserProfilePath, ChromePath, LocalStatePath);

        if (!File.Exists(chromeCookiesPath) || !File.Exists(chromeLocalStatePath))
        {
            throw new FileNotFoundException();
        }

        var decryptedKey = GetDecryptedKey(chromeLocalStatePath);
        var browserCookies = ReadBrowserCookies(chromeCookiesPath, domains);

        DecryptCookies(browserCookies, decryptedKey);

        return browserCookies;
    }
}