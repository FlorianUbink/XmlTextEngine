using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XmlFormEngine
{
    public partial class GameWindow : Form
    {
        public bool AllowEnter { get; set; }
        public bool InputEnter { get; set; }

        public bool isClicked { get; set; }
        private string InputString = "";
        private string PrevInputString = "";
        private static GameWindow windowHandle = null;
        public int maxY { get; set; }
        GameManager gameManager;
        Dictionary<string, Label> clickDic;

        public delegate void BranchReturnHandle(XmlNodeList optionList, string rollT);
        BranchReturnHandle branch2Handle = null;

        public GameWindow()
        {
            gameManager = new GameManager();
            clickDic = new Dictionary<string, Label>();
            branch2Handle = new BranchReturnHandle(gameManager.eventProcessor.Branch2);
            InitializeComponent();
            maxY = 0;
            AllowEnter = true;
            InputEnter = false;
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            windowHandle = this;
            clickDic.Add("A", windowHandle.LabelA);
            clickDic.Add("B", windowHandle.LabelB);
            gameManager.Update();
        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Program.gameState = GameState.Exit;
            }
        }

        public static void PrintTextBox(string pText)
        {
            // fill textbox
            windowHandle.PrintBox.Text = pText.Trim();
            
        }

        public static void PrintConsole(List<string> preProssesed)
        {
            if (windowHandle.PrintBox.Text != "")
            {
                Point newCursorPoint = new Point(Cursor.Position.X, windowHandle.maxY);
                Cursor.Position = newCursorPoint;

            }


            foreach (string pString in preProssesed)
            {
                windowHandle.PrintBox.Text += pString;
            }

        }



        public static string InputBranch(Dictionary<string,string> ClickDic, Dictionary<string, string> TypeDic, XmlNodeList commandList)
        {
            windowHandle.AllowEnter = false;
            bool Active = true;
            string returnKey = "";

            #region Clickable Options
            if (ClickDic.Count != 0 && ClickDic.Count <= 3)
            {
                foreach (string key in ClickDic.Keys)
                {
                    windowHandle.clickDic[key].Text = ClickDic[key];
                    windowHandle.clickDic[key].Visible = true;
                    windowHandle.clickDic[key].Enabled = true;
                    windowHandle.clickDic[key].BringToFront();
                    windowHandle.clickDic[key].Location = new Point(5, windowHandle.maxY + 5);
                    windowHandle.maxY = windowHandle.clickDic[key].Location.Y;
                    windowHandle.maxY += windowHandle.clickDic[key].Height;
                }

            }
            #endregion

            #region Typable Options
            if (TypeDic.Count != 0)
            {
                windowHandle.InputBox.Location = new Point(5, windowHandle.maxY + 5);
                windowHandle.InputBox.Visible = true;
                windowHandle.InputBox.Enabled = true;
                windowHandle.InputEnter = true;
            }
            #endregion

            #region Check Update
            //while (Active)
            //{
            //    // checks typeinput
            //    if (windowHandle.InputString != windowHandle.PrevInputString)
            //    {
            //        foreach (string key in TypeDic.Keys)
            //        {
            //            if (windowHandle.InputString == TypeDic[key])
            //            {
            //                returnKey = key;
            //                Active = false;
            //                break;
            //            }
            //        }
            //    }

            //    // checks Clickinput
            //    if (Active)
            //    {
            //        foreach (string key in ClickDic.Keys)
            //        {
            //            windowHandle.clickDic[key].Click += GameWindow_Click;

            //            if (windowHandle.isClicked)
            //            {
            //                returnKey = key;
            //                Active = false;
            //                break;
            //            }
            //        }
            //    }

            //}
            #endregion

            #region Update

            foreach (string key in windowHandle.clickDic.Keys)
            {
                windowHandle.clickDic[key].MouseClick += delegate { windowHandle.branch2Handle(commandList, key); };
            }

            #endregion


            return returnKey;

        }

        private static void GameWindow_Click(object sender, EventArgs e)
        {
            windowHandle.isClicked = true;
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (AllowEnter)
                {
                    // reset Settings
                    ResetWindow();
                    
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (InputEnter)
                {
                    PrevInputString = InputString;
                    InputString = windowHandle.InputBox.Text.Trim();
                }
            }
        }

        private void PrintBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            maxY = e.NewRectangle.Height;
           
        }

        public static void ResetWindow()
        {

            // reset Settings
            windowHandle.maxY = 0;
            windowHandle.AllowEnter = true;

            foreach (string key in windowHandle.clickDic.Keys)
            {
                windowHandle.clickDic[key].Text = "";
                windowHandle.clickDic[key].Enabled = false;
                windowHandle.clickDic[key].Visible = false;

            }

            windowHandle.PrintBox.Text = "";

            // reload
            windowHandle.gameManager.Update();
        }
    }
}
