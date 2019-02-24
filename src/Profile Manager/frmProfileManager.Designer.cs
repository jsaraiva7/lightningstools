namespace Profile_Manager
{
    partial class frmProfileManager
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moduleManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.moduleManagementToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProfileToolStripMenuItem,
            this.saveProfileToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openProfileToolStripMenuItem
            // 
            this.openProfileToolStripMenuItem.Name = "openProfileToolStripMenuItem";
            this.openProfileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openProfileToolStripMenuItem.Text = "Open Profile";
            this.openProfileToolStripMenuItem.Click += new System.EventHandler(this.openProfileToolStripMenuItem_Click);
            // 
            // saveProfileToolStripMenuItem
            // 
            this.saveProfileToolStripMenuItem.Name = "saveProfileToolStripMenuItem";
            this.saveProfileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveProfileToolStripMenuItem.Text = "Save Profile";
            this.saveProfileToolStripMenuItem.Click += new System.EventHandler(this.saveProfileToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // moduleManagementToolStripMenuItem
            // 
            this.moduleManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addModuleToolStripMenuItem,
            this.removeModuleToolStripMenuItem});
            this.moduleManagementToolStripMenuItem.Name = "moduleManagementToolStripMenuItem";
            this.moduleManagementToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.moduleManagementToolStripMenuItem.Text = "Module Management";
            // 
            // addModuleToolStripMenuItem
            // 
            this.addModuleToolStripMenuItem.Name = "addModuleToolStripMenuItem";
            this.addModuleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addModuleToolStripMenuItem.Text = "Add Module";
            this.addModuleToolStripMenuItem.Click += new System.EventHandler(this.addModuleToolStripMenuItem_Click);
            // 
            // removeModuleToolStripMenuItem
            // 
            this.removeModuleToolStripMenuItem.Name = "removeModuleToolStripMenuItem";
            this.removeModuleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeModuleToolStripMenuItem.Text = "Remove Module";
            this.removeModuleToolStripMenuItem.Visible = false;
            this.removeModuleToolStripMenuItem.Click += new System.EventHandler(this.removeModuleToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 422);
            this.dataGridView1.TabIndex = 1;
            // 
            // frmProfileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmProfileManager";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moduleManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeModuleToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

