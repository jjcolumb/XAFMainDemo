using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;

namespace Demos.Data.Web {
    public class UseSQLAlternativeInfoController : ObjectViewController<DetailView, UseSQLAlternativeInfo> {
        public static Unit SQLAlternativeInfoWindowWidth = new Unit(680);
        public static Unit SQLAlternativeInfoWindowHeight = new Unit(285);
        public UseSQLAlternativeInfoController() : base() { }
        protected override void OnActivated() {
            base.OnActivated();
            StaticTextViewItem sqlIssueItem = (StaticTextViewItem)View.FindItem("SQLIssueText");
            sqlIssueItem.Text = string.Format("<b><size=+2>{0}</size></b>", ViewCurrentObject.SQLIssue);

            StaticTextViewItem alternativeItem = (StaticTextViewItem)View.FindItem("AlternativeStaticText");
            alternativeItem.Text = string.Format("<b>{0}</b> will be used instead.", ViewCurrentObject.Alternative);

            StaticTextViewItem noteItem = (StaticTextViewItem)View.FindItem("NoteStaticText");
            noteItem.Text = string.Format("<b>Note:</b> {0}", ViewCurrentObject.Restrictions);
        }
    }

    public class ShowSQLAlternativeInfoController : WindowController {
        public ShowSQLAlternativeInfoController() : base() {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            if(UseSQLAlternativeInfoSingleton.Instance.UseAlternative) {
                Window.TemplateChanged += Window_TemplateChanged;
            }
        }

        private void Window_TemplateChanged(object sender, System.EventArgs e) {
            Window.TemplateChanged -= Window_TemplateChanged;
            if(Window.Template != null) {
                ((System.Web.UI.Page)((WebWindow)Window).Template).LoadComplete += ShowSQLAlternativeInfoController_LoadComplete;
            }
        }

        private void ShowSQLAlternativeInfoController_LoadComplete(object sender, System.EventArgs e) {
            ((System.Web.UI.Page)((WebWindow)Window).Template).LoadComplete -= ShowSQLAlternativeInfoController_LoadComplete;
            IObjectSpace detailViewObjectSpace = WebApplication.Instance.CreateObjectSpace(typeof(Demos.Data.UseSQLAlternativeInfo));
            DetailView useSQLAlternativeDetailView = WebApplication.Instance.CreateDetailView(detailViewObjectSpace, detailViewObjectSpace.GetObject(Demos.Data.UseSQLAlternativeInfoSingleton.Instance.Info));
            WebApplication.Instance.PopupWindowManager.PopupShowing += PopupWindowManager_PopupShowing;
            WebApplication.Instance.ShowViewStrategy.ShowViewInPopupWindow(useSQLAlternativeDetailView);
        }

        private void PopupWindowManager_PopupShowing(object sender, PopupShowingEventArgs e) {
            WebApplication.Instance.PopupWindowManager.PopupShowing -= PopupWindowManager_PopupShowing;
            e.PopupControl.CustomizePopupWindowSize += (s, ea) => {
                ea.Width = UseSQLAlternativeInfoController.SQLAlternativeInfoWindowWidth;
                ea.Height = UseSQLAlternativeInfoController.SQLAlternativeInfoWindowHeight;
                DialogController dialogController = ea.PopupFrame.GetController<DialogController>();
                if(dialogController != null) {
                    dialogController.CancelAction.Active["Required"] = false;
                }
                ea.PopupTemplateType = DevExpress.ExpressApp.Web.Controls.PopupTemplateType.FindDialog;
                ea.Handled = true;
            };
        }
    }
    
}
