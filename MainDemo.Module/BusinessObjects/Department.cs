using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty(nameof(Department.Title))]
    public class Department : BaseObject {
        private string title;
        private string office;
        public Department(Session session)
            : base(session) {
        }
        public string Title {
            get {
                return title;
            }
            set {
                SetPropertyValue(nameof(Title), ref title, value);
            }
        }
        public string Office {
            get {
                return office;
            }
            set {
                SetPropertyValue(nameof(Office), ref office, value);
            }
        }
        [Association("Department-Contacts")]
        public XPCollection<Contact> Contacts {
            get {
                return GetCollection<Contact>(nameof(Contacts));
            }
        }
        [Association("Departments-Positions")]
        public XPCollection<Position> Positions {
            get {
                return GetCollection<Position>(nameof(Positions));
            }
        }
    }
}
