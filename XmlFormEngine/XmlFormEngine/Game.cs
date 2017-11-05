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
        Branch_Result,
        None
    }

    public partial class Game : Form
    {
        #region Properties
        #region Public
        public EnterHandling enterHandle { get; set; }
        public CommandHandling currentHandling { get; set; }
        #endregion
        #region Private

        //GamePlay
        XmlDocument XmlFile_Current = new XmlDocument();
        XmlNodeList command_List = null;
        XmlNodeList command_ContentList = null;
        EventProcessor eventProcessor;
        Dictionary<string, string> XmlFileLibrary = new Dictionary<string, string>();
        List<int> EI_Enabled = new List<int>();
        int comList_I = 0;
        int CI_Current = -1;
        int EI_Current = 20;
        int EI_Previous =-1;

        //Print
        List<string> sourceText = new List<string>();

        // Banch
        bool input_Available = false;
        bool roll_Available = false;
        string result_Info = "";
        #endregion
        #endregion



        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            #region Load XmlFileLibrary
            XmlFileLibrary.Add("20-21", "XmlFile3.xml");
            #endregion
            eventProcessor = new EventProcessor();
            Update_CommandList();
            Update_Command();
        }




        private void XmlFileLoader()
        {
            foreach (string range in XmlFileLibrary.Keys)
            {
                string[] spectrum = range.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (EI_Current >= int.Parse(spectrum[0]) && EI_Current < int.Parse(spectrum[1]))
                {
                    XmlFile_Current.Load((@"..\..\" + XmlFileLibrary[range]));
                    break;
                }
            }

            if (XmlFile_Current == null)
            {
                throw new Exception("Cant find:" + EI_Current);
            }
        }

        private void Update_CommandList()
        {
            if (EI_Current != EI_Previous)
            {
                Start_EISearch:
                if(command_List == null)
                {
                    XmlFileLoader();
                }
                string search = ("/Game/E_" + EI_Current);
                command_List = XmlFile_Current.SelectSingleNode(search).ChildNodes;
                if (command_List.Count == 0)
                {
                    XmlFileLoader();
                    goto Start_EISearch;
                }

                EI_Previous = EI_Current;
            }
        }

        private void Update_Command()
        {
            bool Active = true;

            while (Active)
            {
                if (command_List != null)
                {
                    XmlNode command = command_List[comList_I];
                    comList_I += 1;

                    if (command != null)
                    {
                        // default command
                        if (command.Attributes.Count == 0 && CI_Current == -1)
                        {
                            command_ContentList = command.ChildNodes;
                            currentHandling = update_CurrentHandling(command.Name);
                            Process_CommandContent();
                        }

                        // EI tag
                        else if (command.Attributes["EI"] != null)
                        {
                            if (EI_Enabled.Contains(int.Parse(command.Attributes["EI"].Value)) && CI_Current == -1)
                            {
                                command_ContentList = command.ChildNodes;
                                currentHandling = update_CurrentHandling(command.Name);
                                Process_CommandContent();
                            }
                        }

                        // CI tag has priority over EI tag
                        else if (command.Attributes["CI"] != null)
                        {
                            if (int.Parse(command.Attributes["CI"].Value) == CI_Current)
                            {
                                command_ContentList = command.ChildNodes;
                                currentHandling = update_CurrentHandling(command.Name);
                                Process_CommandContent();
                            }
                        }

                        // value reloop
                        if (command.Name == "Branch" || command_List.Count <= comList_I)
                        {
                            Active = false;
                        }
                    }
                    else
                    {
                        Active = false;
                    }

                }
                else
                {
                    Active = false;
                }
            }
        }


        private void Process_CommandContent()
        {
            Update_Start:
            switch (currentHandling)
            {
                case CommandHandling.Print:
                    sourceText = eventProcessor.Print_PreProcess(command_ContentList);
                    PrintWindow_TextProcessing();
                    break;

                case CommandHandling.Branch_Condition:
                    command_ContentList = eventProcessor.Condition_Check(command_ContentList, out input_Available, out roll_Available);
                    if (input_Available)
                    {
                        currentHandling = CommandHandling.Branch_Input;
                    }
                    else if (roll_Available)
                    {
                        currentHandling = CommandHandling.Branch_Roll;
                    }
                    else
                    {
                        currentHandling = CommandHandling.Branch_Result;
                    }
                    goto Update_Start;

                case CommandHandling.Branch_Input:
                    eventProcessor.Input_SetTrigger(command_ContentList, ref Opt_A, ref Opt_B, ref Opt_C, ref Opt_D);
                    break;

                case CommandHandling.Branch_Roll:
                    command_ContentList = eventProcessor.Roll_Solve(command_ContentList, out result_Info);
                    currentHandling = CommandHandling.Branch_Result;
                    goto Update_Start;

                case CommandHandling.Branch_Result:
                    eventProcessor.Result_Solve(command_ContentList, ref result_Info, ref EI_Current, ref CI_Current, ref EI_Enabled);
                    break;
                default:
                    break;
            }
        }

        private void PrintWindow_TextProcessing()
        {
            if (sourceText.Count > 1)
            {
                enterHandle = EnterHandling.continueEvent;
            }
            else
            {
                enterHandle = EnterHandling.newEvent;
            }

            PrintWindow.Text += sourceText[0];
            sourceText.Remove(sourceText[0]);
        }

        private void Game_Reset()
        {
            PrintWindow.Text = "";
            Opt_A.Text = "";
            Opt_B.Text = "";
            Opt_C.Text = "";
            Opt_D.Text = "";
            Opt_A.Enabled = false;
            Opt_B.Enabled = false;
            Opt_C.Enabled = false;
            Opt_D.Enabled = false;
            Opt_A.Visible = false;
            Opt_B.Visible = false;
            Opt_C.Visible = false;
            Opt_D.Visible = false;
        }

        private void Input_ContentListUpdate(ref XmlNodeList command_ContentList, string Optiontag)
        {
            for (int i = 0; i < command_ContentList.Count; i++)
            {
                if (command_ContentList[i].Attributes["Tag"].Value != Optiontag)
                {
                    command_ContentList[i].ParentNode.RemoveChild(command_ContentList[i]);
                }
            }
        }


        private void PrintWindow_ContentsResized(object sender, ContentsResizedEventArgs e)
        {

        }

        #region OptionEvents
        #region ClickEvents

        private void Opt_A_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "A");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
        }

        private void Opt_B_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "B");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
        }

        private void Opt_C_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "C");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
        }

        private void Opt_D_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "D");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
        }
        #endregion
        #region HoverEvents

        private void Opt_A_MouseHover(object sender, EventArgs e)
        {
            Opt_A.ForeColor = Color.Red;
        }

        private void Opt_A_MouseLeave(object sender, EventArgs e)
        {
            Opt_A.ForeColor = Color.Black;
        }

        private void Opt_B_MouseHover(object sender, EventArgs e)
        {
            Opt_B.ForeColor = Color.Red;
        }

        private void Opt_B_MouseLeave(object sender, EventArgs e)
        {
            Opt_B.ForeColor = Color.Black;
        }

        private void Opt_C_MouseHover(object sender, EventArgs e)
        {
            Opt_C.ForeColor = Color.Red;
        }

        private void Opt_C_MouseLeave(object sender, EventArgs e)
        {
            Opt_C.ForeColor = Color.Black;
        }

        private void Opt_D_MouseHover(object sender, EventArgs e)
        {
            Opt_D.ForeColor = Color.Red;
        }

        private void Opt_D_MouseLeave(object sender, EventArgs e)
        {
            Opt_D.ForeColor = Color.Black;
        }
        #endregion

        #endregion

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (enterHandle)
                {
                    case EnterHandling.newEvent:
                        Game_Reset();
                        Update_CommandList();
                        Update_Command();
                        break;
                    case EnterHandling.continueEvent:
                        PrintWindow_TextProcessing();
                        break;
                    case EnterHandling.confirmInput:
                        break;
                    default:
                        break;
                }
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Program.gameState = GameState.Exit;
            }
        }

        private CommandHandling update_CurrentHandling(string command_Name)
        {
            switch (command_Name)
            {
                case "Print":
                    return CommandHandling.Print;
                case "Branch":
                    return CommandHandling.Branch_Condition;
                default:
                    return CommandHandling.None;
            }
        }

        private CommandHandling update_CurrentHandling(bool Roll_Available)
        {
            if (roll_Available)
            {
                return CommandHandling.Branch_Roll;
            }
            else
            {
                return CommandHandling.Branch_Result;
            }
        }
    }
}
