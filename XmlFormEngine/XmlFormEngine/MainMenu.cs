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
        public MainMenu()
        {
            InitializeComponent();
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

        private void Editor_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Function comming soon ! :D");

            //Program.gameState = GameState.Editor;

        }
    }
}
