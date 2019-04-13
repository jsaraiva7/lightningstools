namespace PhccHsm.ConfigManager.UI
{
    partial class frmPromptForPeripheralAddress
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtBaseAddress = new System.Windows.Forms.TextBox();
            this.lblDeviceAddress = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblFriendlyName = new System.Windows.Forms.Label();
            this.tbFriendlyName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(81, 106);
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
            this.cmdCancel.Location = new System.Drawing.Point(162, 106);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Location = new System.Drawing.Point(117, 58);
            this.txtBaseAddress.MaxLength = 25;
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Size = new System.Drawing.Size(181, 20);
            this.txtBaseAddress.TabIndex = 2;
            this.txtBaseAddress.TextChanged += new System.EventHandler(this.txtBaseAddress_TextChanged);
            // 
            // lblDeviceAddress
            // 
            this.lblDeviceAddress.AutoSize = true;
            this.lblDeviceAddress.Location = new System.Drawing.Point(12, 61);
            this.lblDeviceAddress.Name = "lblDeviceAddress";
            this.lblDeviceAddress.Size = new System.Drawing.Size(98, 13);
            this.lblDeviceAddress.TabIndex = 3;
            this.lblDeviceAddress.Text = "Peripheral &Address:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblFriendlyName
            // 
            this.lblFriendlyName.AutoSize = true;
            this.lblFriendlyName.Location = new System.Drawing.Point(12, 21);
            this.lblFriendlyName.Name = "lblFriendlyName";
            this.lblFriendlyName.Size = new System.Drawing.Size(77, 13);
            this.lblFriendlyName.TabIndex = 5;
            this.lblFriendlyName.Text = "Friendly &Name:";
            // 
            // tbFriendlyName
            // 
            this.tbFriendlyName.Location = new System.Drawing.Point(119, 18);
            this.tbFriendlyName.MaxLength = 25;
            this.tbFriendlyName.Name = "tbFriendlyName";
            this.tbFriendlyName.Size = new System.Drawing.Size(181, 20);
            this.tbFriendlyName.TabIndex = 4;
            // 
            // frmPromptForPeripheralAddress
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(310, 141);
            this.ControlBox = false;
            this.Controls.Add(this.lblFriendlyName);
            this.Controls.Add(this.tbFriendlyName);
            this.Controls.Add(this.lblDeviceAddress);
            this.Controls.Add(this.txtBaseAddress);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPromptForPeripheralAddress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Peripheral Address";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtBaseAddress;
        private System.Windows.Forms.Label lblDeviceAddress;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblFriendlyName;
        private System.Windows.Forms.TextBox tbFriendlyName;
    }
}