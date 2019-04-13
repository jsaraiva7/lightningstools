namespace PhccHsm.ConfigManager.UI
{
    partial class CalibrationList
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteCalibrationPoint = new System.Windows.Forms.Button();
            this.btnAddCalibrationPoint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnHomingSignal = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHomeInSignal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 409);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeleteCalibrationPoint
            // 
            this.btnDeleteCalibrationPoint.Location = new System.Drawing.Point(660, 409);
            this.btnDeleteCalibrationPoint.Name = "btnDeleteCalibrationPoint";
            this.btnDeleteCalibrationPoint.Size = new System.Drawing.Size(112, 23);
            this.btnDeleteCalibrationPoint.TabIndex = 3;
            this.btnDeleteCalibrationPoint.Text = "Remove Selected";
            this.btnDeleteCalibrationPoint.UseVisualStyleBackColor = true;
            this.btnDeleteCalibrationPoint.Click += new System.EventHandler(this.btnDeleteCalibrationPoint_Click);
            // 
            // btnAddCalibrationPoint
            // 
            this.btnAddCalibrationPoint.Location = new System.Drawing.Point(542, 409);
            this.btnAddCalibrationPoint.Name = "btnAddCalibrationPoint";
            this.btnAddCalibrationPoint.Size = new System.Drawing.Size(112, 23);
            this.btnAddCalibrationPoint.TabIndex = 2;
            this.btnAddCalibrationPoint.Text = "Add calibration Point";
            this.btnAddCalibrationPoint.UseVisualStyleBackColor = true;
            this.btnAddCalibrationPoint.Click += new System.EventHandler(this.btnAddCalibrationPoint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = " ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 375);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnHomingSignal
            // 
            this.btnHomingSignal.Location = new System.Drawing.Point(266, 409);
            this.btnHomingSignal.Name = "btnHomingSignal";
            this.btnHomingSignal.Size = new System.Drawing.Size(127, 23);
            this.btnHomingSignal.TabIndex = 7;
            this.btnHomingSignal.Text = "Select Home_In Signal";
            this.btnHomingSignal.UseVisualStyleBackColor = true;
            this.btnHomingSignal.Click += new System.EventHandler(this.btnHomingSignal_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(399, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Delete Home_In Signal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Home In Signal:";
            // 
            // lblHomeInSignal
            // 
            this.lblHomeInSignal.AutoSize = true;
            this.lblHomeInSignal.Location = new System.Drawing.Point(354, 442);
            this.lblHomeInSignal.Name = "lblHomeInSignal";
            this.lblHomeInSignal.Size = new System.Drawing.Size(33, 13);
            this.lblHomeInSignal.TabIndex = 10;
            this.lblHomeInSignal.Text = "None";
            // 
            // CalibrationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 467);
            this.Controls.Add(this.lblHomeInSignal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnHomingSignal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteCalibrationPoint);
            this.Controls.Add(this.btnAddCalibrationPoint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "CalibrationList";
            this.Text = "Calibration List for ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteCalibrationPoint;
        private System.Windows.Forms.Button btnAddCalibrationPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnHomingSignal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHomeInSignal;
    }
}