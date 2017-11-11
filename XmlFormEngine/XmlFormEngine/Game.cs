using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlFormEngine
{
    public enum EnterHandling
    {
        newEvent,
        continueEvent,
        confirmInput,
        disabled
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
        public CommandHandling currentHandling { get; set; }
        public int PrintWindow_YValue { get; set; }
        public object SystemDraw { get; private set; }
        #endregion
        #region Private

        //GamePlay
        EnterHandling enterHandle;
        XmlDocument XmlFile_Gamefile = new XmlDocument();
        XmlDocument SettingXml = null;
        XmlNodeList command_List = null;
        XmlNodeList command_ContentList = null;
        EventProcessor eventProcessor;
        Dictionary<string, string> XmlFileLibrary = new Dictionary<string, string>();
        List<int> EI_Enabled = new List<int>();
        int comList_I = 0;
        int CI_Current = -1;
        int EI_Current = -1;
        int EI_Previous =-1;
        bool Update_Active = true;
        int tickCount = 0;
        //Print
        List<string> sourceText = new List<string>();
        int printStart = 0;
        int lineStart = 0;
        XmlNodeList PrintList_Processed = null;
        int sec = 0;

        // Banch
        Dictionary<string, string> Opt_type = new Dictionary<string, string>();
        bool input_Available = false;
        bool roll_Available = false;
        string result_Info = "";
        string Type_ErrorMessage = "\n Sorry, I dont know what u mean by that.\n";
        #endregion
        #endregion

        #region Pre-Game
        public Game(XmlDocument settingXml)
        {
            InitializeComponent();
            SettingXml = settingXml;
            
        }

        private void Game_Load(object sender, EventArgs e)
        {
            #region DebugLoad
            EI_Current = 20;
            EI_Enabled.Add(20);
            #endregion

            #region Load XmlFileLibrary
            string entry = SettingXml.SelectSingleNode("/Game/XmlLibrary").InnerText;
            string[] entry_split = entry.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string file in entry_split)
            {
                string longString = file.Trim().Replace(" ", "");
                string[] split = longString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                XmlFileLibrary.Add(split[0], split[1]);
            }
            
            #endregion
            eventProcessor = new EventProcessor(this);
            PrintWindow.TabStop = false;
            Game_Reset();

            Update_CommandList();
            Update_Command();
        }

        #endregion

        #region Content distribution
        private void XmlFileLoader()
        {
            foreach (string range in XmlFileLibrary.Keys)
            {
                string[] spectrum = range.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (EI_Current >= int.Parse(spectrum[0]) && EI_Current < int.Parse(spectrum[1]))
                {
                    XmlFile_Gamefile.Load((@"..\..\" + XmlFileLibrary[range]));
                    break;
                }
            }

            if (XmlFile_Gamefile == null)
            {
                throw new Exception("Cant find:" + EI_Current);
            }
        }

        private void Update_CommandList()
        {
            if (EI_Current != EI_Previous)
            {
                Update_Active = true;
                Start_EISearch:
                if(command_List == null)
                {
                    XmlFileLoader();
                }
                string search = ("/Game/E_" + EI_Current);
                command_List = XmlFile_Gamefile.SelectSingleNode(search).ChildNodes;
                if (command_List.Count == 0)
                {
                    XmlFileLoader();
                    goto Start_EISearch;
                }

                EI_Previous = EI_Current;
                comList_I = 0;
            }
        }

        private void Update_Command()
        {
            //Update_Active = true;
            while (Update_Active)
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
                            Update_Active = false;
                        }
                    }
                    else
                    {
                        Update_Active = false;
                    }

                }
                else
                {
                    Update_Active = false;
                }
            }
        }

        private void Process_CommandContent()
        {
            Update_Start:
            switch (currentHandling)
            {
                case CommandHandling.Print:
                    PrintList_Processed = eventProcessor.Print_PreProcess(command_ContentList, ref EI_Enabled);
                    PrintWindow_TextProcessing(PrintList_Processed);
                    break;

                case CommandHandling.Branch_Condition:
                    command_ContentList = eventProcessor.Condition_Check(command_ContentList, out input_Available, out roll_Available);
                    if (input_Available)
                    {
                        currentHandling = CommandHandling.Branch_Input;
                        enterHandle = EnterHandling.disabled;
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
                    eventProcessor.Input_SetTrigger(command_ContentList, ref enterHandle, ref Opt_type, ref Opt_TypeBox, 
                                                                         ref Opt_A, ref Opt_B, ref Opt_C, ref Opt_D);
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

        #endregion

        #region Command Functions
        private void PrintWindow_TextProcessing(XmlNodeList printList)
        {
            sec = 0;
            for (int i = printStart; i < printList.Count; i++)
            {

                XmlNode printNode = printList[i];
                if (printNode.HasChildNodes)
                {
                    
                    for (int j = lineStart; j < printNode.ChildNodes.Count; j++)
                    {
                        XmlNode line = printNode.ChildNodes[j];
                        switch (line.Name)
                        {
                            case "LC":
                                string innerCommand = line.InnerText.ToUpper().Replace(" ", "");
                                if (innerCommand == "WAITINPUT")
                                {
                                    enterHandle = EnterHandling.continueEvent;
                                    printStart = i;
                                    lineStart = j + 1;
                                    Update_Active = false;
                                    goto End;
                                }
                                else if (innerCommand.Contains("WAIT"))
                                {
                                    sec = int.Parse(innerCommand.Replace("WAIT", ""));
                                    printStart = i;
                                    lineStart = j + 1;
                                    Update_Active = false;
                                    timer2.Start();
                                    goto End;
                                }
                                break;
                            default:
                                PrintWindow.Text += line.InnerText;
                                break;
                        }
                    }
                    lineStart = 0;
                }
                else
                {
                    PrintWindow.Text += printNode.InnerText;
                }
            }
        End: {  }

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
        #endregion

        #region Event-Related

        #region Console
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                
                switch (enterHandle)
                {
                    case EnterHandling.newEvent:
                        Game_Reset();
                    ReHandle:
                        Update_Active = true;
                        lineStart = 0;
                        printStart = 0;
                        Update_CommandList();
                        Update_Command();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case EnterHandling.continueEvent:
                        PrintWindow_TextProcessing(PrintList_Processed);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        enterHandle = EnterHandling.newEvent;
                        goto ReHandle;
                    case EnterHandling.confirmInput:
                        PrintWindow.Text = PrintWindow.Text.Replace(Type_ErrorMessage, " ");
                        string input = Opt_TypeBox.Text.Replace(" ", "");
                        input = input.ToUpper();

                        if (Opt_type.ContainsKey(input))
                        {
                            Input_ContentListUpdate(ref command_ContentList, Opt_type[input]);
                            currentHandling = update_CurrentHandling(roll_Available);
                            Process_CommandContent();
                            Game_Reset();
                            Update_CommandList();
                            Update_Command();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else
                        {
                            Opt_TypeBox.Text = "";
                            PrintWindow.Text += Type_ErrorMessage;
                        }


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
        #endregion

        private void PrintWindow_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (PrintWindow.Text != "")
            {
                PrintWindow_YValue = e.NewRectangle.Bottom + 20;
            }

        }

        #region Opt_Labels

        #region Click

        private void Opt_A_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "A");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
            Game_Reset();
            Update_CommandList();
            Update_Command();

        }

        private void Opt_B_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "B");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
            Game_Reset();
            Update_CommandList();
            Update_Command();
        }

        private void Opt_C_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "C");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
            Game_Reset();
            Update_CommandList();
            Update_Command();
        }

        private void Opt_D_MouseClick(object sender, MouseEventArgs e)
        {
            Input_ContentListUpdate(ref command_ContentList, "D");
            currentHandling = update_CurrentHandling(roll_Available);
            Process_CommandContent();
            Game_Reset();
            Update_CommandList();
            Update_Command();
        }
        #endregion

        #region Resize
        private void Opt_A_TextChanged(object sender, EventArgs e)
        {
            if (Opt_A.Text != "")
            {
                Opt_A.Location = new Point(PrintWindow.Location.X, PrintWindow_YValue + 5);
                PrintWindow_YValue = Opt_A.Height + Opt_A.Location.Y;
            }
        }

        private void Opt_B_TextChanged(object sender, EventArgs e)
        {
            if (Opt_B.Text != "")
            {
                Opt_B.Location = new Point(PrintWindow.Location.X, PrintWindow_YValue + 5);
                PrintWindow_YValue = Opt_B.Height + Opt_B.Location.Y;
            }
        }

        private void Opt_C_TextChanged(object sender, EventArgs e)
        {
            if (Opt_C.Text != "")
            {
                Opt_C.Location = new Point(PrintWindow.Location.X, PrintWindow_YValue + 5);
                PrintWindow_YValue = Opt_C.Height + Opt_C.Location.Y;
            }
        }

        private void Opt_D_TextChanged(object sender, EventArgs e)
        {
            if (Opt_D.Text != "")
            {
                Opt_D.Location = new Point(PrintWindow.Location.X, PrintWindow_YValue + 5);
                PrintWindow_YValue = Opt_D.Height + Opt_D.Location.Y;
            }
        }

        private void Opt_TypeBox_EnabledChanged(object sender, EventArgs e)
        {
            Opt_TypeBox.Location = new Point(PrintWindow.Location.X + 5, PrintWindow_YValue + 10);
            PrintWindow_YValue = Opt_TypeBox.Height + Opt_TypeBox.Location.Y;
        }
        #endregion

        #region Hover

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

        #endregion

        #region Help functions

        private void Game_Reset()
        {
            PrintWindow.Text = "";
            Opt_TypeBox.Text = "";
            Opt_A.Text = "";
            Opt_B.Text = "";
            Opt_C.Text = "";
            Opt_D.Text = "";
            Opt_TypeBox.Enabled = false;
            Opt_A.Enabled = false;
            Opt_B.Enabled = false;
            Opt_C.Enabled = false;
            Opt_D.Enabled = false;
            Opt_TypeBox.Visible = false;
            Opt_A.Visible = false;
            Opt_B.Visible = false;
            Opt_C.Visible = false;
            Opt_D.Visible = false;
            enterHandle = EnterHandling.newEvent;
            PrintWindow_YValue = 0;
        }
        #region CommandHandeling
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
        #endregion

        public string LinkingInformationLoad(string call)
        {
            bool Active = true;
            string returnString = call;

            while (Active)
            {
                #region EventIdentifier
                if (returnString.Contains("isEIEnabled"))
                {
                    int start = returnString.IndexOf('(', returnString.IndexOf("isEIEnabled"));
                    int end = returnString.IndexOf(')', start) - 1;
                    int EI = int.Parse(returnString.Substring(start + 1, end - start));
                    int validation = -1;

                    for (int i = 0; i < EI_Enabled.Count; i++)
                    {
                        if (EI == EI_Enabled[i])
                        {
                            validation = 1;
                            break;
                        }
                        else if (i == EI_Enabled.Count - 1)
                        {
                            validation = 0;
                            break;
                        }
                    }
                    returnString = returnString.Replace("isEIEnabled(" + EI + ')', validation + "");
                }
                else if (returnString.Contains("isEICurrent"))
                {
                    int start = returnString.IndexOf('(', returnString.IndexOf("isEIEnabled"));
                    int end = returnString.IndexOf(')', start);
                    int EI = int.Parse(returnString.Substring(start + 1, end - start));
                    int validation = -1;

                    if (EI == EI_Current) { validation = 1; }

                    else { validation = 0; }

                    returnString = returnString.Replace("isEICurrent(" + EI + ')', validation + "");
                }
                #endregion

                #region CI_Identifier
                //if (returnString.Contains("isCIEnabled"))
                //{
                //    int start = returnString.IndexOf('(', returnString.IndexOf("isCIEnabled"));
                //    int end = returnString.IndexOf(')', start);
                //    int CI = int.Parse(returnString.Substring(start + 1, end - start));
                //    int validation = -1;

                //    for (int i = 0; i < CI_Enabled.Count; i++)
                //    {
                //        if (CI == CI_Enabled[i])
                //        {
                //            validation = 1;
                //            break;
                //        }
                //        else if (i == CI_Enabled.Count - 1)
                //        {
                //            validation = 0;
                //            break;
                //        }
                //    }
                //    returnString = returnString.Replace("isCIEnabled(" + CI + ')', validation + "");
                //}
                else if (returnString.Contains("isCICurrent"))
                {
                    int start = returnString.IndexOf('(', returnString.IndexOf("isCIEnabled"));
                    int end = returnString.IndexOf(')', start);
                    int CI = int.Parse(returnString.Substring(start + 1, end - start));
                    int validation = -1;

                    if (CI == CI_Current) { validation = 1; }

                    else { validation = 0; }

                    returnString = returnString.Replace("isCICurrent(" + CI + ')', validation + "");
                }
                #endregion

                else { Active = false; }
            }
            return returnString;
        }


        #endregion

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        private void PrintWindow_SelectionChanged(object sender, EventArgs e)
        {
            PrintWindow.SelectionLength = 0;
            HideCaret(PrintWindow.Handle);
        }


        private void PrintWindow_TextChanged(object sender, EventArgs e)
        {
            HideCaret(PrintWindow.Handle);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                tickCount += timer2.Interval;
                if (tickCount >= sec * 1000)
                {
                    timer2.Stop();
                    tickCount = 0;
                    PrintWindow_TextProcessing(PrintList_Processed);
                    Update_CommandList();
                    Update_Command();
                    Update_Active = true;
                }
            }
        }
    }
}
