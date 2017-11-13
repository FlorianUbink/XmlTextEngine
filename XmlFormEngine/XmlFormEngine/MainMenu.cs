using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XmlFormEngine
{
    public partial class MainMenu : Form
    {

        public MainMenu( string[] saves)
        {
            InitializeComponent();
            Label_Version.Text = string.Format("V{0}", Program.xSettings.Root.Element("General").Element("Version").Value);
            SavePanelLoad(saves);
            ManagePanels("main");
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

        private void LoadGame_Button_Click(object sender, EventArgs e)
        {
            ManagePanels("load");
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           Program.index = listBox1.SelectedIndex;
           Program.gameState = GameState.Game;
           this.Dispose();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            ManagePanels("main");
        }

        private void OpenDialoge_Click(object sender, EventArgs e)
        {
            string path = Program.xSettings.Root.Element("Path").Element("Saves").Value;
            savefiledir.Text = string.Format("Save folder: \n{0}",path);
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.SelectedPath = @Path.GetFullPath(path);
            
            fileDialog.ShowDialog();
            path = fileDialog.SelectedPath;
            Program.xSettings.Root.Element("Path").Element("Saves").Value = path;
            savefiledir.Text = string.Format("Save folder: \n {0}", path);
            SavePanelLoad(Directory.GetFiles(path));
            Program.xSettings.Save(@"..\..\XmlResources\Misc\Settings.xml");
        }

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            ManagePanels("settings");
        }

        private void SavePanelLoad(string[] saves)
        {
            listBox1.Items.Clear();
            foreach (string save in saves)
            {
                string[] file = Path.GetFileName(save).Split(new char[] { '_' });
                string r = string.Format("{0}-{1}-{2} {3}:{4}", file).Replace(".sdata", "");
                listBox1.Items.Add(r);
            }
            if (listBox1.Items.Count == 0)
            { LoadGame_Button.Enabled = false; }
        }

        private void ManagePanels(string enabled_Panel)
        {
            listBox1.Enabled = false;
            listBox1.Visible = false;
            ReturnButton.Enabled = false;
            ReturnButton.Visible = false;
            Main.Visible = false;
            Main.Enabled = false;
            settings.Visible = false;
            settings.Enabled = false;

            switch (enabled_Panel)
            {
                case "main":
                    Title.Text = "Main Menu";
                    Main.Visible = true;
                    Main.Enabled = true;
                    break;
                case "load":
                    Title.Text = "Load Menu";
                    listBox1.Enabled = true;
                    listBox1.Visible = true;
                    ReturnButton.Enabled = true;
                    ReturnButton.Visible = true;
                    break;
                case "settings":
                    Title.Text = "Settings";
                    string path = Program.xSettings.Root.Element("Path").Element("Saves").Value;
                    savefiledir.Text = string.Format("Save folder: \n{0}", path);
                    settings.Visible = true;
                    settings.Enabled = true;
                    ReturnButton.Enabled = true;
                    ReturnButton.Visible = true;

                    break;

                default:
                    break;
            }
        }
    }
}
