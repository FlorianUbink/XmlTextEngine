namespace XmlFormEngine
{
    partial class GameWindow
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
            this.PrintBox = new System.Windows.Forms.RichTextBox();
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.LabelA = new System.Windows.Forms.Label();
            this.LabelB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PrintBox
            // 
            this.PrintBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrintBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintBox.Location = new System.Drawing.Point(2, 5);
            this.PrintBox.Name = "PrintBox";
            this.PrintBox.ReadOnly = true;
            this.PrintBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PrintBox.Size = new System.Drawing.Size(930, 320);
            this.PrintBox.TabIndex = 0;
            this.PrintBox.Text = "";
            this.PrintBox.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.PrintBox_ContentsResized);
            // 
            // InputBox
            // 
            this.InputBox.Enabled = false;
            this.InputBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.Location = new System.Drawing.Point(2, 456);
            this.InputBox.Multiline = false;
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(930, 33);
            this.InputBox.TabIndex = 1;
            this.InputBox.Text = "";
            this.InputBox.Visible = false;

            // 
            // LabelA
            // 
            this.LabelA.AutoSize = true;
            this.LabelA.Enabled = false;
            this.LabelA.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelA.Location = new System.Drawing.Point(12, 340);
            this.LabelA.Name = "LabelA";
            this.LabelA.Size = new System.Drawing.Size(58, 19);
            this.LabelA.TabIndex = 2;
            this.LabelA.Text = "LabelA";
            this.LabelA.Visible = false;
            // 
            // LabelB
            // 
            this.LabelB.AutoSize = true;
            this.LabelB.Enabled = false;
            this.LabelB.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelB.Location = new System.Drawing.Point(12, 376);
            this.LabelB.Name = "LabelB";
            this.LabelB.Size = new System.Drawing.Size(58, 19);
            this.LabelB.TabIndex = 3;
            this.LabelB.Text = "LabelB";
            this.LabelB.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.LabelB);
            this.Controls.Add(this.LabelA);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.PrintBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameWindow_FormClosing);
            this.Load += new System.EventHandler(this.GameWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox PrintBox;
        private System.Windows.Forms.RichTextBox InputBox;
        private System.Windows.Forms.Label LabelA;
        private System.Windows.Forms.Label LabelB;
    }
}