namespace Phcc.DeviceManager.UI
{
    partial class frmPhccDeviceManager
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhccDeviceManager));
            this.tvDevicesAndPeripherals = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesSetComPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDevicesAddMotherboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheral = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoa40Do = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoa7Seg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoa8Servo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoaAircore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoaAnOut1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesAddPeripheralDoaStepper = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDevicesCalibrate = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateServosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateAnalogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configure7SegmentDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureDOA40DOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDevicesRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContextSetCOMPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextAddMotherboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheral = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoa40do = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoaAircore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoaStepper = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoaAnOut1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoa7Seg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextAddPeripheralDoa8Servo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextCalibrate = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateServosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateAnalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configure7SegmentDisplayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configureDOA40DOToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSaveExcel = new System.Windows.Forms.SaveFileDialog();
            this.motherboardDigitalOutputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motherboardDigitalOutputsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalInputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalInputsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.ctxContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvDevicesAndPeripherals
            // 
            this.tvDevicesAndPeripherals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDevicesAndPeripherals.Location = new System.Drawing.Point(0, 24);
            this.tvDevicesAndPeripherals.Name = "tvDevicesAndPeripherals";
            this.tvDevicesAndPeripherals.Size = new System.Drawing.Size(656, 320);
            this.tvDevicesAndPeripherals.TabIndex = 1;
            this.tvDevicesAndPeripherals.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDevicesAndPeripherals_AfterSelect);
            this.tvDevicesAndPeripherals.Click += new System.EventHandler(this.tvDevicesAndPeripherals_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuDevices,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(656, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.toolStripMenuItem4,
            this.mnuFileOpen,
            this.toolStripMenuItem2,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Phcc.DeviceManager.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(249, 22);
            this.mnuFileNew.Text = "&New configuration file";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Phcc.DeviceManager.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(249, 22);
            this.mnuFileOpen.Text = "&Open configuration file...";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Phcc.DeviceManager.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(249, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(249, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(246, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuFileExit.Size = new System.Drawing.Size(249, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuDevices
            // 
            this.mnuDevices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDevicesSetComPort,
            this.toolStripMenuItem9,
            this.mnuDevicesAddMotherboard,
            this.mnuDevicesAddPeripheral,
            this.toolStripMenuItem10,
            this.mnuDevicesCalibrate,
            this.toolStripMenuItem7,
            this.mnuDevicesRemove});
            this.mnuDevices.Name = "mnuDevices";
            this.mnuDevices.Size = new System.Drawing.Size(59, 20);
            this.mnuDevices.Text = "&Devices";
            // 
            // mnuDevicesSetComPort
            // 
            this.mnuDevicesSetComPort.Name = "mnuDevicesSetComPort";
            this.mnuDevicesSetComPort.Size = new System.Drawing.Size(194, 22);
            this.mnuDevicesSetComPort.Text = "Set COM Port";
            this.mnuDevicesSetComPort.Click += new System.EventHandler(this.mnuDevicesSetComPort_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuDevicesAddMotherboard
            // 
            this.mnuDevicesAddMotherboard.Name = "mnuDevicesAddMotherboard";
            this.mnuDevicesAddMotherboard.Size = new System.Drawing.Size(194, 22);
            this.mnuDevicesAddMotherboard.Text = "Add &motherboard...";
            this.mnuDevicesAddMotherboard.Click += new System.EventHandler(this.mnuDevicesAddMotherboard_Click);
            // 
            // mnuDevicesAddPeripheral
            // 
            this.mnuDevicesAddPeripheral.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDevicesAddPeripheralDoa40Do,
            this.mnuDevicesAddPeripheralDoa7Seg,
            this.mnuDevicesAddPeripheralDoa8Servo,
            this.mnuDevicesAddPeripheralDoaAircore,
            this.mnuDevicesAddPeripheralDoaAnOut1,
            this.mnuDevicesAddPeripheralDoaStepper});
            this.mnuDevicesAddPeripheral.Name = "mnuDevicesAddPeripheral";
            this.mnuDevicesAddPeripheral.Size = new System.Drawing.Size(194, 22);
            this.mnuDevicesAddPeripheral.Text = "Add &peripheral";
            // 
            // mnuDevicesAddPeripheralDoa40Do
            // 
            this.mnuDevicesAddPeripheralDoa40Do.Name = "mnuDevicesAddPeripheralDoa40Do";
            this.mnuDevicesAddPeripheralDoa40Do.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoa40Do.Text = "DOA_40DO...";
            this.mnuDevicesAddPeripheralDoa40Do.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoa40Do_Click);
            // 
            // mnuDevicesAddPeripheralDoa7Seg
            // 
            this.mnuDevicesAddPeripheralDoa7Seg.Name = "mnuDevicesAddPeripheralDoa7Seg";
            this.mnuDevicesAddPeripheralDoa7Seg.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoa7Seg.Text = "DOA_7Seg";
            this.mnuDevicesAddPeripheralDoa7Seg.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoa7Seg_Click);
            // 
            // mnuDevicesAddPeripheralDoa8Servo
            // 
            this.mnuDevicesAddPeripheralDoa8Servo.Name = "mnuDevicesAddPeripheralDoa8Servo";
            this.mnuDevicesAddPeripheralDoa8Servo.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoa8Servo.Text = "DOA_8Servo...";
            this.mnuDevicesAddPeripheralDoa8Servo.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoa8Servo_Click);
            // 
            // mnuDevicesAddPeripheralDoaAircore
            // 
            this.mnuDevicesAddPeripheralDoaAircore.Name = "mnuDevicesAddPeripheralDoaAircore";
            this.mnuDevicesAddPeripheralDoaAircore.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoaAircore.Text = "DOA_Aircore...";
            this.mnuDevicesAddPeripheralDoaAircore.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoaAircore_Click);
            // 
            // mnuDevicesAddPeripheralDoaAnOut1
            // 
            this.mnuDevicesAddPeripheralDoaAnOut1.Name = "mnuDevicesAddPeripheralDoaAnOut1";
            this.mnuDevicesAddPeripheralDoaAnOut1.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoaAnOut1.Text = "DOA_AnOut1...";
            this.mnuDevicesAddPeripheralDoaAnOut1.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoaAnOut1_Click);
            // 
            // mnuDevicesAddPeripheralDoaStepper
            // 
            this.mnuDevicesAddPeripheralDoaStepper.Name = "mnuDevicesAddPeripheralDoaStepper";
            this.mnuDevicesAddPeripheralDoaStepper.Size = new System.Drawing.Size(154, 22);
            this.mnuDevicesAddPeripheralDoaStepper.Text = "DOA_Stepper...";
            this.mnuDevicesAddPeripheralDoaStepper.Click += new System.EventHandler(this.mnuDevicesAddPeripheralDoaStepper_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuDevicesCalibrate
            // 
            this.mnuDevicesCalibrate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motherboardDigitalOutputsToolStripMenuItem,
            this.calibrateServosToolStripMenuItem1,
            this.calibrateAnalogToolStripMenuItem1,
            this.configure7SegmentDisplayToolStripMenuItem,
            this.configureDOA40DOToolStripMenuItem});
            this.mnuDevicesCalibrate.Name = "mnuDevicesCalibrate";
            this.mnuDevicesCalibrate.Size = new System.Drawing.Size(194, 22);
            this.mnuDevicesCalibrate.Text = "&Calibrate / Configure...";
            this.mnuDevicesCalibrate.Click += new System.EventHandler(this.mnuDevicesCalibrate_Click);
            // 
            // calibrateServosToolStripMenuItem1
            // 
            this.calibrateServosToolStripMenuItem1.Name = "calibrateServosToolStripMenuItem1";
            this.calibrateServosToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.calibrateServosToolStripMenuItem1.Text = "Calibrate Servos";
            this.calibrateServosToolStripMenuItem1.Click += new System.EventHandler(this.calibrateServosToolStripMenuItem1_Click);
            // 
            // calibrateAnalogToolStripMenuItem1
            // 
            this.calibrateAnalogToolStripMenuItem1.Name = "calibrateAnalogToolStripMenuItem1";
            this.calibrateAnalogToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.calibrateAnalogToolStripMenuItem1.Text = "Calibrate Analog";
            this.calibrateAnalogToolStripMenuItem1.Click += new System.EventHandler(this.calibrateAnalogToolStripMenuItem1_Click);
            // 
            // configure7SegmentDisplayToolStripMenuItem
            // 
            this.configure7SegmentDisplayToolStripMenuItem.Name = "configure7SegmentDisplayToolStripMenuItem";
            this.configure7SegmentDisplayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configure7SegmentDisplayToolStripMenuItem.Text = "7SegmentDisplay";
            this.configure7SegmentDisplayToolStripMenuItem.Click += new System.EventHandler(this.configure7SegmentDisplayToolStripMenuItem_Click);
            // 
            // configureDOA40DOToolStripMenuItem
            // 
            this.configureDOA40DOToolStripMenuItem.Name = "configureDOA40DOToolStripMenuItem";
            this.configureDOA40DOToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configureDOA40DOToolStripMenuItem.Text = "&DOA 40 DO";
            this.configureDOA40DOToolStripMenuItem.Click += new System.EventHandler(this.configureDOA40DOToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuDevicesRemove
            // 
            this.mnuDevicesRemove.Image = global::Phcc.DeviceManager.Properties.Resources.Delete;
            this.mnuDevicesRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDevicesRemove.Name = "mnuDevicesRemove";
            this.mnuDevicesRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDevicesRemove.Size = new System.Drawing.Size(194, 22);
            this.mnuDevicesRemove.Text = "&Remove...";
            this.mnuDevicesRemove.Click += new System.EventHandler(this.mnuDevicesRemove_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToExcelToolStripMenuItem.Text = "Export to Excel";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // ctxContext
            // 
            this.ctxContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextSetCOMPort,
            this.toolStripMenuItem5,
            this.mnuContextAddMotherboard,
            this.mnuContextAddPeripheral,
            this.toolStripMenuItem8,
            this.mnuContextCalibrate,
            this.toolStripMenuItem6,
            this.mnuContextRemove});
            this.ctxContext.Name = "ctxMotherboard";
            this.ctxContext.Size = new System.Drawing.Size(195, 132);
            // 
            // mnuContextSetCOMPort
            // 
            this.mnuContextSetCOMPort.Name = "mnuContextSetCOMPort";
            this.mnuContextSetCOMPort.Size = new System.Drawing.Size(194, 22);
            this.mnuContextSetCOMPort.Text = "&Set COM Port...";
            this.mnuContextSetCOMPort.Click += new System.EventHandler(this.mnuContextSetComPort_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuContextAddMotherboard
            // 
            this.mnuContextAddMotherboard.Name = "mnuContextAddMotherboard";
            this.mnuContextAddMotherboard.Size = new System.Drawing.Size(194, 22);
            this.mnuContextAddMotherboard.Text = "Add &Motherboard...";
            this.mnuContextAddMotherboard.Click += new System.EventHandler(this.mnuContextAddMotherboard_Click);
            // 
            // mnuContextAddPeripheral
            // 
            this.mnuContextAddPeripheral.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextAddPeripheralDoa40do,
            this.mnuContextAddPeripheralDoaAircore,
            this.mnuContextAddPeripheralDoaStepper,
            this.mnuContextAddPeripheralDoaAnOut1,
            this.mnuContextAddPeripheralDoa7Seg,
            this.mnuContextAddPeripheralDoa8Servo});
            this.mnuContextAddPeripheral.Name = "mnuContextAddPeripheral";
            this.mnuContextAddPeripheral.Size = new System.Drawing.Size(194, 22);
            this.mnuContextAddPeripheral.Text = "Add &Peripheral...";
            // 
            // mnuContextAddPeripheralDoa40do
            // 
            this.mnuContextAddPeripheralDoa40do.Name = "mnuContextAddPeripheralDoa40do";
            this.mnuContextAddPeripheralDoa40do.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoa40do.Text = "DOA_40DO...";
            this.mnuContextAddPeripheralDoa40do.Click += new System.EventHandler(this.mnuContextAddPeripheralDoa40Do_Click);
            // 
            // mnuContextAddPeripheralDoaAircore
            // 
            this.mnuContextAddPeripheralDoaAircore.Name = "mnuContextAddPeripheralDoaAircore";
            this.mnuContextAddPeripheralDoaAircore.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoaAircore.Text = "DOA_Aircore...";
            this.mnuContextAddPeripheralDoaAircore.Click += new System.EventHandler(this.mnuContextAddPeripheralDoaAircore_Click);
            // 
            // mnuContextAddPeripheralDoaStepper
            // 
            this.mnuContextAddPeripheralDoaStepper.Name = "mnuContextAddPeripheralDoaStepper";
            this.mnuContextAddPeripheralDoaStepper.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoaStepper.Text = "DOA_Stepper...";
            this.mnuContextAddPeripheralDoaStepper.Click += new System.EventHandler(this.mnuContextAddPeripheralDoaStepper_Click);
            // 
            // mnuContextAddPeripheralDoaAnOut1
            // 
            this.mnuContextAddPeripheralDoaAnOut1.Name = "mnuContextAddPeripheralDoaAnOut1";
            this.mnuContextAddPeripheralDoaAnOut1.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoaAnOut1.Text = "DOA_AnOut1...";
            this.mnuContextAddPeripheralDoaAnOut1.Click += new System.EventHandler(this.mnuContextAddPeripheralDoaAnOut1_Click);
            // 
            // mnuContextAddPeripheralDoa7Seg
            // 
            this.mnuContextAddPeripheralDoa7Seg.Name = "mnuContextAddPeripheralDoa7Seg";
            this.mnuContextAddPeripheralDoa7Seg.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoa7Seg.Text = "DOA_7Seg";
            this.mnuContextAddPeripheralDoa7Seg.Click += new System.EventHandler(this.mnuContextAddPeripheralDoa7Seg_Click);
            // 
            // mnuContextAddPeripheralDoa8Servo
            // 
            this.mnuContextAddPeripheralDoa8Servo.Name = "mnuContextAddPeripheralDoa8Servo";
            this.mnuContextAddPeripheralDoa8Servo.Size = new System.Drawing.Size(154, 22);
            this.mnuContextAddPeripheralDoa8Servo.Text = "DOA_8Servo...";
            this.mnuContextAddPeripheralDoa8Servo.Click += new System.EventHandler(this.mnuContextAddPeripheralDoa8Servo_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuContextCalibrate
            // 
            this.mnuContextCalibrate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motherboardDigitalOutputsToolStripMenuItem1,
            this.calibrateServosToolStripMenuItem,
            this.calibrateAnalogToolStripMenuItem,
            this.configure7SegmentDisplayToolStripMenuItem1,
            this.configureDOA40DOToolStripMenuItem1});
            this.mnuContextCalibrate.Name = "mnuContextCalibrate";
            this.mnuContextCalibrate.Size = new System.Drawing.Size(194, 22);
            this.mnuContextCalibrate.Text = "&Calibrate / Configure...";
            this.mnuContextCalibrate.Click += new System.EventHandler(this.mnuContextCalibrate_Click);
            // 
            // calibrateServosToolStripMenuItem
            // 
            this.calibrateServosToolStripMenuItem.Name = "calibrateServosToolStripMenuItem";
            this.calibrateServosToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.calibrateServosToolStripMenuItem.Text = "Calibrate Servos";
            this.calibrateServosToolStripMenuItem.Click += new System.EventHandler(this.calibrateServosToolStripMenuItem_Click);
            // 
            // calibrateAnalogToolStripMenuItem
            // 
            this.calibrateAnalogToolStripMenuItem.Name = "calibrateAnalogToolStripMenuItem";
            this.calibrateAnalogToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.calibrateAnalogToolStripMenuItem.Text = "Calibrate Analog";
            this.calibrateAnalogToolStripMenuItem.Click += new System.EventHandler(this.calibrateAnalogToolStripMenuItem_Click);
            // 
            // configure7SegmentDisplayToolStripMenuItem1
            // 
            this.configure7SegmentDisplayToolStripMenuItem1.Name = "configure7SegmentDisplayToolStripMenuItem1";
            this.configure7SegmentDisplayToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.configure7SegmentDisplayToolStripMenuItem1.Text = "Configure 7SegmentDisplay";
            this.configure7SegmentDisplayToolStripMenuItem1.Click += new System.EventHandler(this.configure7SegmentDisplayToolStripMenuItem1_Click);
            // 
            // configureDOA40DOToolStripMenuItem1
            // 
            this.configureDOA40DOToolStripMenuItem1.Name = "configureDOA40DOToolStripMenuItem1";
            this.configureDOA40DOToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.configureDOA40DOToolStripMenuItem1.Text = "Configure DOA 40 DO";
            this.configureDOA40DOToolStripMenuItem1.Click += new System.EventHandler(this.configureDOA40DOToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuContextRemove
            // 
            this.mnuContextRemove.Image = global::Phcc.DeviceManager.Properties.Resources.Delete;
            this.mnuContextRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuContextRemove.Name = "mnuContextRemove";
            this.mnuContextRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuContextRemove.Size = new System.Drawing.Size(194, 22);
            this.mnuContextRemove.Text = "Remove...";
            this.mnuContextRemove.Click += new System.EventHandler(this.mnuContextRemove_Click);
            // 
            // dlgSaveExcel
            // 
            this.dlgSaveExcel.DefaultExt = "xlsx";
            // 
            // motherboardDigitalOutputsToolStripMenuItem
            // 
            this.motherboardDigitalOutputsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.digitalInputsToolStripMenuItem});
            this.motherboardDigitalOutputsToolStripMenuItem.Name = "motherboardDigitalOutputsToolStripMenuItem";
            this.motherboardDigitalOutputsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.motherboardDigitalOutputsToolStripMenuItem.Text = "Motherboard";
            // 
            // motherboardDigitalOutputsToolStripMenuItem1
            // 
            this.motherboardDigitalOutputsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.digitalInputsToolStripMenuItem1});
            this.motherboardDigitalOutputsToolStripMenuItem1.Name = "motherboardDigitalOutputsToolStripMenuItem1";
            this.motherboardDigitalOutputsToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.motherboardDigitalOutputsToolStripMenuItem1.Text = "Motherboard";
            // 
            // digitalInputsToolStripMenuItem
            // 
            this.digitalInputsToolStripMenuItem.Name = "digitalInputsToolStripMenuItem";
            this.digitalInputsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.digitalInputsToolStripMenuItem.Text = "Digital Inputs";
            // 
            // digitalInputsToolStripMenuItem1
            // 
            this.digitalInputsToolStripMenuItem1.Name = "digitalInputsToolStripMenuItem1";
            this.digitalInputsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.digitalInputsToolStripMenuItem1.Text = "Digital Inputs";
            // 
            // frmPhccDeviceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 344);
            this.Controls.Add(this.tvDevicesAndPeripherals);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPhccDeviceManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PHCC Device Manager";
            this.Load += new System.EventHandler(this.frmConfigureDevicesAndPeripherals_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ctxContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TreeView tvDevicesAndPeripherals;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextSetCOMPort;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheral;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoa40do;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoaAircore;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoaStepper;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoaAnOut1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoa7Seg;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddPeripheralDoa8Servo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuContextRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuDevices;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddMotherboard;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheral;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoa40Do;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoaAircore;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoaStepper;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoa7Seg;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoa8Servo;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesAddPeripheralDoaAnOut1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddMotherboard;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCalibrate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesSetComPort;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuDevicesCalibrate;
        private System.Windows.Forms.ToolStripMenuItem calibrateServosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrateAnalogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrateServosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem calibrateAnalogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configure7SegmentDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configure7SegmentDisplayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog dlgSaveExcel;
        private System.Windows.Forms.ToolStripMenuItem configureDOA40DOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureDOA40DOToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem motherboardDigitalOutputsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motherboardDigitalOutputsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem digitalInputsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem digitalInputsToolStripMenuItem1;
    }
}