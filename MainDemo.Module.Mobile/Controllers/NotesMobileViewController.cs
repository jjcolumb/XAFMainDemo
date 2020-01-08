using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Mobile.Editors;
using DevExpress.ExpressApp.Mobile.MobileModel;

namespace MainDemo.Module.Mobile.Controllers {
    public class NotesMobileViewController : ViewController {
        public NotesMobileViewController() {
            TargetObjectType = typeof(DevExpress.Persistent.BaseImpl.Note);
        }
        protected override void OnActivated() {
            base.OnActivated();
            MobilePropertyEditor propertyEditor;
            if(View is ListView) {
                propertyEditor = ((MobileListEditor)((ListView)View).Editor).RowItem.Items["Text"] as MobilePropertyEditor;
            }
            else {
                propertyEditor = ((DetailView)View).FindItem("Text") as MobilePropertyEditor;
            }
            if(propertyEditor != null) {
                if(propertyEditor.Control != null) {
                    EnableHtml(propertyEditor);
                }
                else {
                    propertyEditor.ControlCreated += PropertyEditor_ControlCreated;
                }
            }
        }
        private void EnableHtml(MobilePropertyEditor propertyEditor) {
            Component component = propertyEditor.Control as Component;
            if(component != null) {
                component.Encode = false;
            }
        }
        private void PropertyEditor_ControlCreated(object sender, EventArgs e) {
            EnableHtml((MobilePropertyEditor)sender);
        }
    }

}
