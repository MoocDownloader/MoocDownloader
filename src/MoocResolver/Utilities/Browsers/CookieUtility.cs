using System.Net;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace MoocResolver.Utilities.Browsers;

public class CookieUtility
{
    /// <summary>
    /// Parse cookie text into <see cref="CookieContainer"/>
    ///
    /// Text cookie text has two formats: JSON and Netscape.
    /// </summary>
    /// <param name="cookieData">The cookie text.</param>
    /// <returns><see cref="CookieContainer"/> parsed from cookie text.</returns>
    public static CookieContainer ParseCookies(string cookieData)
    {
        if (string.IsNullOrEmpty(cookieData))
        {
            throw new ArgumentNullException(nameof(cookieData));
        }

        // Detect the format of cookie text:
        //
        //     The cookie text is detected to be Netscape schema
        //     if each line of text except those starting with #
        //     matches Netscape pattern.
        //
        // File format:
        //
        // Officially, the first line of the file must be one of the following:
        // 
        //     # HTTP Cookie File
        //     # Netscape HTTP Cookie File 
        // 
        // Fields are separated by tab characters (\t or \009 or 0x09).
        // 
        //     Lines are separated by the newline format in use by the running operating system.
        //     That means CRLF (\r\n) for Windows and LF (\n) for Unix-like systems such as Linux, macOS, FreeBSD, etc. 
        //
        // The 7 fields are as follows:
        //
        //     +------------+----------------+---------------+-----------------------------------------------------+
        //     | Field Name | Type           | Example Value | Notes                                               |
        //     +---------------------------------------------------------------------------------------------------+
        //   0 | host       | string         | example.com   | Hostname that owns the cookie                       |
        //     +---------------------------------------------------------------------------------------------------+
        //   1 | subdomains | boolean string | FALSE         | Include subdomains (old attempt at SameSite)        |
        //     +---------------------------------------------------------------------------------------------------+
        //   2 | path       | string         | /             | Pathname that owns the cookie at the host           |
        //     +---------------------------------------------------------------------------------------------------+
        //   3 | isSecure   | boolean string | TRUE          | Send/receive cookie over HTTPS only.                |
        //     +---------------------------------------------------------------------------------------------------+
        //   4 | expiry     | number         | 1663611142    | Cookie expiration in standard Unix timestamp format |
        //     +---------------------------------------------------------------------------------------------------+
        //   5 | name       | string         | cookiename    | Cookie name                                         |
        //     +---------------------------------------------------------------------------------------------------+
        //   6 | value      | string         | cookievalue   | Cookie value                                        |
        //     +------------+----------------+---------------+-----------------------------------------------------+
        //
        // Source: http://fileformats.archiveteam.org/index.php?title=Netscape_cookies.txt

        const string netsacpePattern = @"^\S+\s{1}(TRUE|FALSE){1}\s{1}\S+\s{1}(TRUE|FALSE){1}\s{1}\d+\s{1}\S+\s{1}\S+";

        var cookieContainer = new CookieContainer();
        var cookieLines = cookieData.Split(Environment.NewLine)
            .Select(line => line.StartsWith("#HttpOnly_") ? line.Replace("#HttpOnly_", string.Empty) : line)
            .Where(line => !line.StartsWith("#"))
            .ToList();

        if (cookieLines.All(line => Regex.IsMatch(line, netsacpePattern)))
        {
            // Netscape Format.
            foreach (var array in cookieLines.Select(cookieLine => cookieLine.Split('\t')))
            {
                if (array.Length != 7) throw new ArgumentException();

                try
                {
                    cookieContainer.Add(new Cookie(name: array[5], value: array[6], path: array[2], domain: array[0]));
                }
                catch (Exception)
                {
                    //
                }
            }
        }
        else
        {
            // JSON Format.
            var cookies = JsonNode.Parse(cookieData)?.AsArray();

            if (cookies is null)
            {
                return cookieContainer;
            }

            foreach (var cookie in cookies.AsArray())
            {
                if (cookie is null)
                {
                    continue;
                }

                try
                {
                    cookieContainer.Add(new Cookie(
                        name: cookie["name"]?.GetValue<string>() ?? string.Empty,
                        value: cookie["value"]?.GetValue<string>() ?? string.Empty,
                        path: cookie["path"]?.GetValue<string>() ?? string.Empty,
                        domain: cookie["domain"]?.GetValue<string>() ?? string.Empty));
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        return cookieContainer;
    }
}