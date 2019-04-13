namespace PhccHsm.ConfigManager.UI.Doa7SegConfig
{
    partial class Doa7SegmentModeSelector
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
            this.rbDigital = new System.Windows.Forms.RadioButton();
            this.rb7Segment = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gbconfig = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nbFirstPin = new System.Windows.Forms.NumericUpDown();
            this.nbSementCount = new System.Windows.Forms.NumericUpDown();
            this.gb14Segment = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numOutput = new System.Windows.Forms.NumericUpDown();
            this.rbReversed = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbconfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFirstPin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSementCount)).BeginInit();
            this.gb14Segment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // rbDigital
            // 
            this.rbDigital.AutoSize = true;
            this.rbDigital.Checked = true;
            this.rbDigital.Location = new System.Drawing.Point(51, 28);
            this.rbDigital.Name = "rbDigital";
            this.rbDigital.Size = new System.Drawing.Size(104, 17);
            this.rbDigital.TabIndex = 0;
            this.rbDigital.TabStop = true;
            this.rbDigital.Text = "Digital Out Mode";
            this.rbDigital.UseVisualStyleBackColor = true;
            this.rbDigital.CheckedChanged += new System.EventHandler(this.rbDigital_CheckedChanged);
            // 
            // rb7Segment
            // 
            this.rb7Segment.AutoSize = true;
            this.rb7Segment.Location = new System.Drawing.Point(51, 51);
            this.rb7Segment.Name = "rb7Segment";
            this.rb7Segment.Size = new System.Drawing.Size(145, 17);
            this.rb7Segment.TabIndex = 1;
            this.rb7Segment.Text = "Display 7 segments mode";
            this.rb7Segment.UseVisualStyleBackColor = true;
            this.rb7Segment.CheckedChanged += new System.EventHandler(this.rb7Segment_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mode Selection:";
            // 
            // gbconfig
            // 
            this.gbconfig.Controls.Add(this.gb14Segment);
            this.gbconfig.Controls.Add(this.label4);
            this.gbconfig.Controls.Add(this.label3);
            this.gbconfig.Controls.Add(this.label2);
            this.gbconfig.Controls.Add(this.nbFirstPin);
            this.gbconfig.Controls.Add(this.nbSementCount);
            this.gbconfig.Location = new System.Drawing.Point(13, 108);
            this.gbconfig.Name = "gbconfig";
            this.gbconfig.Size = new System.Drawing.Size(441, 251);
            this.gbconfig.TabIndex = 3;
            this.gbconfig.TabStop = false;
            this.gbconfig.Text = "Display Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(399, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "NOTE: this assumes that 7 segment displays were wired as per the documentation. ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port Number (1 to 32)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number Displays (7 segment modules)";
            // 
            // nbFirstPin
            // 
            this.nbFirstPin.Location = new System.Drawing.Point(22, 151);
            this.nbFirstPin.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nbFirstPin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbFirstPin.Name = "nbFirstPin";
            this.nbFirstPin.Size = new System.Drawing.Size(120, 20);
            this.nbFirstPin.TabIndex = 1;
            this.nbFirstPin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbFirstPin.ValueChanged += new System.EventHandler(this.nbFirstPin_ValueChanged);
            // 
            // nbSementCount
            // 
            this.nbSementCount.Location = new System.Drawing.Point(22, 71);
            this.nbSementCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nbSementCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbSementCount.Name = "nbSementCount";
            this.nbSementCount.Size = new System.Drawing.Size(120, 20);
            this.nbSementCount.TabIndex = 0;
            this.nbSementCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbSementCount.ValueChanged += new System.EventHandler(this.nbSementCount_ValueChanged);
            // 
            // gb14Segment
            // 
            this.gb14Segment.Controls.Add(this.label8);
            this.gb14Segment.Controls.Add(this.label7);
            this.gb14Segment.Controls.Add(this.label6);
            this.gb14Segment.Controls.Add(this.label5);
            this.gb14Segment.Controls.Add(this.numOutput);
            this.gb14Segment.Controls.Add(this.rbReversed);
            this.gb14Segment.Location = new System.Drawing.Point(0, 0);
            this.gb14Segment.Name = "gb14Segment";
            this.gb14Segment.Size = new System.Drawing.Size(441, 251);
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
            256,
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
            // Doa7SegmentModeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 450);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbconfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rb7Segment);
            this.Controls.Add(this.rbDigital);
            this.Name = "Doa7SegmentModeSelector";
            this.Text = "Doa7SegmentModeSelector";
            this.gbconfig.ResumeLayout(false);
            this.gbconfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFirstPin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSementCount)).EndInit();
            this.gb14Segment.ResumeLayout(false);
            this.gb14Segment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbDigital;
        private System.Windows.Forms.RadioButton rb7Segment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbconfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nbFirstPin;
        private System.Windows.Forms.NumericUpDown nbSementCount;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gb14Segment;
        private System.Windows.Forms.RadioButton rbReversed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numOutput;
    }
}