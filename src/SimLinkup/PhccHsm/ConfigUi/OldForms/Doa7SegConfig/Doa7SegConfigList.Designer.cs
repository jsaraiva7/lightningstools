namespace PhccHsm.ConfigUi.OldForms.Doa7SegConfig
{
    partial class Doa7SegConfigList
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
            this.dataViewOutput = new System.Windows.Forms.DataGridView();
            this.btnAddMode = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // dataViewOutput
            // 
            this.dataViewOutput.AllowUserToAddRows = false;
            this.dataViewOutput.AllowUserToDeleteRows = false;
            this.dataViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataViewOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataViewOutput.Location = new System.Drawing.Point(-8, 1);
            this.dataViewOutput.Name = "dataViewOutput";
            this.dataViewOutput.Size = new System.Drawing.Size(796, 384);
            this.dataViewOutput.TabIndex = 0;
            this.dataViewOutput.SelectionChanged += new System.EventHandler(this.dataViewOutput_SelectionChanged);
            // 
            // btnAddMode
            // 
            this.btnAddMode.Location = new System.Drawing.Point(514, 415);
            this.btnAddMode.Name = "btnAddMode";
            this.btnAddMode.Size = new System.Drawing.Size(134, 23);
            this.btnAddMode.TabIndex = 1;
            this.btnAddMode.Text = "Add Mode";
            this.btnAddMode.UseVisualStyleBackColor = true;
            this.btnAddMode.Click += new System.EventHandler(this.btnAddMode_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(654, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove Selected";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 415);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(134, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Doa7SegConfigList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAddMode);
            this.Controls.Add(this.dataViewOutput);
            this.Name = "Doa7SegConfigList";
            this.Text = "DOA 7 Segment Output Mode Configuration (defaults to Digital Out Unless Configure" +
    "d Here) ";
            this.Load += new System.EventHandler(this.Doa7SegConfigList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataViewOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataViewOutput;
        private System.Windows.Forms.Button btnAddMode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOk;
    }
}