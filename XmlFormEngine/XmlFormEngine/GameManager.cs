using System.Xml;

namespace XmlFormEngine
{
    public enum GameUpdate
    {
        FileChange,
        NodeChange,
        NextCommand
    }

    public class GameManager
    {
        public EventProcessor eventProcessor = new EventProcessor();
        XmlDocument xmlDoc = new XmlDocument();
        XmlNodeList commandList;

        int prevEI = -1;
        public static int CurrentEventI { get; set; }
        public static GameUpdate gameUpdate { get; set; }
        public string xmlPath { get; set; }


        public GameManager()
        {

            // de-bug
            xmlPath = @"..\..\XMLFile2.xml";
            eventProcessor.ManualEISet("1");
            CurrentEventI = 1;
            eventProcessor.nextCI = 1;
        }


        public void GetXmlFile(string path)
        {
            xmlDoc.Load(path);
        }

        public void Update()
        {
            ReUpdate:
            switch (gameUpdate)
            {
                case GameUpdate.FileChange:
                    GetXmlFile(xmlPath);
                    gameUpdate = GameUpdate.NodeChange;
                    goto ReUpdate;
                    

                case GameUpdate.NodeChange:
                    eventProcessor.LoadNode(xmlDoc, CurrentEventI);
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
