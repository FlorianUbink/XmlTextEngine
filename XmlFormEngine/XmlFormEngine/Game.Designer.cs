using System.Drawing;

namespace XmlFormEngine
{
    partial class Game
    {
        Color BackgroundColor;
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
            this.PrintWindow = new System.Windows.Forms.RichTextBox();
            this.Opt_A = new System.Windows.Forms.Label();
            this.Opt_B = new System.Windows.Forms.Label();
            this.Opt_C = new System.Windows.Forms.Label();
            this.Opt_D = new System.Windows.Forms.Label();
            this.Opt_TypeBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // PrintWindow
            // 
            this.PrintWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintWindow.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PrintWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PrintWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.PrintWindow.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintWindow.Location = new System.Drawing.Point(12, 12);
            this.PrintWindow.Name = "PrintWindow";
            this.PrintWindow.ReadOnly = true;
            this.PrintWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PrintWindow.Size = new System.Drawing.Size(920, 477);
            this.PrintWindow.TabIndex = 0;
            this.PrintWindow.Text = "";
            this.PrintWindow.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.PrintWindow_ContentsResized);
            // 
            // Opt_A
            // 
            this.Opt_A.AutoSize = true;
            this.Opt_A.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Opt_A.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Opt_A.Enabled = false;
            this.Opt_A.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_A.Location = new System.Drawing.Point(419, 101);
            this.Opt_A.Name = "Opt_A";
            this.Opt_A.Size = new System.Drawing.Size(45, 19);
            this.Opt_A.TabIndex = 1;
            this.Opt_A.Text = "OptA";
            this.Opt_A.Visible = false;
            this.Opt_A.TextChanged += new System.EventHandler(this.Opt_A_TextChanged);
            this.Opt_A.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Opt_A_MouseClick);
            this.Opt_A.MouseLeave += new System.EventHandler(this.Opt_A_MouseLeave);
            this.Opt_A.MouseHover += new System.EventHandler(this.Opt_A_MouseHover);
            // 
            // Opt_B
            // 
            this.Opt_B.AutoSize = true;
            this.Opt_B.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Opt_B.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Opt_B.Enabled = false;
            this.Opt_B.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_B.Location = new System.Drawing.Point(419, 138);
            this.Opt_B.Name = "Opt_B";
            this.Opt_B.Size = new System.Drawing.Size(45, 19);
            this.Opt_B.TabIndex = 2;
            this.Opt_B.Text = "OptB";
            this.Opt_B.Visible = false;
            this.Opt_B.TextChanged += new System.EventHandler(this.Opt_B_TextChanged);
            this.Opt_B.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Opt_B_MouseClick);
            this.Opt_B.MouseLeave += new System.EventHandler(this.Opt_B_MouseLeave);
            this.Opt_B.MouseHover += new System.EventHandler(this.Opt_B_MouseHover);
            // 
            // Opt_C
            // 
            this.Opt_C.AutoSize = true;
            this.Opt_C.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Opt_C.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Opt_C.Enabled = false;
            this.Opt_C.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_C.Location = new System.Drawing.Point(419, 173);
            this.Opt_C.Name = "Opt_C";
            this.Opt_C.Size = new System.Drawing.Size(45, 19);
            this.Opt_C.TabIndex = 3;
            this.Opt_C.Text = "OptC";
            this.Opt_C.Visible = false;
            this.Opt_C.TextChanged += new System.EventHandler(this.Opt_C_TextChanged);
            this.Opt_C.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Opt_C_MouseClick);
            this.Opt_C.MouseLeave += new System.EventHandler(this.Opt_C_MouseLeave);
            this.Opt_C.MouseHover += new System.EventHandler(this.Opt_C_MouseHover);
            // 
            // Opt_D
            // 
            this.Opt_D.AutoSize = true;
            this.Opt_D.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Opt_D.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Opt_D.Enabled = false;
            this.Opt_D.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_D.Location = new System.Drawing.Point(419, 203);
            this.Opt_D.Name = "Opt_D";
            this.Opt_D.Size = new System.Drawing.Size(46, 19);
            this.Opt_D.TabIndex = 4;
            this.Opt_D.Text = "OptD";
            this.Opt_D.Visible = false;
            this.Opt_D.TextChanged += new System.EventHandler(this.Opt_D_TextChanged);
            this.Opt_D.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Opt_D_MouseClick);
            this.Opt_D.MouseLeave += new System.EventHandler(this.Opt_D_MouseLeave);
            this.Opt_D.MouseHover += new System.EventHandler(this.Opt_D_MouseHover);
            // 
            // Opt_TypeBox
            // 
            this.Opt_TypeBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Opt_TypeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Opt_TypeBox.Enabled = false;
            this.Opt_TypeBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_TypeBox.Location = new System.Drawing.Point(339, 411);
            this.Opt_TypeBox.Multiline = false;
            this.Opt_TypeBox.Name = "Opt_TypeBox";
            this.Opt_TypeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Opt_TypeBox.Size = new System.Drawing.Size(228, 29);
            this.Opt_TypeBox.TabIndex = 5;
            this.Opt_TypeBox.Text = "";
            this.Opt_TypeBox.EnabledChanged += new System.EventHandler(this.Opt_TypeBox_EnabledChanged);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.Opt_TypeBox);
            this.Controls.Add(this.Opt_D);
            this.Controls.Add(this.Opt_C);
            this.Controls.Add(this.Opt_B);
            this.Controls.Add(this.Opt_A);
            this.Controls.Add(this.PrintWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "Game";
            this.Text = "Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox PrintWindow;
        private System.Windows.Forms.Label Opt_A;
        private System.Windows.Forms.Label Opt_B;
        private System.Windows.Forms.Label Opt_C;
        private System.Windows.Forms.Label Opt_D;
        private System.Windows.Forms.RichTextBox Opt_TypeBox;
    }
}