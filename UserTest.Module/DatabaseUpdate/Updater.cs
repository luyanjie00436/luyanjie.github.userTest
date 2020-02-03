using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using UserTest.Module.BusinessObjects;

namespace UserTest.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            PermissionPolicyRole administrativeRole = ObjectSpace.FindObject<PermissionPolicyRole>(
             new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (administrativeRole == null)
            {
                administrativeRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                administrativeRole.Name = SecurityStrategy.AdministratorRoleName;
                administrativeRole.IsAdministrative = true;
            }
            const string adminName = "Administrator";
            Employee administratorUser = ObjectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", adminName));
            if (administratorUser == null)
            {
                administratorUser = ObjectSpace.CreateObject<Employee>();
                administratorUser.UserName = adminName;
                administratorUser.IsActive = true;
                administratorUser.SetPassword("");
                administratorUser.Roles.Add(administrativeRole);
            }
            PermissionPolicyRole userRole = ObjectSpace.FindObject<PermissionPolicyRole>(
                new BinaryOperator("Name", "User"));
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "User";
                userRole.AddTypePermission<Employee>(
                    SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                userRole.AddTypePermission<Company>(
                    SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
            }
            if (ObjectSpace.FindObject<Company>(null) == null)
            {
                Company company1 = ObjectSpace.CreateObject<Company>();
                company1.Name = "Company 1";
                company1.Employees.Add(administratorUser);
                Employee user1 = ObjectSpace.CreateObject<Employee>();
                user1.UserName = "Sam";
                user1.SetPassword("");
                user1.Roles.Add(userRole);
                Employee user2 = ObjectSpace.CreateObject<Employee>();
                user2.UserName = "John";
                user2.SetPassword("");
                user2.Roles.Add(userRole);
                Company company2 = ObjectSpace.CreateObject<Company>();
                company2.Name = "Company 2";
                company2.Employees.Add(user1);
                company2.Employees.Add(user2);
            }
            ObjectSpace.CommitChanges(); //此行代码保存创建的对象
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

				defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);                
            }
            return defaultRole;
        }
    }
}
