# OBSOLETE - How to allow Link/Unlink objects for a ManyToMany collection property when an end-user doesn't have permissions to modify the associated objects


<p><strong>==============================<br><strong>This solution can be applied only to the old Security System. <br>Starting with v13.2, you can configure member permissions to grant access to the associated collections (see <a href="https://www.devexpress.com/Support/Center/p/S30570">Security - Provide the capability to link/unlink objects in non-aggregated collections if they have the corresponding permissions while the master object is restricted</a> ). You should not use this example in a new project.<br></strong><strong>==============================</strong><br><br></strong>Suppose, I have the Organization and Person classes. Each Person object can be associated with a number of Organization objects, and each Organization object can be associated with a number of Person objects (a many-to-many relationship, see the BusinessClasses.cs file):<strong><br></strong></p>


```cs
	public class Person : XPObject {

		public Person(Session session) : base(session) { }

		public string Name {

			get { return GetPropertyValue<string>("Name"); }

			set { SetPropertyValue<string>("Name", value); }

		}

		[Association("Organizations-Persons2")]

		public XPCollection<Organization> Organizations {

			get { return GetCollection<Organization>("Organizations"); }

		}

	}



	public class Organization : XPObject {

		public Organization(Session session) : base(session) { }

		public string FullName {

			get { return GetPropertyValue<string>("FullName"); }

			set { SetPropertyValue<string>("FullName", value); }

		}

		[Association("Organizations-Persons")]

		public XPCollection<Person> Persons {

			get { return GetCollection<Person>("Persons"); }

		}

	}



```


<p>And, an end-user needs to modify any Person object, including its "Organizations" collection ('Link' or 'Unlink' organization objects), but he or she should not modify any property of an Organization object.</p>
<p>To accomplish this, I grant all operations under the Person objects and grant only the Read operation over the Organization objects (see the Updater.cs file):</p>


```cs
	userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));

	userRole.AddPermission(new ObjectAccessPermission(typeof(Person), ObjectAccess.AllAccess, ObjectAccessModifier.Allow));

	userRole.AddPermission(new ObjectAccessPermission(typeof(Organization), ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny));



```


<p>With these permissions, I have achieved the necessary behavior, except for that an end-user cannot modify the Organizations collection property. This is a restriction of the current implementation of the XAF Security system.</p>
<p>To avoid it, I introduce a new class and manually allow modifying the "Organizations" property in the code (see the AllowManyToMany_ObjectAccessComparer.cs file):</p>


```cs
	public class AllowManyToMany_ObjectAccessComparer : ObjectAccessComparer {

		public override bool IsSubsetOf(ObjectAccessPermission sourcePermission, ObjectAccessPermission targetPermission) {

			if(sourcePermission.AccessItemList.Count == 1) {

				ParticularAccessItem sourceAccessItem = sourcePermission.AccessItemList[0];

				if(typeof(Organization).IsAssignableFrom(sourceAccessItem.ObjectType) && sourceAccessItem.Access == ObjectAccess.Write && sourceAccessItem.Modifier == ObjectAccessModifier.Allow) {

					if(sourcePermission.Contexts != null && sourcePermission.Contexts.CollectionPropertyContext != null

						&& sourcePermission.Contexts.CollectionPropertyContext.CollectionPropertyName == "Organizations"

						&& typeof(Person).IsAssignableFrom(sourcePermission.Contexts.CollectionPropertyContext.MasterObjectType)) {

						return true;

					}

				}

			}

			return base.IsSubsetOf(sourcePermission, targetPermission);

		}

	}



```


<p>and set this class as the current ObjectAccessComparer in the main.cs and global.aspx.cs files:</p>


```cs
	ObjectAccessComparerBase.SetCurrentComparer(new AllowManyToMany_ObjectAccessComparer ());



```


<p>Thanks,<br> Dan</p>

<br/>


