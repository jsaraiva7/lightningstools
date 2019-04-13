namespace PhccSupportModule.ConfigUi.OldForms.DigitalConfig
{
    partial class DigitalModeSelector
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.gb14Segment = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numOutput = new System.Windows.Forms.NumericUpDown();
            this.rbReversed = new System.Windows.Forms.RadioButton();
            this.gb14Segment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(35, 403);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gb14Segment
            // 
            this.gb14Segment.Controls.Add(this.label8);
            this.gb14Segment.Controls.Add(this.label7);
            this.gb14Segment.Controls.Add(this.label6);
            this.gb14Segment.Controls.Add(this.label5);
            this.gb14Segment.Controls.Add(this.numOutput);
            this.gb14Segment.Controls.Add(this.rbReversed);
            this.gb14Segment.Location = new System.Drawing.Point(12, 25);
            this.gb14Segment.Name = "gb14Segment";
            this.gb14Segment.Size = new System.Drawing.Size(441, 359);
            this.gb14Segment.TabIndex = 6;
            this.gb14Segment.TabStop = false;
            this.gb14Segment.Text = "Output Configuration";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Output Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(412, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Then, click on add. Repeat the process for remaining outputs that need to be inve" +
    "rted";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(335, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Write the output number, and click on the checkbox to invert the pin. ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Please use this screen to invert any digital output.";
            // 
            // numOutput
            // 
            this.numOutput.Location = new System.Drawing.Point(63, 151);
            this.numOutput.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.numOutput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOutput.Name = "numOutput";
            this.numOutput.Size = new System.Drawing.Size(120, 20);
            this.numOutput.TabIndex = 1;
            this.numOutput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbReversed
            // 
            this.rbReversed.AutoSize = true;
            this.rbReversed.Location = new System.Drawing.Point(258, 154);
            this.rbReversed.Name = "rbReversed";
            this.rbReversed.Size = new System.Drawing.Size(77, 17);
            this.rbReversed.TabIndex = 0;
            this.rbReversed.TabStop = true;
            this.rbReversed.Text = "Reversed?";
            this.rbReversed.UseVisualStyleBackColor = true;
            // 
            // DigitalModeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 450);
            this.Controls.Add(this.gb14Segment);
            this.Controls.Add(this.btnAdd);
            this.Name = "DigitalModeSelector";
            this.Text = "Doa7SegmentModeSelector";
            this.gb14Segment.ResumeLayout(false);
            this.gb14Segment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gb14Segment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numOutput;
        private System.Windows.Forms.RadioButton rbReversed;
    }
}