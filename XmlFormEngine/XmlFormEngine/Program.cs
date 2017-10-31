using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            gameState = GameState.MainMenu;

            ChangedGamestate:
            switch (gameState)
            {
                case GameState.MainMenu:
                    Application.Run(new MainMenu());
                    goto ChangedGamestate;

                case GameState.Game:
                    Application.Run(new Game());
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
