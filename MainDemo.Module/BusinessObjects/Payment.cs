using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Payment : BaseObject {
        private double rate;
        private double hours;

        public Payment(Session session)
            : base(session) {
        }
        private Contact contact;
        public Contact Contact {
            get {
                return contact;
            }
            set {
                SetPropertyValue(nameof(Contact), ref contact, value);
            }
        }
        public double Rate {
            get {
                return rate;
            }
            set {
                if(SetPropertyValue(nameof(Rate), ref rate, value))
                    OnChanged(nameof(Amount));
            }
        }
        public double Hours {
            get {
                return hours;
            }
            set {
                if(SetPropertyValue(nameof(Hours), ref hours, value))
                    OnChanged(nameof(Amount));
            }
        }
        [PersistentAlias("Rate * Hours")]
        public double Amount {
            get {
                return Convert.ToDouble(EvaluateAlias(nameof(Amount)));
            }
        }
    }
}
