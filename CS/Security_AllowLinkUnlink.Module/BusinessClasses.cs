using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace Security_AllowLinkUnlink.Module {
    [DefaultClassOptions]
    public class MyPerson : XPObject {
        public MyPerson(Session session) : base(session) { }
        public string Name {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue<string>("Name", value); }
        }
        [Association("Organizations-Persons")]
        public XPCollection<MyOrganization> Organizations {
            get { return GetCollection<MyOrganization>("Organizations"); }
        }
    }

    [DefaultClassOptions]
    public class MyOrganization : XPObject {
        public MyOrganization(Session session) : base(session) { }
        public string FullName {
            get { return GetPropertyValue<string>("FullName"); }
            set { SetPropertyValue<string>("FullName", value); }
        }
        [Association("Organizations-Persons")]
        public XPCollection<MyPerson> Persons {
            get { return GetCollection<MyPerson>("Persons"); }
        }
    }
}
