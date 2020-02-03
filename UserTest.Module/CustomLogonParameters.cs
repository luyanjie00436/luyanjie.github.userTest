using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using UserTest.Module.BusinessObjects;

namespace UserTest.Module
{
    /// <summary>
    /// 自定义登录参数
    /// </summary>
    [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("登录")]                       //登录标题
    public class CustomLogonParameters : INotifyPropertyChanged, ISerializable
    {
        #region 构造方法
        public CustomLogonParameters() { }

        //ISerializable,序列化
        public CustomLogonParameters(SerializationInfo info, StreamingContext context)
        {
            if (info.MemberCount > 0)
            {
                UserName = info.GetString("UserName");
                Password = info.GetString("Password");
            }
        }
        #endregion
        
        private Company company;                //公司
        private Employee employee;              //员工
        private string password;                //密码

        [ImmediatePostData]   //值应该尽快传递给绑定对象，但用户更改值时，允许强制更新
        [XafDisplayName("公司")]
        public Company Company
        {
            get { return company; }
            set
            {
                if (value == company) return;
                company = value;
                Employee = null;
                OnPropertyChanged("Company");
            }
        }
        [DataSourceProperty("Company.Employees")]           
        [ImmediatePostData] //值应该尽快传递给绑定对象，但用户更改值时，允许强制更新
        [XafDisplayName("员工")]
        public Employee Employee
        {
            get { return employee; }
            set
            {
                if (Company == null) return;
                employee = value;
                if (employee != null)
                {
                    UserName = employee.UserName;
                }
                OnPropertyChanged("Employee");
            }
        }
        [Browsable(false)]                  //用户名在登录页面不显示    
        [XafDisplayName("用户名")]                 
        public String UserName { get; set; }
        [PasswordPropertyText(true)]  
        [XafDisplayName("密码")]
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value){ return; }
                password = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //属性更改时引发事件页面上能快速更改事件）
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }       

        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName);
            info.AddValue("Password", Password);
        }
    }
}