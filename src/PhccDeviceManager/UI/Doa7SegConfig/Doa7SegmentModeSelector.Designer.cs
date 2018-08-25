namespace Phcc.DeviceManager.UI.Doa7SegConfig
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.gb14Segment = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nbFirsPin14Segment = new System.Windows.Forms.NumericUpDown();
            this.nb14Segments = new System.Windows.Forms.NumericUpDown();
            this.rb14Segments = new System.Windows.Forms.RadioButton();
            this.gbconfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFirstPin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbSementCount)).BeginInit();
            this.gb14Segment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFirsPin14Segment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb14Segments)).BeginInit();
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
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "First Pin on the first display:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of individual 7 segment displays:";
            // 
            // nbFirstPin
            // 
            this.nbFirstPin.Location = new System.Drawing.Point(22, 151);
            this.nbFirstPin.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nbFirstPin.Name = "nbFirstPin";
            this.nbFirstPin.Size = new System.Drawing.Size(120, 20);
            this.nbFirstPin.TabIndex = 1;
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
            this.gb14Segment.Controls.Add(this.label5);
            this.gb14Segment.Controls.Add(this.label6);
            this.gb14Segment.Controls.Add(this.label7);
            this.gb14Segment.Controls.Add(this.nbFirsPin14Segment);
            this.gb14Segment.Controls.Add(this.nb14Segments);
            this.gb14Segment.Location = new System.Drawing.Point(12, 108);
            this.gb14Segment.Name = "gb14Segment";
            this.gb14Segment.Size = new System.Drawing.Size(441, 251);
            this.gb14Segment.TabIndex = 6;
            this.gb14Segment.TabStop = false;
            this.gb14Segment.Text = "Display Configuration";
            this.gb14Segment.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(405, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "NOTE: this assumes that 14 segment displays were wired as per the documentation. " +
    "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "First Pin on the first display:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(204, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Number of individual 14 segment displays:";
            // 
            // nbFirsPin14Segment
            // 
            this.nbFirsPin14Segment.Location = new System.Drawing.Point(22, 151);
            this.nbFirsPin14Segment.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nbFirsPin14Segment.Name = "nbFirsPin14Segment";
            this.nbFirsPin14Segment.Size = new System.Drawing.Size(120, 20);
            this.nbFirsPin14Segment.TabIndex = 1;
            this.nbFirsPin14Segment.ValueChanged += new System.EventHandler(this.nbFirsPin14Segment_ValueChanged);
            // 
            // nb14Segments
            // 
            this.nb14Segments.Location = new System.Drawing.Point(22, 71);
            this.nb14Segments.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.nb14Segments.Name = "nb14Segments";
            this.nb14Segments.Size = new System.Drawing.Size(120, 20);
            this.nb14Segments.TabIndex = 0;
            this.nb14Segments.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nb14Segments.ValueChanged += new System.EventHandler(this.nb14Segments_ValueChanged);
            // 
            // rb14Segments
            // 
            this.rb14Segments.AutoSize = true;
            this.rb14Segments.Location = new System.Drawing.Point(51, 74);
            this.rb14Segments.Name = "rb14Segments";
            this.rb14Segments.Size = new System.Drawing.Size(151, 17);
            this.rb14Segments.TabIndex = 7;
            this.rb14Segments.Text = "Display 14 segments mode";
            this.rb14Segments.UseVisualStyleBackColor = true;
            this.rb14Segments.CheckedChanged += new System.EventHandler(this.rb14Segments_CheckedChanged);
            // 
            // Doa7SegmentModeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 450);
            this.Controls.Add(this.rb14Segments);
            this.Controls.Add(this.gb14Segment);
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
            ((System.ComponentModel.ISupportInitialize)(this.nbFirsPin14Segment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb14Segments)).EndInit();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nbFirsPin14Segment;
        private System.Windows.Forms.NumericUpDown nb14Segments;
        private System.Windows.Forms.RadioButton rb14Segments;
    }
}