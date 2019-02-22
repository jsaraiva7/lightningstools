using System.ComponentModel;
using System.Windows.Forms;

namespace SimLinkup.UI.UserControls
{
    partial class SignalsView
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

        #region Component Designer generated code

 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvSignals = new System.Windows.Forms.TreeView();
            this.lvSignals = new System.Windows.Forms.ListView();
            this.Source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbVisualization = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVisualization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvSignals
            // 
            this.tvSignals.FullRowSelect = true;
            this.tvSignals.Location = new System.Drawing.Point(3, 3);
            this.tvSignals.Name = "tvSignals";
            this.tvSignals.Size = new System.Drawing.Size(399, 594);
            this.tvSignals.TabIndex = 3;
            this.tvSignals.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSignals_AfterSelect);
            // 
            // lvSignals
            // 
            this.lvSignals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Source});
            this.lvSignals.FullRowSelect = true;
            this.lvSignals.LabelWrap = false;
            this.lvSignals.Location = new System.Drawing.Point(405, 3);
            this.lvSignals.MultiSelect = false;
            this.lvSignals.Name = "lvSignals";
            this.lvSignals.Size = new System.Drawing.Size(405, 594);
            this.lvSignals.TabIndex = 2;
            this.lvSignals.UseCompatibleStateImageBehavior = false;
            this.lvSignals.View = System.Windows.Forms.View.Details;
            this.lvSignals.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSignals_ColumnClick);
            // 
            // pbVisualization
            // 
            this.pbVisualization.BackColor = System.Drawing.SystemColors.Control;
            this.pbVisualization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVisualization.Location = new System.Drawing.Point(814, 3);
            this.pbVisualization.Margin = new System.Windows.Forms.Padding(2);
            this.pbVisualization.Name = "pbVisualization";
            this.pbVisualization.Size = new System.Drawing.Size(403, 391);
            this.pbVisualization.TabIndex = 0;
            this.pbVisualization.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(874, 476);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(295, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(967, 417);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SignalsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pbVisualization);
            this.Controls.Add(this.lvSignals);
            this.Controls.Add(this.tvSignals);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "SignalsView";
            this.Size = new System.Drawing.Size(1222, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pbVisualization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView tvSignals;
        private ListView lvSignals;
        private ColumnHeader Source;
        private PictureBox pbVisualization;
        private TrackBar trackBar1;
        private Button button1;
    }
}
