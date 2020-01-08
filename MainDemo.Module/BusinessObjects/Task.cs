using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    [Appearance("FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView", Criteria = "Status=='Completed'", FontStyle =System.Drawing.FontStyle.Strikeout)]
    public class DemoTask : Task, IComparable {
        private Priority priority;
        private int estimatedWorkHours;
        private int actualWorkHours;
        public DemoTask(Session session)
            : base(session) {
        }
        [Appearance("PriorityBackColorPink", AppearanceItemType = "ViewItem", Criteria = "Priority=2", BackColor = "0xfff0f0")]
        public Priority Priority {
            get {
                return priority;
            }
            set {
                SetPropertyValue(nameof(Priority), ref priority, value);
            }
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            Priority = Priority.Normal;
        }
        [ToolTip("View, assign or remove contacts for the current task")]
        [Association("Contact-DemoTask")]
        public XPCollection<Contact> Contacts {
            get {
                return GetCollection<Contact>(nameof(Contacts));
            }
        }
        public override string ToString() {
            return this.Subject;
        }
        [Action(ToolTip = "Postpone the task to the next day", ImageName = "State_Task_Deferred")]
        public void Postpone() {
            if (DueDate == DateTime.MinValue) {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }
        public int EstimatedWorkHours {
            get {
                return estimatedWorkHours;
            }
            set {
                SetPropertyValue<int>(nameof(EstimatedWorkHours), ref estimatedWorkHours, value);
            }
        }
        public int ActualWorkHours {
            get {
                return actualWorkHours;
            }
            set {
                SetPropertyValue<int>(nameof(ActualWorkHours), ref actualWorkHours, value);
            }
        }
    }
    public enum Priority {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}
