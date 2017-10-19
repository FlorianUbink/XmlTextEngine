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
            this.Editor_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Game_Button
            // 
            this.Game_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_Button.Location = new System.Drawing.Point(12, 12);
            this.Game_Button.Name = "Game_Button";
            this.Game_Button.Size = new System.Drawing.Size(240, 50);
            this.Game_Button.TabIndex = 0;
            this.Game_Button.Text = "New Game";
            this.Game_Button.UseVisualStyleBackColor = true;
            this.Game_Button.Click += new System.EventHandler(this.Game_Button_Click);
            // 
            // Editor_Button
            // 
            this.Editor_Button.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor_Button.Location = new System.Drawing.Point(12, 131);
            this.Editor_Button.Name = "Editor_Button";
            this.Editor_Button.Size = new System.Drawing.Size(240, 50);
            this.Editor_Button.TabIndex = 1;
            this.Editor_Button.Text = "Editor";
            this.Editor_Button.UseVisualStyleBackColor = true;
            this.Editor_Button.Click += new System.EventHandler(this.Editor_Button_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 221);
            this.Controls.Add(this.Editor_Button);
            this.Controls.Add(this.Game_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Game_Button;
        private System.Windows.Forms.Button Editor_Button;
    }
}