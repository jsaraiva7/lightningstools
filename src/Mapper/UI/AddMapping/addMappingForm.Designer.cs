namespace Mapper.UI.AddMapping
{
    partial class addMappingForm
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
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblIn = new System.Windows.Forms.Label();
            this.lblOut = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(317, 97);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(250, 23);
            this.btnOutput.TabIndex = 1;
            this.btnOutput.Text = "Output Signal";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(184, 144);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(42, 97);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(225, 23);
            this.btnInput.TabIndex = 4;
            this.btnInput.Text = "Input Signal";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(317, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblIn
            // 
            this.lblIn.AutoSize = true;
            this.lblIn.Location = new System.Drawing.Point(39, 44);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(97, 13);
            this.lblIn.TabIndex = 6;
            this.lblIn.Text = "Please select Input";
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(314, 70);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(107, 13);
            this.lblOut.TabIndex = 7;
            this.lblOut.Text = "Please Select Output";
            // 
            // addMappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 307);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.lblIn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnOutput);
            this.Name = "addMappingForm";
            this.Text = "addMappingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblIn;
        private System.Windows.Forms.Label lblOut;
    }
}