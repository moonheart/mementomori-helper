using System.Runtime.InteropServices;
using System.Text;
using HtmlConverter.Configurations;
using HtmlConverter.Options;

namespace MementoMori.BotServer.Utils;

public static class ImageUtil
{
    public static byte[] HtmlToImage(string html, int? width = 600)
    {
        return HtmlConverter.Core.HtmlConverter.ConvertHtmlToImage(new ImageConfiguration
        {
            Content = html,
            Quality = 90,
            Format = ImageFormat.Jpg,
            Width = width,
            MinimumFontSize = 24,
        });
    }
}