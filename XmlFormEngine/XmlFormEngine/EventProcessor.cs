using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using TextProcessing;

namespace XmlFormEngine
{
    public class EventProcessor
    {
        #region Public properties
        public int nextCI { get; set; }
        public int nextEI { get; set; }
        public int indexIndex { get; set; } = 0;
        public bool AutoNode { get; set; } = true;
        public object Keyboard { get; private set; }
        #endregion

        #region Private Properties

        bool hasRoll = false;
        bool hasInput = false;
        string rollTag = "";

        List<string> includedOptions = new List<string>();
        List<string> usedOptions = new List<string>();
        List<int> EnabledEI = new List<int>();
        List<int> EnabledCI = new List<int>();

        public delegate void PrintBoxHandle(string pText);
        PrintBoxHandle pbHandle = new PrintBoxHandle(GameWindow.PrintTextBox);
        public delegate string BranchInputHandler(Dictionary<string, string> ClickDic, Dictionary<string, string> TypeDic, XmlNodeList commandOption);
        BranchInputHandler InputHandle = new BranchInputHandler(GameWindow.InputBranch);
        public delegate void ResetWindowHandle();
        ResetWindowHandle resetHandle = new ResetWindowHandle(GameWindow.ResetWindow);


        XmlNodeList CommandList = null;
        XmlDocument xmlDoc = new XmlDocument();
        CharacterManager cManager = new CharacterManager();
        string[] splitCharacters = new string[] { "\r\n" };
        string[] transformOperators = new string[] { "==", "+=", "-=", "/=", "*=" };
        private static string WindowInput = string.Empty;
        Random dice = new Random();
        #endregion

        #region Public functions

        public void LoadNode(XmlDocument xmlDoc, int CurrentEventI)
        {
            CommandList = null;
            CommandList = xmlDoc.SelectSingleNode("/Game/Event[@EI = " + CurrentEventI + "]").ChildNodes;
            indexIndex = 0;
            AutoNode = true;
        }

        public void CommandLoader()
        {

            while (AutoNode)
            {
                XmlNode nextCommand = null;


                for (int i = indexIndex; i < CommandList.Count; i++)
                {
                    if (CommandList[i].Attributes.Count == 0)
                    {
                        nextCommand = CommandList[i];
                        indexIndex = i + 1;
                        break;
                    }

                    else if (int.Parse(CommandList[i].Attributes["CI"].Value) == nextCI)
                    {
                        nextCommand = CommandList[i];
                        indexIndex = i + 1;
                        break;
                    }

                }

                if (nextCommand == null)
                {
                    AutoNode = false;
                }
                else
                {
                    AutoNode = CommandProces(nextCommand);
                }
            }
            EnabledCI.Clear();
            AutoNode = true;
        }

        public void ManualEISet(string EI)
        {
            // ATM not string but int
            if (!EnabledEI.Contains(int.Parse(EI)))
            {
                EnabledEI.Add(int.Parse(EI));
            }
        }
        #endregion

        #region Command-functions
        private bool CommandProces(XmlNode CommandNode)
        {

            if (CommandNode.Name == "Print")
            {
                // sent to Print
                Print(CommandNode.ChildNodes);
                return true;
            }

            else if (CommandNode.Name == "Branch")
            {
                // sent to Branch
                Branch(CommandNode.SelectNodes("Option"));
                return false;
            }
            else
            {
                throw new Exception(CommandNode.Name + " DOESNT EXIST IN CONTEXT.");
            }
        }

        private bool Print(XmlNodeList commandPrintList)
        {
            string windowText = "";

            foreach (XmlNode printNode in commandPrintList)
            {
                #region Enable/Disable nodes
                if (printNode.Name == "Enable")
                {
                    if (printNode.Attributes["EI"] != null)
                    {
                        if (EnabledEI.Contains(int.Parse(printNode.Attributes["EI"].Value)))
                        {
                            string pText = printNode.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = SlashCommands(pText);

                            windowText += pText;
                        }
                    }
                    if (printNode.Attributes["CI"] != null)
                    {
                        if (EnabledCI.Contains(int.Parse(printNode.Attributes["CI"].Value)))
                        {
                            string pText = printNode.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = SlashCommands(pText);
                            windowText += pText;
                        }
                    }
                }
                else if (printNode.Name == "Disable")
                {
                    if (printNode.Attributes["EI"] != null)
                    {
                        if (!EnabledEI.Contains(int.Parse(printNode.Attributes["EI"].Value)))
                        {
                            string pText = printNode.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = SlashCommands(pText);
                            windowText += pText;
                        }
                    }
                    if (printNode.Attributes["CI"] != null)
                    {
                        if (!EnabledCI.Contains(int.Parse(printNode.Attributes["CI"].Value)))
                        {
                            string pText = printNode.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = SlashCommands(pText);
                            windowText += pText;
                        }
                    }
                }
                #endregion

                else
                {
                    string pText = printNode.InnerText.TrimStart();
                    pText = pText.TrimEnd(new char[] { ' ' });
                    pText = SlashCommands(pText);

                    windowText += pText;
                }
            }

            pbHandle.Invoke(windowText);
            return true;    // return value indicates if this block contains a stop-autoreload
        }

