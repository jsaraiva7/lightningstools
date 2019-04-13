using System.ComponentModel;
using System.Windows.Forms;
using SimLinkup.UI.UserControls;

namespace SimLinkup.UI
{
    partial class frmOptions
    {

        /// Required designer variable.
        /// </summary>
        private IContainer components = null;


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

        #region Windows Form Designer generated code


        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPlugins = new System.Windows.Forms.TabPage();
            this.tabPluginsSubtabs = new System.Windows.Forms.TabControl();
            this.tabHardwareSupport = new System.Windows.Forms.TabPage();
            this.hardwareSupportModuleList = new SimLinkup.UI.UserControls.ModuleList();
            this.tabSimSupport = new System.Windows.Forms.TabPage();
            this.simSupportModuleList = new SimLinkup.UI.UserControls.ModuleList();
            this.tabStartup = new System.Windows.Forms.TabPage();
            this.gbStartupOptions = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numFrequency = new System.Windows.Forms.NumericUpDown();
            this.chkMinimizeWhenStarted = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToSystemTray = new System.Windows.Forms.CheckBox();
            this.chkStartAutomaticallyWhenLaunched = new System.Windows.Forms.CheckBox();
            this.chkLaunchAtSystemStartup = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPlugins.SuspendLayout();
            this.tabPluginsSubtabs.SuspendLayout();
            this.tabHardwareSupport.SuspendLayout();
            this.tabSimSupport.SuspendLayout();
            this.tabStartup.SuspendLayout();
            this.gbStartupOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(12, 2);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(93, 3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdOK);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer1.Size = new System.Drawing.Size(574, 283);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPlugins);
            this.tabControl1.Controls.Add(this.tabStartup);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(574, 249);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPlugins
            // 
            this.tabPlugins.Controls.Add(this.tabPluginsSubtabs);
            this.tabPlugins.Location = new System.Drawing.Point(4, 22);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlugins.Size = new System.Drawing.Size(566, 223);
            this.tabPlugins.TabIndex = 2;
            this.tabPlugins.Text = "Plug-ins";
            this.tabPlugins.UseVisualStyleBackColor = true;
            // 
            // tabPluginsSubtabs
            // 
            this.tabPluginsSubtabs.Controls.Add(this.tabHardwareSupport);
            this.tabPluginsSubtabs.Controls.Add(this.tabSimSupport);
            this.tabPluginsSubtabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPluginsSubtabs.Location = new System.Drawing.Point(3, 3);
            this.tabPluginsSubtabs.Name = "tabPluginsSubtabs";
            this.tabPluginsSubtabs.SelectedIndex = 0;
            this.tabPluginsSubtabs.Size = new System.Drawing.Size(560, 217);
            this.tabPluginsSubtabs.TabIndex = 0;
            // 
            // tabHardwareSupport
            // 
            this.tabHardwareSupport.AutoScroll = true;
            this.tabHardwareSupport.Controls.Add(this.hardwareSupportModuleList);
            this.tabHardwareSupport.Location = new System.Drawing.Point(4, 22);
            this.tabHardwareSupport.Name = "tabHardwareSupport";
            this.tabHardwareSupport.Padding = new System.Windows.Forms.Padding(3);
            this.tabHardwareSupport.Size = new System.Drawing.Size(552, 191);
            this.tabHardwareSupport.TabIndex = 0;
            this.tabHardwareSupport.Text = "Hardware Support Modules";
            this.tabHardwareSupport.UseVisualStyleBackColor = true;
            // 
            // hardwareSupportModuleList
            // 
            this.hardwareSupportModuleList.AutoSize = true;
            this.hardwareSupportModuleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hardwareSupportModuleList.HardwareSupportModules = null;
            this.hardwareSupportModuleList.Location = new System.Drawing.Point(3, 3);
            this.hardwareSupportModuleList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hardwareSupportModuleList.Name = "hardwareSupportModuleList";
            this.hardwareSupportModuleList.SimSupportModules = null;
            this.hardwareSupportModuleList.Size = new System.Drawing.Size(546, 185);
            this.hardwareSupportModuleList.TabIndex = 0;
            // 
            // tabSimSupport
            // 
            this.tabSimSupport.AutoScroll = true;
            this.tabSimSupport.Controls.Add(this.simSupportModuleList);
            this.tabSimSupport.Location = new System.Drawing.Point(4, 22);
            this.tabSimSupport.Name = "tabSimSupport";
            this.tabSimSupport.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimSupport.Size = new System.Drawing.Size(552, 191);
            this.tabSimSupport.TabIndex = 1;
            this.tabSimSupport.Text = "Sim Support Modules";
            this.tabSimSupport.UseVisualStyleBackColor = true;
            // 
            // simSupportModuleList
            // 
            this.simSupportModuleList.AutoSize = true;
            this.simSupportModuleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simSupportModuleList.HardwareSupportModules = null;
            this.simSupportModuleList.Location = new System.Drawing.Point(3, 3);
            this.simSupportModuleList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.simSupportModuleList.Name = "simSupportModuleList";
            this.simSupportModuleList.SimSupportModules = null;
            this.simSupportModuleList.Size = new System.Drawing.Size(546, 185);
            this.simSupportModuleList.TabIndex = 1;
            // 
            // tabStartup
            // 
            this.tabStartup.AutoScroll = true;
            this.tabStartup.Controls.Add(this.gbStartupOptions);
            this.tabStartup.Location = new System.Drawing.Point(4, 22);
            this.tabStartup.Name = "tabStartup";
            this.tabStartup.Padding = new System.Windows.Forms.Padding(3);
            this.tabStartup.Size = new System.Drawing.Size(566, 223);
            this.tabStartup.TabIndex = 1;
            this.tabStartup.Text = "Startup";
            this.tabStartup.UseVisualStyleBackColor = true;
            // 
            // gbStartupOptions
            // 
            this.gbStartupOptions.Controls.Add(this.label1);
            this.gbStartupOptions.Controls.Add(this.numFrequency);
            this.gbStartupOptions.Controls.Add(this.chkMinimizeWhenStarted);
            this.gbStartupOptions.Controls.Add(this.chkMinimizeToSystemTray);
            this.gbStartupOptions.Controls.Add(this.chkStartAutomaticallyWhenLaunched);
            this.gbStartupOptions.Controls.Add(this.chkLaunchAtSystemStartup);
            this.gbStartupOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStartupOptions.Location = new System.Drawing.Point(3, 3);
            this.gbStartupOptions.Name = "gbStartupOptions";
            this.gbStartupOptions.Size = new System.Drawing.Size(560, 217);
            this.gbStartupOptions.TabIndex = 5;
            this.gbStartupOptions.TabStop = false;
            this.gbStartupOptions.Text = "Startup Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desired Loop Frequency:";
            // 
            // numFrequency
            // 
            this.numFrequency.Location = new System.Drawing.Point(6, 137);
            this.numFrequency.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numFrequency.Name = "numFrequency";
            this.numFrequency.Size = new System.Drawing.Size(120, 20);
            this.numFrequency.TabIndex = 4;
            this.numFrequency.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // chkMinimizeWhenStarted
            // 
            this.chkMinimizeWhenStarted.AutoSize = true;
            this.chkMinimizeWhenStarted.Location = new System.Drawing.Point(6, 88);
            this.chkMinimizeWhenStarted.Name = "chkMinimizeWhenStarted";
            this.chkMinimizeWhenStarted.Size = new System.Drawing.Size(130, 17);
            this.chkMinimizeWhenStarted.TabIndex = 3;
            this.chkMinimizeWhenStarted.Text = "&Minimize when started";
            this.chkMinimizeWhenStarted.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToSystemTray
            // 
            this.chkMinimizeToSystemTray.AutoSize = true;
            this.chkMinimizeToSystemTray.Location = new System.Drawing.Point(6, 65);
            this.chkMinimizeToSystemTray.Name = "chkMinimizeToSystemTray";
            this.chkMinimizeToSystemTray.Size = new System.Drawing.Size(139, 17);
            this.chkMinimizeToSystemTray.TabIndex = 2;
            this.chkMinimizeToSystemTray.Text = "&Minimize to System Tray";
            this.chkMinimizeToSystemTray.UseVisualStyleBackColor = true;
            // 
            // chkStartAutomaticallyWhenLaunched
            // 
            this.chkStartAutomaticallyWhenLaunched.AutoSize = true;
            this.chkStartAutomaticallyWhenLaunched.Location = new System.Drawing.Point(6, 42);
            this.chkStartAutomaticallyWhenLaunched.Name = "chkStartAutomaticallyWhenLaunched";
            this.chkStartAutomaticallyWhenLaunched.Size = new System.Drawing.Size(188, 17);
            this.chkStartAutomaticallyWhenLaunched.TabIndex = 1;
            this.chkStartAutomaticallyWhenLaunched.Text = "&Start automatically when launched";
            this.chkStartAutomaticallyWhenLaunched.UseVisualStyleBackColor = true;
            // 
            // chkLaunchAtSystemStartup
            // 
            this.chkLaunchAtSystemStartup.AutoSize = true;
            this.chkLaunchAtSystemStartup.Location = new System.Drawing.Point(6, 19);
            this.chkLaunchAtSystemStartup.Name = "chkLaunchAtSystemStartup";
            this.chkLaunchAtSystemStartup.Size = new System.Drawing.Size(148, 17);
            this.chkLaunchAtSystemStartup.TabIndex = 0;
            this.chkLaunchAtSystemStartup.Text = "&Launch at System Startup";
            this.chkLaunchAtSystemStartup.UseVisualStyleBackColor = true;
            this.chkLaunchAtSystemStartup.CheckedChanged += new System.EventHandler(this.chkLaunchAtSystemStartup_CheckedChanged);
            // 
            // frmOptions
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(574, 283);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPlugins.ResumeLayout(false);
            this.tabPluginsSubtabs.ResumeLayout(false);
            this.tabHardwareSupport.ResumeLayout(false);
            this.tabHardwareSupport.PerformLayout();
            this.tabSimSupport.ResumeLayout(false);
            this.tabSimSupport.PerformLayout();
            this.tabStartup.ResumeLayout(false);
            this.gbStartupOptions.ResumeLayout(false);
            this.gbStartupOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button cmdOK;
        private Button cmdCancel;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage tabStartup;
        private TabPage tabPlugins;
        private TabControl tabPluginsSubtabs;
        private TabPage tabHardwareSupport;
        private TabPage tabSimSupport;
        private GroupBox gbStartupOptions;
        private CheckBox chkMinimizeWhenStarted;
        private CheckBox chkMinimizeToSystemTray;
        private CheckBox chkStartAutomaticallyWhenLaunched;
        private CheckBox chkLaunchAtSystemStartup;
        private ModuleList hardwareSupportModuleList;
        private ModuleList simSupportModuleList;
        private Label label1;
        private NumericUpDown numFrequency;
    }
}