using System;
using System.Web;
using Analytics.Enums;

namespace Analytics
{
    public class Analytics : IHttpModule
    {
        private static AnalysisEngine _engine;

        /// <summary>
        /// Added this public accessor to allow mvc action attribute methods to use analytics
        /// </summary>
        public static AnalysisEngine Engine => _engine;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        
            if (null == _engine)
                _engine = new AnalysisEngine();
        }

        public void Dispose() { }

        private void OnBeginRequest(object source, EventArgs evernArgs)
        {
            _engine?.CaptureAnalysis(((HttpApplication)source).Context, TrackingType.Page);
        }
    }
}