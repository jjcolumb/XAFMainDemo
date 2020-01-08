using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Mobile.Editors;
using System.Collections.Generic;
using DevExpress.ExpressApp.Mobile.MobileModel;

namespace MainDemo.Module.Mobile.Controllers {
    public partial class MobileNullTextEditorController : ViewController {
        public MobileNullTextEditorController() {
            InitializeComponent();
            RegisterActions(components);
        }
        private void InitNullText(MobilePropertyEditor propertyEditor) {
            if(propertyEditor.ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit) {
                ((DateBox)propertyEditor.Control)["inputAttr"] = new Dictionary<string, object> {
                    { "placeholder", CaptionHelper.NullValueText }
                };
            }
        }
        private void propertyEditor_ControlCreated(object sender, EventArgs e) {
            InitNullText((MobilePropertyEditor)sender);
        }
        protected override void OnActivated() {
            base.OnActivated();
            MobilePropertyEditor propertyEditor = ((DetailView)View).FindItem("Anniversary") as MobilePropertyEditor;
            if(propertyEditor != null) {
                if(propertyEditor.Control != null) {
                    InitNullText(propertyEditor);
                }
                else {
                    propertyEditor.ControlCreated += new EventHandler<EventArgs>(propertyEditor_ControlCreated);
                }
            }
        }
        protected override void OnDeactivated() {
			base.OnDeactivated();
			ViewItem propertyEditor = ((DetailView)View).FindItem("Anniversary");
			if(propertyEditor != null) {
				propertyEditor.ControlCreated -= new EventHandler<EventArgs>(propertyEditor_ControlCreated);
			}
		}

    }
}
