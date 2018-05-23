using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace Security_AllowLinkUnlink.Module {
    public class Updater : ModuleUpdater {
        public Updater(ObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if(new XPCollection<User>(Session).Count == 0) {
                User user = new User(Session);
                user.UserName = "Sam";
                user.Save();

                Role userRole = new Role(Session);
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));
                userRole.AddPermission(new ObjectAccessPermission(typeof(MyPerson), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));
                userRole.AddPermission(new ObjectAccessPermission(typeof(MyOrganization), ObjectAccess.Create | ObjectAccess.Delete | ObjectAccess.Write, ObjectAccessModifier.Deny));

                userRole.Users.Add(user);

                userRole.Save();

                MyOrganization organization = new MyOrganization(Session);
                organization.FullName = "Organization 1";
                organization.Save();

                MyPerson person = new MyPerson(Session);
                person.Name = "Person 1";
                person.Save();
            }
        }
    }
}
