using System.Collections.Specialized;
using System.Web;
using System.Configuration.Provider;

namespace Analytics.Analyzer
{
    public abstract class AnalyzerProvider : ProviderBase, IAnalyzer
    {
        /// <summary>
        /// Same as public bool Enabled { get; private set; }
        /// </summary>
        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
        }

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

        public abstract void Analyze(HttpContext context);
    }
}
