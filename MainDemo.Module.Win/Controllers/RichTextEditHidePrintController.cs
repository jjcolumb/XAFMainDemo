using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.Persistent.BaseImpl;

namespace MainDemo.Module.Win.Controllers {
    public class RichTextEditHidePrintController : ViewController<ListView> {
        public RichTextEditHidePrintController() {
            TargetObjectType = typeof(Note);
        }
        protected override void OnActivated() {
            base.OnActivated();
            Frame.GetController<PrintingController>()?.Active.SetItemValue("RichTextEdit", false);
            Frame.GetController<WinExportController>()?.Active.SetItemValue("RichTextEdit", false);

        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            Frame.GetController<PrintingController>()?.Active.RemoveItem("RichTextEdit");
            Frame.GetController<WinExportController>()?.Active.RemoveItem("RichTextEdit");
        }
    }
}
