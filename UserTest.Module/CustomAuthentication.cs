using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security;
using UserTest.Module.BusinessObjects;

namespace UserTest.Module
{
    /// <summary>
    /// 自定义登录参数类
    /// </summary>
    public class CustomAuthentication : AuthenticationBase, IAuthenticationStandard
    {
        private CustomLogonParameters customLogonParameters;            //登录参数
        public CustomAuthentication()
        {
            customLogonParameters = new CustomLogonParameters();
        }
        /// <summary>
        /// 注销
        /// </summary>
        public override void Logoff()
        {
            base.Logoff();
            customLogonParameters = new CustomLogonParameters();
        }
        /// <summary>
        /// 重置登录参数
        /// </summary>
        public override void ClearSecuredLogonParameters()
        {
            customLogonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        /// <summary>
        /// 通过比较登录参数与数据库中用户对象的值，进行验证。
        /// 
        /// </summary>
        /// <param name="objectSpace">用户数据操作对象空间</param>
        /// <returns>经身份验证的用户</returns>
        public override object Authenticate(IObjectSpace objectSpace)
        {

            Employee employee = objectSpace.FindObject<Employee>(new BinaryOperator("UserName", customLogonParameters.UserName));
            if (employee == null)
            {
                throw new ArgumentNullException("Employee");
            }
            if (!employee.ComparePassword(customLogonParameters.Password))
            {
                throw new AuthenticationException(employee.UserName, "密码错误.");
            }
            return employee;
        }

        /// <summary>
        /// 初始化登录参数
        /// </summary>
        /// <param name="logonParameters">登录参数</param>
        public override void SetLogonParameters(object logonParameters)
        {
            this.customLogonParameters = (CustomLogonParameters)logonParameters;
        }

        /// <summary>
        /// 返回到要添加程序的应用列表
        /// </summary>
        /// <returns></returns>
        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(CustomLogonParameters) };
        }
        /// <summary>
        /// 指示登录过程是否是交互式的(通过登录对话框请求登录参数)
        /// </summary>
        public override bool AskLogonParametersViaUI
        {
            get { return true; }
        }

        /// <summary>
        /// 返回登录参数
        /// </summary>
        public override object LogonParameters
        {
            get { return customLogonParameters; }
        }
        /// <summary>
        /// 是否启用注销方法，继承AuthenticationBase
        /// </summary>
        public override bool IsLogoffEnabled
        {
            get { return true; }
        }
    }
}