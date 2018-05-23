using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace Security_AllowLinkUnlink.Win {
    public partial class Security_AllowLinkUnlinkWindowsFormsApplication : WinApplication {
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }
        public Security_AllowLinkUnlinkWindowsFormsApplication() {
            InitializeComponent();
        }

        private void Security_AllowLinkUnlinkWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
			e.Updater.Update();
			e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "The automatic update is disabled, because the application was started without debugging.\r\n" +
                    "You should start the application under Visual Studio, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.");
            }
#endif
        }

        private void Security_AllowLinkUnlinkWindowsFormsApplication_LastLogonParametersRead(object sender, LastLogonParametersReadEventArgs e) {
            AuthenticationStandardLogonParameters logonParams = (AuthenticationStandardLogonParameters)e.LogonObject;
            if(string.IsNullOrEmpty(logonParams.UserName)) {
                logonParams.UserName = "Sam";
            }
        }
    }
}
