using DevExpress.ExpressApp.ReportsV2.Mobile;
using DevExpress.Persistent.Base;
using DevExpress.XtraReports.Security;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
//using DXHttpProxyClient;

namespace MainDemo.Mobile {
    public class Global : System.Web.HttpApplication {
        public void Application_Start(object sender, EventArgs e) {
            DefaultWebDocumentViewerContainer.Register<IWebDocumentViewerReportResolver, XafReportsResolver<MainDemoMobileApplication>>();
        }
        protected void Application_BeginRequest(object sender, EventArgs e) {
            CorsSupport.HandlePreflightRequest(HttpContext.Current);
        }
    }
}
