using System;
using System.Collections.Generic;
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

        [STAThread]
        static void Main()
        {
            XmlDocument XmlFile_Settings = new XmlDocument();
            XmlFile_Settings.Load(@"..\..\XmlResources\Misc\Settings.xml");
            string[] general = XmlFile_Settings.SelectSingleNode("/Game/General").InnerText.
                                                Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);

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
                    Application.Run(new MainMenu(general[0]));
                    goto ChangedGamestate;

                case GameState.Game:
                    Application.Run(new Game(XmlFile_Settings));
                    //Application.Run(new GameWindow());
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
