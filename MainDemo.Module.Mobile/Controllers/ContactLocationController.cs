using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Maps.Mobile;
using DevExpress.ExpressApp.Maps.Mobile.Editors;
using DevExpress.ExpressApp.Mobile.Editors;
using MainDemo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDemo.Module.Mobile.Controllers {
    public class ContactLocationController : ObjectViewController<ListView, Contact> {

        protected override void OnActivated() {
            base.OnActivated();
            MobileMapsListEditor editor = View.Editor as MobileMapsListEditor;
            if(editor != null) {
                editor.CustomGetFocusedObjectClientSideScript += (s, e) => {
                    e.Script = @"{ 
                        Location: { 
                            Latitude: $data.location.lat, 
                            Longitude: $data.location.lng 
                        }
                    }";
                };
            }
        }
    }
}
