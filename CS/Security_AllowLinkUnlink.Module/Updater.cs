using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace Security_AllowLinkUnlink.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if(ObjectSpace.GetObjects<User>().Count == 0) {
                User user = ObjectSpace.CreateObject<User>();
                user.UserName = "Sam";
                user.Save();

                Role userRole = ObjectSpace.CreateObject<Role>();
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));
                userRole.AddPermission(new ObjectAccessPermission(typeof(MyPerson), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));
                userRole.AddPermission(new ObjectAccessPermission(typeof(MyOrganization), ObjectAccess.Create | ObjectAccess.Delete | ObjectAccess.Write, ObjectAccessModifier.Deny));

                userRole.Users.Add(user);

                userRole.Save();

                MyOrganization organization = ObjectSpace.CreateObject<MyOrganization>();
                organization.FullName = "Organization 1";
                organization.Save();

                MyPerson person = ObjectSpace.CreateObject<MyPerson>();
                person.Name = "Person 1";
                person.Save();
            }
        }
    }
}
