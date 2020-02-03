using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace UserTest.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("公司"),XafDefaultProperty("Name")]
    public class Company : BaseObject
    {
        public Company(Session session) : base(session) { }
        private string name;
        
        [XafDisplayName("名称")]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
        [Association("Company-Employees")]
        public XPCollection<Employee> Employees
        {
            get { return GetCollection<Employee>("Employees"); }
        }
    }
}