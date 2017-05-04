using System.Collections.Specialized;
using System.Web;
using System.Configuration.Provider;
using System.Web.Hosting;

namespace Analytics.Analyzer
{
    public abstract class AnalyzerProvider : ProviderBase, IAnalyzer
    {
        #region Properties

        /// <summary>
        /// Same as public bool Enabled { get; private set; }
        /// </summary>
        private bool _enabled;

        protected bool Enabled => _enabled;

        /// <summary>
        /// 
        /// </summary>
        protected static readonly string ApplicationName = HostingEnvironment.SiteName;

        #endregion

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            try
            {
                _enabled = bool.Parse(config["enabled"]);
            }
            catch
            {
                _enabled = false;               
            }
        }

        public abstract void Analyze(HttpContext context, TrackingType type);
    }
}