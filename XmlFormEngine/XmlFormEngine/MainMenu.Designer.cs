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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.Label_Version = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Main = new System.Windows.Forms.Panel();
            this.Settings_Button = new System.Windows.Forms.Button();
            this.LoadGame_Button = new System.Windows.Forms.Button();
            this.Game_Button = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Panel();
            this.OpenDialoge = new System.Windows.Forms.Button();
            this.savefiledir = new System.Windows.Forms.Label();
            this.Main.SuspendLayout();
            this.settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Version.Location = new System.Drawing.Point(230, 330);
            this.Label_Version.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(41, 14);
            this.Label_Version.TabIndex = 2;
            this.Label_Version.Text = "Version";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(62, 92);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(158, 175);
            this.listBox1.TabIndex = 6;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // Main
            // 
            this.Main.Controls.Add(this.Settings_Button);
            this.Main.Controls.Add(this.LoadGame_Button);
            this.Main.Controls.Add(this.Game_Button);
            this.Main.Location = new System.Drawing.Point(20, 66);
            this.Main.Name = "Main";
            this.Main.Size = new System.Drawing.Size(265, 260);
            this.Main.TabIndex = 4;
            this.Main.TabStop = true;
            // 
            // Settings_Button
            // 
            this.Settings_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings_Button.Location = new System.Drawing.Point(12, 191);
            this.Settings_Button.Name = "Settings_Button";
            this.Settings_Button.Size = new System.Drawing.Size(240, 50);
            this.Settings_Button.TabIndex = 4;
            this.Settings_Button.Text = "Settings";
            this.Settings_Button.UseVisualStyleBackColor = true;
            this.Settings_Button.Click += new System.EventHandler(this.Settings_Button_Click);
            // 
            // LoadGame_Button
            // 
            this.LoadGame_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadGame_Button.Location = new System.Drawing.Point(11, 116);
            this.LoadGame_Button.Name = "LoadGame_Button";
            this.LoadGame_Button.Size = new System.Drawing.Size(240, 50);
            this.LoadGame_Button.TabIndex = 3;
            this.LoadGame_Button.Text = "Load Game";
            this.LoadGame_Button.UseVisualStyleBackColor = true;
            this.LoadGame_Button.Click += new System.EventHandler(this.LoadGame_Button_Click);
            // 
            // Game_Button
            // 
            this.Game_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_Button.Location = new System.Drawing.Point(12, 48);
            this.Game_Button.Name = "Game_Button";
            this.Game_Button.Size = new System.Drawing.Size(240, 50);
            this.Game_Button.TabIndex = 0;
            this.Game_Button.Text = "New Game";
            this.Game_Button.UseVisualStyleBackColor = true;
            this.Game_Button.Click += new System.EventHandler(this.Game_Button_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(11, 9);
            this.Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(260, 54);
            this.Title.TabIndex = 1;
            this.Title.Text = "Main Menu";
            // 
            // ReturnButton
            // 
            this.ReturnButton.Enabled = false;
            this.ReturnButton.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnButton.Location = new System.Drawing.Point(20, 313);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(75, 23);
            this.ReturnButton.TabIndex = 7;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Visible = false;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // settings
            // 
            this.settings.Controls.Add(this.OpenDialoge);
            this.settings.Controls.Add(this.savefiledir);
            this.settings.Location = new System.Drawing.Point(12, 53);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(275, 254);
            this.settings.TabIndex = 8;
            // 
            // OpenDialoge
            // 
            this.OpenDialoge.Image = ((System.Drawing.Image)(resources.GetObject("OpenDialoge.Image")));
            this.OpenDialoge.Location = new System.Drawing.Point(226, 39);
            this.OpenDialoge.Name = "OpenDialoge";
            this.OpenDialoge.Size = new System.Drawing.Size(33, 23);
            this.OpenDialoge.TabIndex = 0;
            this.OpenDialoge.UseVisualStyleBackColor = true;
            this.OpenDialoge.Click += new System.EventHandler(this.OpenDialoge_Click);
            // 
            // savefiledir
            // 
            this.savefiledir.AutoSize = true;
            this.savefiledir.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savefiledir.Location = new System.Drawing.Point(11, 30);
            this.savefiledir.Name = "savefiledir";
            this.savefiledir.Size = new System.Drawing.Size(62, 15);
            this.savefiledir.TabIndex = 1;
            this.savefiledir.Text = "savefiledir";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 353);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Engine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Main.ResumeLayout(false);
            this.settings.ResumeLayout(false);
            this.settings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel Main;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button LoadGame_Button;
        private System.Windows.Forms.Button Game_Button;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button Settings_Button;
        private System.Windows.Forms.Panel settings;
        private System.Windows.Forms.Label savefiledir;
        private System.Windows.Forms.Button OpenDialoge;
    }
}