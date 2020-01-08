using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using MainDemo.Module.Web.Controllers;

namespace MainDemo.Module.Web {
    [ToolboxItemFilter("Xaf.Platform.Web")]
    public sealed partial class MainDemoWebModule : ModuleBase {
        private void Application_CreateCustomLogonWindowControllers(object sender, CreateCustomLogonWindowControllersEventArgs e) {
            e.Controllers.Add(new LogonParametersViewController());
        }
        public MainDemoWebModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomLogonWindowControllers += Application_CreateCustomLogonWindowControllers;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
}
