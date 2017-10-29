using System.Collections.Generic;
using System.Xml;

namespace XmlFormEngine
{
    public enum GameUpdate
    {
        NodeChange,
        NextCommand
    }

    public class GameManager
    {
        public EventProcessor eventProcessor = new EventProcessor();
        XmlNodeList commandList;

        int prevEI = -1;
        public static int CurrentEventI { get; set; }
        public static GameUpdate gameUpdate { get; set; }
        public string xmlPath { get; set; }


        public GameManager()
        {

            // de-bug
            xmlPath = @"..\..\XMLFile2.xml";
            //eventProcessor.ManualEISet("1");
            CurrentEventI = 10;
            eventProcessor.nextCI = 1;
        }


        public void Update()
        {
            ReUpdate:
            switch (gameUpdate)
            {
                case GameUpdate.NodeChange:
                    eventProcessor.LoadNode(CurrentEventI);
                    gameUpdate = GameUpdate.NextCommand;
                    goto ReUpdate;

                case GameUpdate.NextCommand:
                    eventProcessor.CommandLoader();
                    break;

                default:
                    break;
            }
        }

    }
}
