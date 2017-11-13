using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XmlFormEngine
{
    public enum GameState
    {
        MainMenu,
        Game,
        Editor,
        Exit
    }





    static class Program
    {
        public static GameState gameState { get; set; }
        public static int index { get; set; } = -1;
        public static XDocument xSettings { get; set; } = XDocument.Load(@"..\..\XmlResources\Misc\Settings.xml");


        [STAThread]
        static void Main()
        {;


            XmlDocument XmlFile_Settings = new XmlDocument();
            XmlFile_Settings.Load(@"..\..\XmlResources\Misc\Settings.xml");
            string[] general = XmlFile_Settings.SelectSingleNode("/Game/General").InnerText.
                                                Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
            string save = "";

            if (!Directory.Exists(xSettings.Root.Element("Path").Element("Saves").Value))
            {
                xSettings.Root.Element("Path").Element("Saves").Value = @"..\..\XmlResources\Save";
            }

            string[] saves = Directory.GetFiles(xSettings.Root.Element("Path").Element("Saves").Value);


            gameState = GameState.MainMenu;

            #region XDocument - test

            //var doc =XDocument.Load(@"..\..\XmlResources/Characters/Playable.xml");

            //Player0 player = new Player0
            //{
            //    Name = doc.Root.Element("Player4").Element("a").Value,
            //    Age = int.Parse(doc.Root.Element("Player4").Element("b").Value),
            //    Strength = int.Parse(doc.Root.Element("Player4").Element("c").Value),
            //};

            #endregion

            ChangedGamestate:
            switch (gameState)
            {
                case GameState.MainMenu:
                    Application.Run(new MainMenu(saves));

                    goto ChangedGamestate;

                case GameState.Game:
                    if (saves.Count() != 0 && index != -1)
                    {
                        save = saves[index];
                    }
                    Application.Run(new Game(XmlFile_Settings,save));
                    goto ChangedGamestate;

                case GameState.Editor:
                    //nothing
                    goto ChangedGamestate;

                case GameState.Exit:
                    Application.Exit();
                    break;
                default:
                    break;
            }

        }
    }
}
