using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
        Print,
        Branch_Condition,
        Branch_Input,
        Branch_Roll,
        Branch_Result
    }

    public partial class Game : Form
    {
        #region Properties
        #region Public
        public EnterHandling enterHandle { get; set; }
        public CommandHandling currentHandling { get; set; }
        #endregion
        #region Private
        EventProcessor eventProcessor;

        //Print
        List<string> sourceText = new List<string>();

        // Banch
        XmlDocument XmlFile_Current = new XmlDocument();
        XmlNodeList option_List = null;
        bool input_Available = false;
        bool roll_Available = false;
        #endregion
        #endregion



        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            eventProcessor = new EventProcessor();
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
            Update_Start:
            switch (currentHandling)
            {
                case CommandHandling.Print:
                    break;
                case CommandHandling.Branch_Condition:
                    option_List = eventProcessor.Condition_Check(option_List, out input_Available, out roll_Available);
                    if (input_Available)
                    {
                        currentHandling = CommandHandling.Branch_Input;
                    }
                    else if (roll_Available)
                    {
                        currentHandling = CommandHandling.Branch_Roll;
                    }
                    goto Update_Start;

                case CommandHandling.Branch_Input:
                    eventProcessor.Input_SetTrigger(option_List, ref Opt_A, ref Opt_B, ref Opt_C, ref Opt_D);
                    break;
                case CommandHandling.Branch_Roll:
                    break;
                case CommandHandling.Branch_Result:
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
