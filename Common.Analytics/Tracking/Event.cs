namespace Common.Analytics.Tracking
{
    /// <summary>
    /// Holds data for an Analytics Event Hit
    /// </summary>
    public class Event : Base
    {
        public string Category;

        public string Action;

        public string Label;

        public string Value;
    }
}
