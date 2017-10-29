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
    public enum EnterHandling
    {
        newEvent,
        continueEvent,
        confirmInput
    }

    public enum CommandHandling
    {
        PrintNew,
        PrintContinue,
        BranchCondition,
        BranchInput,
        BranchRoll,
        BranchResult
    }

    public partial class Game : Form
    {
        #region Properties
        #region Public
        public EnterHandling enterHandle { get; set; }
        public CommandHandling currentHandling { get; set; }
        #endregion
        #region Private
        //Print
        List<string> sourceText = new List<string>();

        #endregion
        #endregion



        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                switch (enterHandle)
                {
                    case EnterHandling.newEvent:
                        // clear the screen
                        break;
                    case EnterHandling.continueEvent:
                        // maybe just default
                        break;
                    case EnterHandling.confirmInput:
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateGame()
        {
            switch (currentHandling)
            {
                case CommandHandling.PrintNew:
                    // 
                    break;
                case CommandHandling.PrintContinue:
                    break;
                case CommandHandling.BranchCondition:
                    break;
                case CommandHandling.BranchInput:
                    break;
                case CommandHandling.BranchRoll:
                    break;
                case CommandHandling.BranchResult:
                    break;
                default:
                    break;
            }
        }






        private void PrintWindow_ContentsResized(object sender, ContentsResizedEventArgs e)
        {

        }

        #region OptionEvents
        #region ClickEvents

        private void Opt_A_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Opt_B_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Opt_C_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Opt_D_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion
        #region HoverEvents

        private void Opt_A_MouseHover(object sender, EventArgs e)
        {

        }

        private void Opt_B_MouseHover(object sender, EventArgs e)
        {

        }

        private void Opt_C_MouseHover(object sender, EventArgs e)
        {

        }

        private void Opt_D_MouseHover(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion
    }
}
