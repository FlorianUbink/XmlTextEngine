namespace XmlFormEngine
{
    partial class MainMenu
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
            this.Game_Button = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.LoadGame_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Game_Button
            // 
            this.Game_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_Button.Location = new System.Drawing.Point(35, 145);
            this.Game_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Game_Button.Name = "Game_Button";
            this.Game_Button.Size = new System.Drawing.Size(320, 62);
            this.Game_Button.TabIndex = 0;
            this.Game_Button.Text = "New Game";
            this.Game_Button.UseVisualStyleBackColor = true;
            this.Game_Button.Click += new System.EventHandler(this.Game_Button_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(23, 25);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(323, 67);
            this.Title.TabIndex = 1;
            this.Title.Text = "Main Menu";
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Version.Location = new System.Drawing.Point(300, 396);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(49, 16);
            this.Label_Version.TabIndex = 2;
            this.Label_Version.Text = "Version";
            // 
            // LoadGame_Button
            // 
            this.LoadGame_Button.Enabled = false;
            this.LoadGame_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadGame_Button.Location = new System.Drawing.Point(35, 274);
            this.LoadGame_Button.Margin = new System.Windows.Forms.Padding(4);
            this.LoadGame_Button.Name = "LoadGame_Button";
            this.LoadGame_Button.Size = new System.Drawing.Size(320, 62);
            this.LoadGame_Button.TabIndex = 3;
            this.LoadGame_Button.Text = "Load Game";
            this.LoadGame_Button.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 434);
            this.Controls.Add(this.LoadGame_Button);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Game_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Engine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Game_Button;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Button LoadGame_Button;
    }
}