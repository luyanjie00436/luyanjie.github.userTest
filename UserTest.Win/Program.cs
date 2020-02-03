using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using UserTest.Module;
using UserTest.Module.BusinessObjects;

namespace UserTest.Win
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            if(Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder) {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            UserTestWindowsFormsApplication winApplication = new UserTestWindowsFormsApplication();
            // Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
            //winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
            SecurityAdapterHelper.Enable();
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            try {
                winApplication.CreateCustomLogonWindowObjectSpace += application_CreateCustomLogonWindowObjectSpace;

                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }
        }

        /// <summary>
        /// 创建登录窗口的对象空间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void application_CreateCustomLogonWindowObjectSpace(object sender, CreateCustomLogonWindowObjectSpaceEventArgs e)
        {

            e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace(typeof(CustomLogonParameters));
            if (e.ObjectSpace is NonPersistentObjectSpace)
            {
                IObjectSpace objectSpaceCompany = ((XafApplication)sender).CreateObjectSpace(typeof(Company));
                ((NonPersistentObjectSpace)e.ObjectSpace).AdditionalObjectSpaces.Add(objectSpaceCompany);
            }
        }
    }
}
