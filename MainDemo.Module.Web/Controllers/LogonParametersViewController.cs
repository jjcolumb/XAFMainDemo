using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace MainDemo.Module.Web.Controllers {
    public class LogonParametersViewController : ObjectViewController<DetailView, AuthenticationStandardLogonParameters> {
        protected override void OnActivated() {
            base.OnActivated();
            ASPxStringPropertyEditor userNamePropertyEditor = (ASPxStringPropertyEditor)View.FindItem("UserName");
            userNamePropertyEditor.NullText = "Sam or John";
        }
    }
}
