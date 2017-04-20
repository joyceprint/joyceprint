using System;
using System.Web;

namespace Analytics
{
    public class Analytics : IHttpModule
    {
        private static AnalysisEngine _engine;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;

            if (null == _engine)
                _engine = new AnalysisEngine();
        }

        public void Dispose() { }

        private void OnBeginRequest(object source, EventArgs evernArgs)
        {
            _engine?.CaptureAnalysis(((HttpApplication)source).Context);
        }
    }
}