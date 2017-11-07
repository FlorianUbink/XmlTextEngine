using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlFormEngine
{
    public partial class MainMenu : Form
    {
        public MainMenu(string version)
        {
            InitializeComponent();
            Label_Version.Text = "V" + version.Trim();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Program.gameState = GameState.Exit;
            }
            
        }

        private void Game_Button_Click(object sender, EventArgs e)
        {
            Program.gameState = GameState.Game;
            this.Dispose();

        }
    }
}
