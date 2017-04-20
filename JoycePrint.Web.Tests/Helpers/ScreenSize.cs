namespace JoycePrint.Web.Tests.Helpers
{
    public class ScreenSize
    {
        /// <summary>
        /// The height of the screen
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The width of the screen
        /// </summary>
        public int Width { get; set; }

        #region Constructors

        public ScreenSize()
        {

        }

        public ScreenSize(int height, int width)
        {
            Height = height;
            Width = width;
        }

        #endregion
    }
}