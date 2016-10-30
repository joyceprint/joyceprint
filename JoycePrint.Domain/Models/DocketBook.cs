using JoycePrint.Domain.Enums;

namespace JoycePrint.Domain.Models
{
    public class DocketBook
    {
        /// <summary>
        /// The type of docket book
        /// </summary>
        public DocketBookType? Type { get; set; }

        /// <summary>
        /// The number of docket books requested
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The size of the dockets in the docket book
        /// </summary>
        public DocketBookSize? Size { get; set; }
    }
}