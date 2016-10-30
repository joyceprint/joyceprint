using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace JoycePrint.UI
{
    public class BundlesFormats
    {
        [Description("Renders the style bundle with [media = print]")]
        public const string Print = @"<link href=""{0}"" rel=""stylesheet"" type=""text/css"" media=""print"" />";

        [Description("Renders the style bundle with [media = screen]")]
        public const string Email = @"<link href=""{0}"" rel=""stylesheet"" type=""text/css"" media=""screen"" />";
    }
}