        private void Branch(XmlNodeList commandOptionList)
        {

            hasInput = false;
            #region Conditions Check

            foreach (XmlNode commandOption in commandOptionList)
            {
                bool ValidOption = true;


                // Conditions: if all conditions are met this option is shown
                if (commandOption.SelectSingleNode("Conditions") != null)
                {
                    string rInner = commandOption.SelectSingleNode("Conditions").InnerText;
                    rInner = rInner.Trim();

                    string[] Conditions = rInner.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string condition in Conditions)
                    {
                        //solve
                        string pCondition = GetProperty(condition.Trim());
                        pCondition = SolveStringLinking(pCondition);

                        if (ValidOption)
                        {
                            ValidOption = StringCalculator.Compare(pCondition);
                        }

                        //Last Judgement
                        if (condition == Conditions.Last() && ValidOption)
                        {
                            includedOptions.Add(commandOption.Attributes["Tag"].InnerText);

                            if (commandOption.SelectSingleNode("Input") != null)
                            {
                                hasInput = true;
                            }
                            if (commandOption.SelectSingleNode("Roll") != null)
                            {
                                hasRoll = true;
                            }

                        }
                    }
                }
                // default: there are 0 conditions so always active
                else
                {
                    includedOptions.Add(commandOption.Attributes["Tag"].InnerText);

                    if (commandOption.SelectSingleNode("Input") != null)
                    {
                        hasInput = true;
                    }
                    if (commandOption.SelectSingleNode("Roll") != null)
                    {
                        hasRoll = true;
                    }
                }
            }

            #endregion

            #region Input
            if (hasInput)
            {
                List<string> inputOptions = new List<string>();
                List<string> inputOptionsTag = new List<string>();
                Dictionary<string, string> ClickDic = new Dictionary<string, string>();
                Dictionary<string, string> TypeDic = new Dictionary<string, string>();

                Dictionary<string, string> inputTagDic = new Dictionary<string, string>();
                bool isInput = false;

                foreach (XmlNode commandOption in commandOptionList)
                {
                    // Checks if branchTag is included
                    if (includedOptions.Contains(commandOption.Attributes["Tag"].InnerText))
                    {
                        #region Typeable Input
                        if (commandOption.SelectSingleNode("Input").Attributes["Tag"].Value == "Type")
                        {
                            string rInner = commandOption.SelectSingleNode("Input").InnerText;
                            rInner = rInner.Trim();

                            string[] split = rInner.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                            inputOptions.AddRange(split);

                            for (int i = 0; i < split.Length; i++)
                            {
                                TypeDic.Add(commandOption.Attributes["Tag"].InnerText, split[i]);
                            }
                            isInput = true;
                        }
                        #endregion

                        #region Clickable Input
                        else if (commandOption.SelectSingleNode("Input").Attributes["Tag"].Value == "Click")
                        {
                            string rInner = commandOption.SelectSingleNode("Input").InnerText;
                            rInner = rInner.Trim();

                            ClickDic.Add(commandOption.Attributes["Tag"].InnerText, rInner);

                            isInput = true;
                        }
                        #endregion
                    }
                }

                InputHandle(ClickDic, TypeDic, commandOptionList);


            }
        }

        #endregion

        
        public void Branch2(XmlNodeList commandOL, string rollT)
        {
            List<string> resultIndexes = new List<string>();
            rollTag = rollT;
            XmlNodeList commandOptionList = commandOL;

            #region Roll
            if (hasRoll)
            {
                int diceRoll = dice.Next(0, 7);
                resultIndexes.Clear();
                XmlNode processNode = null;


                foreach (XmlNode commandOption in commandOptionList)
                {

                    // select if node should be processed
                    if (commandOption.Attributes["Tag"].InnerText == rollTag)
                    {
                        processNode = commandOption.SelectSingleNode("Roll");
                    }

                    else if (rollTag == "" && includedOptions.Contains(commandOption.Attributes["Tag"].InnerText))
                    {
                        processNode = commandOption.SelectSingleNode("Roll");
                    }

                    // processes node 
                    if (processNode != null)
                    {
                        string rInner = processNode.InnerText;
                        rInner = rInner.Trim();

                        string[] splitConditions = rInner.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);

                        bool firstTime = true;
                        string rString = "";

                        for (int i = 0; i < splitConditions.Count(); i++)
                        {
                            string pCondition = splitConditions[i].Trim();

                            if (pCondition.Contains("Dice"))
                            {
                                pCondition = pCondition.Replace("Dice", diceRoll + "");
                            }

                            pCondition = GetProperty(pCondition);

                            if (StringCalculator.Compare(pCondition))
                            {
                                if (firstTime)
                                {
                                    firstTime = false;
                                    rString += rollTag;
                                }
                                rString += ("-" + i);
                            }
                        }
                        resultIndexes.Add(rString);

                        processNode = null;
                    }
                }
            }

