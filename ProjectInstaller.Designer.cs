
namespace DarkTimeServices
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.darkTimeServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.darkTimeServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // darkTimeServiceProcessInstaller
            // 
            this.darkTimeServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.darkTimeServiceProcessInstaller.Password = null;
            this.darkTimeServiceProcessInstaller.Username = null;
            // 
            // darkTimeServiceInstaller
            // 
            this.darkTimeServiceInstaller.ServiceName = "DarkTimeServices";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.darkTimeServiceProcessInstaller,
            this.darkTimeServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller darkTimeServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller darkTimeServiceInstaller;
    }
}