using System.Text;

namespace MoocResolver.Helpers;

public class Base64Helper
{
    public static string Encode(string plainText)
    {
        var toEncodeAsBytes = Encoding.ASCII.GetBytes(plainText);
        return Convert.ToBase64String(toEncodeAsBytes);
    }

    public static string Decode(string encodedText)
    {
        var encodedDataAsBytes = Convert.FromBase64String(encodedText);
        return Encoding.ASCII.GetString(encodedDataAsBytes);
    }
}