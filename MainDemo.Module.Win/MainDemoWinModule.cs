using System;
using System.Collections.Generic;
using System.ComponentModel;
using Demos.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Updating;

namespace MainDemo.Module.Win {
	[ToolboxItemFilter("Xaf.Platform.Win")]
	public sealed partial class MainDemoWinModule : ModuleBase {
		public MainDemoWinModule() {
			InitializeComponent();
			DevExpress.ExpressApp.Scheduler.Win.SchedulerListEditor.DailyPrintStyleCalendarHeaderVisible = false;
            DevExpress.Persistent.Base.ReportsV2.DataSourceBase.EnableAsyncLoading = false;
            // TODO: DXCORE3
#if !DXCORE3
            DevExpress.ExpressApp.ReportsV2.Win.WinReportServiceController.UseNewWizard = true;
#endif
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // TODO: DXCORE3
#if !DXCORE3
            Demos.Feedback.XAFFeedbackHelper helper = new Demos.Feedback.XAFFeedbackHelper(application);
#endif
        }
        public override IList<PopupWindowShowAction> GetStartupActions() {
            if(UseSQLAlternativeInfoSingleton.Instance.UseAlternative) {
                IList<PopupWindowShowAction> startupActions = base.GetStartupActions();
                PopupWindowShowAction showUseSQLAlternativeInfoAction = new PopupWindowShowAction();
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(UseSQLAlternativeInfo));
                UseSQLAlternativeInfo useSqlAlternativeInfo = objectSpace.GetObject<UseSQLAlternativeInfo>(UseSQLAlternativeInfoSingleton.Instance.Info);
                showUseSQLAlternativeInfoAction.CustomizePopupWindowParams += delegate (Object sender, CustomizePopupWindowParamsEventArgs e) {
                    e.View = Application.CreateDetailView(objectSpace, useSqlAlternativeInfo, true);
                    e.DialogController.CancelAction.Active["Required"] = false;
                    e.IsSizeable = false;
                };
                startupActions.Add(showUseSQLAlternativeInfoAction);
                return startupActions;
            }
            else {
                return base.GetStartupActions();
            }
        }
    }
}
