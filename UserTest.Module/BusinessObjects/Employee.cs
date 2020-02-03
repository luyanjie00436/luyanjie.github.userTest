using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace UserTest.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty("UserName"),XafDisplayName("员工")]
    public class Employee : PermissionPolicyUser
    {
        public Employee(Session session) : base(session) { }
        private Company company;
        [Association("Company-Employees")]
        [XafDisplayName("公司")]
        public Company Company
        {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }
    }
}