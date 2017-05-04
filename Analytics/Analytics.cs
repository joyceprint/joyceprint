using System;
using System.Web;

namespace Analytics
{
    public class Analytics : IHttpModule, IHttpHandler
    {
        private static AnalysisEngine _engine;

        #region HttpModule 

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

        #endregion

        #region Http Handler

        public void ProcessRequest(HttpContext context)
        {
            _engine?.CaptureAnalysis(context, TrackingType.Event);            
        }

        public bool IsReusable { get { return false; } }

        #endregion
    }
}