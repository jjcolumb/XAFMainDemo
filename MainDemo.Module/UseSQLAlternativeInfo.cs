using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Xpo;

namespace Demos.Data {
    [DomainComponent]
    public class UseSQLAlternativeInfo {
        public string SQLIssue { get; set; }
        public string Alternative { get; set;}
        public string Restrictions { get; set; }
    }
}
