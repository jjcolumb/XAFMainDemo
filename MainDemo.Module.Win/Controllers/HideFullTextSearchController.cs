using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using MainDemo.Module.BusinessObjects;

namespace MainDemo.Module.Win.Controllers {
    public class HideFullTextSearchController : ObjectViewController<ListView, DemoTask> {
        public HideFullTextSearchController() : base() {
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
        }
        protected override void OnActivated() {
            base.OnActivated();
            FilterController filterController = Frame.GetController<FilterController>();
            if(filterController != null) {
                filterController.FullTextFilterAction.Active["Use the Find By Subject Action instead"] = false;
            }
        }
    }
}
