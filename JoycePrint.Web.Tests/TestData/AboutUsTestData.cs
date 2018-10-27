using System;

namespace JoycePrint.Web.Tests.TestData
{
    public class AboutUsTestData
    {
        /// <summary>
        /// The company history text that appears on the about us page
        /// </summary>
        public string CompanyHistoryText => $"ABOUT US{Environment.NewLine}Based in Foxford, Co.Mayo, Joyceprint was established in 1994 and has been a Print Trade supplier for over 20 years. As a family-run Irish company, we are widely known for our extensive range of Carbonless NCR products. We offer a wide range of quality products and pride ourselves on providing a friendly and helpful service with fast turnarounds and competitive pricing. Whether you require invoice books, order books, delivery books, receipt books, restaurant books/pads, NCR sets or pads, this is the site to order them from.{Environment.NewLine}{Environment.NewLine}Docket books are available in duplicate, triplicate and four part. All standard sizes from A6 to A3 can be purchased and custom sizes are catered for. NCR sets and pads are also available. We ship anywhere within Ireland.{Environment.NewLine}{Environment.NewLine}Our clients are based nationwide and our best form of advertising is, and always has been, recommendations by satisfied customers. We can guarantee our clients total security and confidentiality.{Environment.NewLine}{Environment.NewLine}Thank you for visiting our website. Contact us today for a no obligation quote.";

        /// <summary>
        /// The address text that appears on the about us page
        /// </summary>
        public string AddressText => $"Castlebar Road,{Environment.NewLine}Foxford, Co Mayo,{Environment.NewLine}F26HH50";

        /// <summary>
        /// The phone text that appears on the about us page
        /// </summary>
        public string PhoneText => "+353-94-9256876";

        /// <summary>
        /// The phone link that appears on the about us page
        /// </summary>
        public string PhoneLink => "tel:353949256876";

        /// <summary>
        /// The email text that appears on the about us page
        /// </summary>
        public string EmailText => "joyceprint@gmail.com";

        /// <summary>
        /// The email link that appears on the about us page
        /// </summary>
        public string EmailLink => "mailto:info@yourdomain.com";

        /// <summary>
        /// The mobile text that appears on the about us page
        /// </summary>
        public string MobileText => "+353-86-0670627";

        /// <summary>
        /// The mobile link that appears on the about us page
        /// </summary>
        public string MobileLink => "tel:353860670627";
    }
}
