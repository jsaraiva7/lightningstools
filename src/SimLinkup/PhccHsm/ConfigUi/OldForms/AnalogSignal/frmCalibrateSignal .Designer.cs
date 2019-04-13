namespace PhccHsm.ConfigUi.OldForms.AnalogSignal
{
    partial class frmCalibrateSignal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalibrateSignal));
            this.CalibrateServoWizard = new Common.UI.Wizard.Wizard();
            this.CalibrateServoWizardPage1 = new Common.UI.Wizard.WizardPage();
            this.lblMultiplier = new System.Windows.Forms.Label();
            this.inputValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.header1 = new Common.UI.Wizard.Header();
            this.nudCalibrateOffset = new System.Windows.Forms.NumericUpDown();
            this.lblCalibrateServoOffset = new System.Windows.Forms.Label();
            this.trkCalibrateServoOffset = new System.Windows.Forms.TrackBar();
            this.CalibrateServoWizardStartPage = new Common.UI.Wizard.WizardPage();
            this.infoPage1 = new Common.UI.Wizard.InfoPage();
            this.calibrateServoWizardPage2 = new Common.UI.Wizard.WizardPage();
            this.wizhdrCalibrateServoPage1 = new Common.UI.Wizard.Header();
            this.nudCalibrateServoGain = new System.Windows.Forms.NumericUpDown();
            this.lblCalibrateServoGain = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CalibrateServoWizard.SuspendLayout();
            this.CalibrateServoWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibrateOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkCalibrateServoOffset)).BeginInit();
            this.CalibrateServoWizardStartPage.SuspendLayout();
            this.calibrateServoWizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibrateServoGain)).BeginInit();
            this.SuspendLayout();
            // 
            // CalibrateServoWizard
            // 
            this.CalibrateServoWizard.Controls.Add(this.CalibrateServoWizardPage1);
            this.CalibrateServoWizard.Controls.Add(this.CalibrateServoWizardStartPage);
            this.CalibrateServoWizard.Controls.Add(this.calibrateServoWizardPage2);
            this.CalibrateServoWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalibrateServoWizard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalibrateServoWizard.Location = new System.Drawing.Point(0, 0);
            this.CalibrateServoWizard.Name = "CalibrateServoWizard";
            this.CalibrateServoWizard.Pages.AddRange(new Common.UI.Wizard.WizardPage[] {
            this.CalibrateServoWizardStartPage,
            this.CalibrateServoWizardPage1});
            this.CalibrateServoWizard.Size = new System.Drawing.Size(445, 281);
            this.CalibrateServoWizard.TabIndex = 0;
            this.CalibrateServoWizard.Load += new System.EventHandler(this.CalibrateServoWizard_Load);
            // 
            // CalibrateServoWizardPage1
            // 
            this.CalibrateServoWizardPage1.Controls.Add(this.label2);
            this.CalibrateServoWizardPage1.Controls.Add(this.lblMultiplier);
            this.CalibrateServoWizardPage1.Controls.Add(this.inputValue);
            this.CalibrateServoWizardPage1.Controls.Add(this.label1);
            this.CalibrateServoWizardPage1.Controls.Add(this.header1);
            this.CalibrateServoWizardPage1.Controls.Add(this.nudCalibrateOffset);
            this.CalibrateServoWizardPage1.Controls.Add(this.lblCalibrateServoOffset);
            this.CalibrateServoWizardPage1.Controls.Add(this.trkCalibrateServoOffset);
            this.CalibrateServoWizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalibrateServoWizardPage1.IsFinishPage = false;
            this.CalibrateServoWizardPage1.Location = new System.Drawing.Point(0, 0);
            this.CalibrateServoWizardPage1.Name = "CalibrateServoWizardPage1";
            this.CalibrateServoWizardPage1.Size = new System.Drawing.Size(445, 233);
            this.CalibrateServoWizardPage1.TabIndex = 2;
            // 
            // lblMultiplier
            // 
            this.lblMultiplier.AutoSize = true;
            this.lblMultiplier.Location = new System.Drawing.Point(257, 127);
            this.lblMultiplier.Name = "lblMultiplier";
            this.lblMultiplier.Size = new System.Drawing.Size(0, 13);
            this.lblMultiplier.TabIndex = 8;
            // 
            // inputValue
            // 
            this.inputValue.Location = new System.Drawing.Point(112, 92);
            this.inputValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.inputValue.Name = "inputValue";
            this.inputValue.Size = new System.Drawing.Size(67, 21);
            this.inputValue.TabIndex = 7;
            this.inputValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Input value:";
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "Adjust the calibration as desired. Input value is RAW value sent by the source si" +
    "gnal mapped on signal mapper. ";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image)(resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(445, 64);
            this.header1.TabIndex = 5;
            this.header1.Title = "Adjust Calibration Offset";
            // 
            // nudCalibrateOffset
            // 
            this.nudCalibrateOffset.Location = new System.Drawing.Point(112, 125);
            this.nudCalibrateOffset.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudCalibrateOffset.Name = "nudCalibrateOffset";
            this.nudCalibrateOffset.Size = new System.Drawing.Size(67, 21);
            this.nudCalibrateOffset.TabIndex = 4;
            this.nudCalibrateOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCalibrateOffset.ValueChanged += new System.EventHandler(this.nudCalibrateServoOffset_ValueChanged);
            // 
            // lblCalibrateServoOffset
            // 
            this.lblCalibrateServoOffset.AutoSize = true;
            this.lblCalibrateServoOffset.Location = new System.Drawing.Point(10, 127);
            this.lblCalibrateServoOffset.Name = "lblCalibrateServoOffset";
            this.lblCalibrateServoOffset.Size = new System.Drawing.Size(96, 13);
            this.lblCalibrateServoOffset.TabIndex = 3;
            this.lblCalibrateServoOffset.Text = "Calibration Offset:";
            // 
            // trkCalibrateServoOffset
            // 
            this.trkCalibrateServoOffset.Enabled = false;
            this.trkCalibrateServoOffset.LargeChange = 1024;
            this.trkCalibrateServoOffset.Location = new System.Drawing.Point(13, 185);
            this.trkCalibrateServoOffset.Maximum = 65535;
            this.trkCalibrateServoOffset.Name = "trkCalibrateServoOffset";
            this.trkCalibrateServoOffset.Size = new System.Drawing.Size(433, 45);
            this.trkCalibrateServoOffset.SmallChange = 256;
            this.trkCalibrateServoOffset.TabIndex = 1;
            this.trkCalibrateServoOffset.TickFrequency = 1024;
            this.trkCalibrateServoOffset.Scroll += new System.EventHandler(this.trkCalibrateServoOffset_Scroll);
            // 
            // CalibrateServoWizardStartPage
            // 
            this.CalibrateServoWizardStartPage.Controls.Add(this.infoPage1);
            this.CalibrateServoWizardStartPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalibrateServoWizardStartPage.IsFinishPage = false;
            this.CalibrateServoWizardStartPage.Location = new System.Drawing.Point(0, 0);
            this.CalibrateServoWizardStartPage.Name = "CalibrateServoWizardStartPage";
            this.CalibrateServoWizardStartPage.Size = new System.Drawing.Size(445, 233);
            this.CalibrateServoWizardStartPage.TabIndex = 1;
            // 
            // infoPage1
            // 
            this.infoPage1.BackColor = System.Drawing.Color.White;
            this.infoPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPage1.Image = null;
            this.infoPage1.Location = new System.Drawing.Point(0, 0);
            this.infoPage1.Name = "infoPage1";
            this.infoPage1.PageText = "This wizard enables you to calibrate  an PHCC analog output";
            this.infoPage1.PageTitle = "Welcome to the Calibration Wizard";
            this.infoPage1.Size = new System.Drawing.Size(445, 233);
            this.infoPage1.TabIndex = 0;
            // 
            // calibrateServoWizardPage2
            // 
            this.calibrateServoWizardPage2.Controls.Add(this.wizhdrCalibrateServoPage1);
            this.calibrateServoWizardPage2.Controls.Add(this.nudCalibrateServoGain);
            this.calibrateServoWizardPage2.Controls.Add(this.lblCalibrateServoGain);
            this.calibrateServoWizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibrateServoWizardPage2.IsFinishPage = false;
            this.calibrateServoWizardPage2.Location = new System.Drawing.Point(0, 0);
            this.calibrateServoWizardPage2.Name = "calibrateServoWizardPage2";
            this.calibrateServoWizardPage2.Size = new System.Drawing.Size(445, 281);
            this.calibrateServoWizardPage2.TabIndex = 3;
            // 
            // wizhdrCalibrateServoPage1
            // 
            this.wizhdrCalibrateServoPage1.BackColor = System.Drawing.SystemColors.Control;
            this.wizhdrCalibrateServoPage1.CausesValidation = false;
            this.wizhdrCalibrateServoPage1.Description = "Adjust the gain until the servo motor is at its maximum position.";
            this.wizhdrCalibrateServoPage1.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizhdrCalibrateServoPage1.Image = ((System.Drawing.Image)(resources.GetObject("wizhdrCalibrateServoPage1.Image")));
            this.wizhdrCalibrateServoPage1.Location = new System.Drawing.Point(0, 0);
            this.wizhdrCalibrateServoPage1.Name = "wizhdrCalibrateServoPage1";
            this.wizhdrCalibrateServoPage1.Size = new System.Drawing.Size(445, 64);
            this.wizhdrCalibrateServoPage1.TabIndex = 9;
            this.wizhdrCalibrateServoPage1.Title = "Adjust Gain";
            // 
            // nudCalibrateServoGain
            // 
            this.nudCalibrateServoGain.Location = new System.Drawing.Point(50, 70);
            this.nudCalibrateServoGain.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCalibrateServoGain.Name = "nudCalibrateServoGain";
            this.nudCalibrateServoGain.Size = new System.Drawing.Size(49, 21);
            this.nudCalibrateServoGain.TabIndex = 8;
            this.nudCalibrateServoGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCalibrateServoGain
            // 
            this.lblCalibrateServoGain.AutoSize = true;
            this.lblCalibrateServoGain.Location = new System.Drawing.Point(12, 72);
            this.lblCalibrateServoGain.Name = "lblCalibrateServoGain";
            this.lblCalibrateServoGain.Size = new System.Drawing.Size(32, 13);
            this.lblCalibrateServoGain.TabIndex = 7;
            this.lblCalibrateServoGain.Text = "Gain:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Position:";
            // 
            // frmCalibrateSignal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 281);
            this.Controls.Add(this.CalibrateServoWizard);
            this.Name = "frmCalibrateSignal";
            this.Text = "Calibrate Analog Signal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalibrateSignal_FormClosing);
            this.CalibrateServoWizard.ResumeLayout(false);
            this.CalibrateServoWizardPage1.ResumeLayout(false);
            this.CalibrateServoWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibrateOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkCalibrateServoOffset)).EndInit();
            this.CalibrateServoWizardStartPage.ResumeLayout(false);
            this.calibrateServoWizardPage2.ResumeLayout(false);
            this.calibrateServoWizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibrateServoGain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Common.UI.Wizard.Wizard CalibrateServoWizard;
        private global::Common.UI.Wizard.WizardPage CalibrateServoWizardStartPage;
        private global::Common.UI.Wizard.WizardPage CalibrateServoWizardPage1;
        private global::Common.UI.Wizard.InfoPage infoPage1;
        private System.Windows.Forms.NumericUpDown nudCalibrateOffset;
        private System.Windows.Forms.Label lblCalibrateServoOffset;
        private System.Windows.Forms.TrackBar trkCalibrateServoOffset;
        private Common.UI.Wizard.Header header1;
        private Common.UI.Wizard.WizardPage calibrateServoWizardPage2;
        private Common.UI.Wizard.Header wizhdrCalibrateServoPage1;
        private System.Windows.Forms.NumericUpDown nudCalibrateServoGain;
        private System.Windows.Forms.Label lblCalibrateServoGain;
        private System.Windows.Forms.NumericUpDown inputValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMultiplier;
        private System.Windows.Forms.Label label2;
    }
}