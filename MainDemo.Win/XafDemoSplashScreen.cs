using System;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraSplashScreen;

namespace Demos.Win {
    public partial class XafDemoSplashScreen : DemoSplashScreen {
        private string GetSplashScreenImageResourcesName() {
            string splashScreenImageResourceName = "SplashScreenImage.svg";
            foreach(string resourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames()) {
                if(resourceName.EndsWith(splashScreenImageResourceName)) {
                    return resourceName;
                }
            }
            return splashScreenImageResourceName;
        }
        private void LoadSplashImageFromResource() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream svgStream = assembly.GetManifestResourceStream(GetSplashScreenImageResourcesName());
            svgStream.Position = 0;
            pictureEdit2.SvgImage = SvgImage.FromStream(svgStream);
        }
        public XafDemoSplashScreen() {
            InitializeComponent();
            LoadSplashImageFromResource();
        }
        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
            if((UpdateSplashCommand)cmd ==UpdateSplashCommand.Description) {
                labelControl2.Text = (string)arg;
            }
        }
    }
}
