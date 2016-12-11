using JoycePrint.Web.Tests.Helpers;

namespace JoycePrint.Web.Tests.TestData
{
    public class HeaderTestData
    {
        public string HomeText => "Home";

        public string HomeLink => $"{Urls.UrlDellDev}/?action=Index";

        public string QuoteText => "Get A Quote";

        public string QuoteLink => $"{Urls.UrlDellDev}/quote?action=Index";

        public string AboutUsText => "About Us";

        public string AboutUsLink => $"{Urls.UrlDellDev}/aboutus?action=Index";
    }
}
