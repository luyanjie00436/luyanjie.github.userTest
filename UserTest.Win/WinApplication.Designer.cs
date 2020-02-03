namespace UserTest.Win {
    partial class UserTestWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new UserTest.Module.UserTestModule();
            this.module4 = new UserTest.Module.Win.UserTestWindowsFormsModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationWindowsFormsModule = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.customAuthentication1 = new UserTest.Module.CustomAuthentication();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.customAuthentication1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.securityStrategyComplex1.UserType = typeof(UserTest.Module.BusinessObjects.Employee);
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // UserTestWindowsFormsApplication
            // 
            this.ApplicationName = "UserTest";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.validationWindowsFormsModule);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Security = this.securityStrategyComplex1;
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.UserTestWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.UserTestWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private UserTest.Module.UserTestModule module3;
        private UserTest.Module.Win.UserTestWindowsFormsModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWindowsFormsModule;
        private Module.CustomAuthentication customAuthentication1;
    }
}
