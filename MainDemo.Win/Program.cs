using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Internal;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Win.Templates.Bars.ActionControls;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.XtraEditors;
using Demos.Data;

namespace MainDemo.Win {
    public class Program {
        [STAThread]
        public static void Main(string[] arguments) {
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(Tracing.GetFileLocationFromSettings() == FileLocation.CurrentUserApplicationDataFolder) {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            MainDemoWinApplication winApplication = new MainDemoWinApplication();
            DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
#if DEBUG
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            AuditTrailService.Instance.QueryCurrentUserName += new QueryCurrentUserNameEventHandler(Instance_QueryCurrentUserName);
            winApplication.CustomizeFormattingCulture += new EventHandler<CustomizeFormattingCultureEventArgs>(winApplication_CustomizeFormattingCulture);
            winApplication.LastLogonParametersReading += new EventHandler<LastLogonParametersReadingEventArgs>(winApplication_LastLogonParametersReading);
            winApplication.CustomizeTemplate += new EventHandler<CustomizeTemplateEventArgs>(WinApplication_CustomizeTemplate);
            SecurityAdapterHelper.Enable();
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            if(connectionStringSettings != null) {
                winApplication.ConnectionString = connectionStringSettings.ConnectionString;
            }
            if(string.IsNullOrEmpty(winApplication.ConnectionString) && winApplication.Connection == null) {
                connectionStringSettings = ConfigurationManager.ConnectionStrings["SqlExpressConnectionString"];
                if(connectionStringSettings != null) {
                    string connectionString = connectionStringSettings.ConnectionString;
                    if(connectionString != InMemoryDataStoreProvider.ConnectionString) {
                        connectionString = DemoDbEngineDetectorHelper.PatchSQLConnectionString(connectionString);
                        if(connectionString == DemoDbEngineDetectorHelper.AlternativeConnectionString) {
                            connectionString = InMemoryDataStoreProvider.ConnectionString;
                            UseSQLAlternativeInfoSingleton.Instance.FillFields(DemoDbEngineDetectorHelper.SQLServerIsNotFoundMessage, DemoXPODatabaseHelper.AlternativeName, DemoXPODatabaseHelper.InMemoryDatabaseUsageMessage);
                        }
                    }
                    winApplication.ConnectionString = connectionString;
                }
            }
#if DEBUG
            foreach(string argument in arguments) {
                if(argument.StartsWith("-connectionString:")) {
                    string connectionString = argument.Replace("-connectionString:", "");
                    winApplication.ConnectionString = connectionString;
                }
            }
#endif
            if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            try {
                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.StopSplash();
                winApplication.HandleException(e);
            }
        }

        private static void WinApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e) {
            if(e.Context == TemplateContext.ApplicationWindow || e.Context == TemplateContext.View) {
                RibbonForm ribbonForm = e.Template as RibbonForm;
                IActionControlsSite actionControlsSite = ribbonForm as IActionControlsSite;
                if((ribbonForm != null) && (actionControlsSite != null)) {
                    IActionControlContainer filtersActionControlContainer = actionControlsSite.ActionContainers.FirstOrDefault<IActionControlContainer>(x => x.ActionCategory == "Filters");
                    if(filtersActionControlContainer is BarLinkActionControlContainer) {
                        BarLinkActionControlContainer barFiltersActionControlContainer = (BarLinkActionControlContainer)filtersActionControlContainer;
                        BarLinkContainerItem barFiltersItem = barFiltersActionControlContainer.BarContainerItem;
                        RibbonControl ribbonControl = ribbonForm.Ribbon;
                        foreach(RibbonPage page in ribbonControl.Pages) {
                            foreach(RibbonPageGroup group in page.Groups) {
                                BarItemLink barFiltersItemLink = group.ItemLinks.FirstOrDefault<BarItemLink>(x => x.Item == barFiltersItem);
                                if(barFiltersItemLink != null) {
                                    group.ItemLinks.Remove(barFiltersItemLink);
                                }
                            }
                        }
                        ribbonForm.Ribbon.PageHeaderItemLinks.Add(barFiltersItem);
                    }

                }
            }
            else if((e.Context == TemplateContext.LookupControl) || (e.Context == TemplateContext.LookupWindow)) {
                LookupControlTemplate lookupControlTemplate = e.Template as LookupControlTemplate;
                if(lookupControlTemplate == null && e.Template is LookupForm) {
                    lookupControlTemplate = ((LookupForm)e.Template).FrameTemplate;
                }
                if(lookupControlTemplate != null) {
                    lookupControlTemplate.ObjectsCreationContainer.ContainerId = "LookupNew";
                    lookupControlTemplate.SearchActionContainer.ContainerId = "LookupFullTextSearch";
                }
            }
        }

        static void Instance_QueryCurrentUserName(object sender, QueryCurrentUserNameEventArgs e) {
            e.CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
        static void winApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e) {
            e.FormattingCulture = CultureInfo.GetCultureInfo("en-US");
        }
        static void winApplication_LastLogonParametersReading(object sender, LastLogonParametersReadingEventArgs e) {
            if(string.IsNullOrEmpty(e.SettingsStorage.LoadOption("", "UserName"))) {
                e.SettingsStorage.SaveOption("", "UserName", "Sam");
            }
        }
    }
}
