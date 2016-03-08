namespace evolutionSoccer
{
    partial class GraphicsForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.graphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearGraphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamStrengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.winsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(959, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // graphsToolStripMenuItem
            // 
            this.graphsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearGraphsToolStripMenuItem,
            this.teamStrengthToolStripMenuItem,
            this.winsToolStripMenuItem,
            this.drawsToolStripMenuItem});
            this.graphsToolStripMenuItem.Name = "graphsToolStripMenuItem";
            this.graphsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.graphsToolStripMenuItem.Text = "Graphs";
            // 
            // clearGraphsToolStripMenuItem
            // 
            this.clearGraphsToolStripMenuItem.Name = "clearGraphsToolStripMenuItem";
            this.clearGraphsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearGraphsToolStripMenuItem.Text = "Clear graphs";
            this.clearGraphsToolStripMenuItem.Click += new System.EventHandler(this.clearGraphsToolStripMenuItem_Click);
            // 
            // teamStrengthToolStripMenuItem
            // 
            this.teamStrengthToolStripMenuItem.Name = "teamStrengthToolStripMenuItem";
            this.teamStrengthToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.teamStrengthToolStripMenuItem.Text = "Team strength";
            this.teamStrengthToolStripMenuItem.Click += new System.EventHandler(this.teamStrengthToolStripMenuItem_Click);
            // 
            // winsToolStripMenuItem
            // 
            this.winsToolStripMenuItem.Name = "winsToolStripMenuItem";
            this.winsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.winsToolStripMenuItem.Text = "Wins";
            this.winsToolStripMenuItem.Click += new System.EventHandler(this.winsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGuideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openGuideToolStripMenuItem
            // 
            this.openGuideToolStripMenuItem.Name = "openGuideToolStripMenuItem";
            this.openGuideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openGuideToolStripMenuItem.Text = "Open guide";
            this.openGuideToolStripMenuItem.Click += new System.EventHandler(this.openGuideToolStripMenuItem_Click);
            // 
            // drawsToolStripMenuItem
            // 
            this.drawsToolStripMenuItem.Name = "drawsToolStripMenuItem";
            this.drawsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.drawsToolStripMenuItem.Text = "Draws";
            this.drawsToolStripMenuItem.Click += new System.EventHandler(this.drawsToolStripMenuItem_Click);
            // 
            // GraphicsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(959, 517);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "GraphicsForm";
            this.Text = "Graphs Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem graphsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGraphsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teamStrengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem winsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawsToolStripMenuItem;
    }
}