            else
            {
                resultIndexes.Clear();
                resultIndexes.Add(rollTag);
            }

            #endregion

            #region Result
            if (hasRoll)
            {
                foreach (string resultString in resultIndexes)
                {
                    string[] trueConditions = resultString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] sResults = null;

                    foreach (XmlNode commandOption in commandOptionList)
                    {
                        if (commandOption.Attributes["Tag"].InnerText == trueConditions[0])
                        {
                            string rInner = commandOption.SelectSingleNode("Result").InnerText;
                            rInner = rInner.Trim();

                            sResults = rInner.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                            break;
                        }
                    }

                    trueConditions = trueConditions.Skip(1).ToArray();  // removes tag from array

                    foreach (string index in trueConditions)
                    {
                        string rResult = sResults[int.Parse(index)];

                        // result solve
                        SetValueProperty(rResult);
                        rResult = SolveStringLinking(rResult);
                    }

                }
            }
            else if (hasInput)
            {
                string[] results = null;

                foreach (XmlNode commandOption in commandOptionList)
                {
                    if (commandOption.Attributes["Tag"].InnerText == rollTag)
                    {
                        string rInner = commandOption.SelectSingleNode("Result").InnerText;
                        rInner = rInner.Trim();

                        results = rInner.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                        break;

                    }
                }

                foreach (string result in results)
                {
                    string rResult = result.Trim();
                    //result solve
                    SetValueProperty(rResult);
                    rResult = SolveStringLinking(rResult);
                }
            }
            #endregion
            resetHandle();
        }
    

    #endregion

        #region Help-functions

    #region Get/Set Functions

    private string GetProperty(string callString)
        {
            bool Active = true;
            string pString = callString;

            while (Active)
            {
                foreach (PropertyInfo cInfo in cManager.GetType().GetProperties())
                {
                    if (pString.Contains(cInfo.Name))
                    {
                        int indexStart = pString.IndexOf(cInfo.Name);
                        int indexEnd = pString.IndexOfAny(new char[] { ' ' }, indexStart);
                        if (indexEnd == -1)
                        {
                            indexEnd = pString.Length;
                        }
                        string isolatedPropertyCall = pString.Substring(indexStart, (indexEnd - indexStart));
                        string property = isolatedPropertyCall.Replace(cInfo.Name + ".", "");
                        object character = cInfo.GetValue(cManager);


                        PropertyInfo pInfo = character.GetType().GetProperty(property);
                        string value = pInfo.GetValue(character) + "";

                        if (pString.Contains("//"))
                        {
                            isolatedPropertyCall = "//" + isolatedPropertyCall;
                        }
                        pString = pString.Replace(isolatedPropertyCall, value);
                        Active = true;
                    }
                    else
                    {
                        Active = false;
                    }
                }
            }

            return pString;
        }

        private string[] GetPropertyArray(string callString)
        {
            bool Active = true;
            string pString = callString;
            string property = "";
            object character = null;

            while (Active)
            {
                foreach (PropertyInfo cInfo in cManager.GetType().GetProperties())
                {
                    if (pString.Contains(cInfo.Name))
                    {
                        int indexStart = pString.IndexOf(cInfo.Name);
                        int indexEnd = pString.IndexOfAny(new char[] { ' ' }, indexStart);
                        if (indexEnd == -1)
                        {
                            indexEnd = pString.Length;
                        }
                        string isolatedPropertyCall = pString.Substring(indexStart, (indexEnd - indexStart));
                        property = isolatedPropertyCall.Replace(cInfo.Name + ".", "");
                        character = cInfo.GetValue(cManager);


                        PropertyInfo pInfo = character.GetType().GetProperty(property);
                        string value = pInfo.GetValue(character) + "";

                        pString = pString.Replace(isolatedPropertyCall, value);
                        Active = true;
                    }
                    else
                    {
                        Active = false;
                    }
                }
            }

            return new string[] { character.GetType().Name, property, pString };
        }

        private void SetProperty(string Character, string Property, dynamic newValue)
        {
            PropertyInfo cInfo = cManager.GetType().GetProperty(Character);
            object character = cInfo.GetValue(cManager);
            if (cInfo != null)
            {
                PropertyInfo pInfo = character.GetType().GetProperty(Property);
                if (pInfo != null)
                {
                    if (pInfo.PropertyType.Name.Contains("Int"))
                    {
                        pInfo.SetValue(character, (int)newValue);
                    }
                    else
                    {
                        pInfo.SetValue(character, newValue);
                    }
                }
            }
        }

        private void SetValueProperty(string callString)
        {
            foreach (string op in transformOperators)
            {
                if (callString.Contains(op))
                {
                    string[] sCall = callString.Split(transformOperators, StringSplitOptions.RemoveEmptyEntries);
                    string argument = GetProperty(sCall[1]);

                    float pArgument = StringCalculator.Solve(argument);

                    string[] itemArray = GetPropertyArray(sCall[0]);
                    string pOp = op.Replace("=", "");

                    double newValue = StringCalculator.Solve(itemArray[2] + pOp + pArgument);

                    SetProperty(itemArray[0], itemArray[1], newValue);
                    break;
                }
            }
        }
        #endregion

        public static void SetInputString(string input)
        {
            WindowInput = input;
        }


        private string SolveStringLinking(string callString)
        {
            if (callString.Contains("Link"))
            {
                int indexStart = callString.IndexOf("Link");
                int indexEnd = callString.IndexOf(')');
                string linkString = callString.Substring(indexStart, (indexEnd - indexStart));
                linkString = linkString.Replace(" ", "");
                linkString = linkString.Replace("Link(", "");
                nextEI = int.Parse(linkString);
                EnabledEI.Add(nextEI);
                GameManager.CurrentEventI = nextEI;
                GameManager.gameUpdate = GameUpdate.NodeChange;
                AutoNode = false;
                return callString;
            }
            else if (callString.Contains("To"))
            {
                int indexStart = callString.IndexOf("To");
                int indexEnd = callString.IndexOf(')');
                string linkString = callString.Substring(indexStart, (indexEnd - indexStart));
                linkString = linkString.Replace(" ", "");
                linkString = linkString.Replace("To(", "");
                nextCI = int.Parse(linkString);
                EnabledCI.Add(nextCI);
                GameManager.gameUpdate = GameUpdate.NextCommand;
                return callString;
            }
            if (callString.Contains("Enable"))
            {
                int indexStart = callString.IndexOf("Enable");
                int indexEnd = callString.IndexOf(')');
                string linkString = callString.Substring(indexStart, (indexEnd - indexStart));
                linkString = linkString.Replace(" ", "");
                linkString = linkString.Replace("Enable(", "");
                EnabledCI.Add(int.Parse(linkString));
                EnabledEI.Add(int.Parse(linkString));
                return callString;
            }

            if (callString.Contains("isEI"))
            {
                int indexStart = callString.IndexOf("isEI(");
                int indexEnd = callString.IndexOf(')');
                string linkString = callString.Substring(indexStart, (indexEnd - indexStart));
                linkString = linkString.Replace(" ", "");
                linkString = linkString.Replace("isEI(", "");

                bool condition = EnabledEI.Contains(int.Parse(linkString));
                string replaceString;

                if (condition)
                {
                    replaceString = callString.Replace(("isEI(" + linkString + ")"), 1 + "");
                }
                else
                {
                    replaceString = callString.Replace(("isEI(" + linkString + ")"), 0 + "");
                }
                return replaceString;
            }
            else if (callString.Contains("isCI"))
            {
                int indexStart = callString.IndexOf("isCI(");
                int indexEnd = callString.IndexOf(')');
                string linkString = callString.Substring(indexStart, (indexEnd - indexStart));
                linkString = linkString.Replace(" ", "");
                linkString = linkString.Replace("isCI(", "");

                bool condition = EnabledCI.Contains(int.Parse(linkString));
                string replaceString;

                if (condition)
                {
                    replaceString = callString.Replace(("isCI(" + linkString + ")"), 1 + "");
                }
                else
                {
                    replaceString = callString.Replace(("isCI(" + linkString + ")"), 0 + "");
                }
                return replaceString;
            }
            else
            {
                return callString;
            }
        }

        private string SlashCommands(string callString)
        {
            bool Active = true;
            string pString = callString;

            while (Active)
            {
                if (pString.Contains("//"))
                {
                    pString = GetProperty(pString);

                    //if (pString.Contains("//Break"))
                    //{
                    //    int sIndex = pString.IndexOf("//Break");
                    //    int eIndex = pString.IndexOf(")", sIndex);
                    //    string iBreak = pString.Substring(sIndex, eIndex - sIndex + 1);

                    //    string[] sBreak = pString.Split(new string[] { iBreak }, StringSplitOptions.None);

                    //    for (int i = 0; i < sBreak.Count(); i++)
                    //    {
                    //        Console.Write(sBreak[i]);
                    //        Console.ReadKey();
                    //    }
                    //    pString = "";
                    //}
                }

                else
                {
                    Active = false;
                }
            }
            return pString;

        }

        #endregion

    }
}
