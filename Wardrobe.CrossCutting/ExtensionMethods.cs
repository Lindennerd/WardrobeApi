using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Wardrobe.CrossCutting;

public static class ExtensionMethods
{
    public static string ConvertToBase64(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string GetMimeType(this IFormFile file)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(file.FileName, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        
        return contentType;
    }

    public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum) return default;
        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? string.Empty);

        if (fieldInfo == null) return description;
        if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            description = attributes.First().Description;
        }

        return description;
    }

    public static string ConvertToMd5Hash(this string value)
    {
        var hashedBytes = MD5.HashData(Encoding.Default.GetBytes(value));
        var sb = new StringBuilder();

        foreach (var t in hashedBytes)
        {
            sb.Append(t.ToString("x2"));
        }

        return sb.ToString();
    }
}