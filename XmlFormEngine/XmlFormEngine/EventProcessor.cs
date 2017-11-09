using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using TextProcessing;

namespace XmlFormEngine
{
    public class EventProcessor
    {

        #region Private Properties

        List<string> includedOptions = new List<string>();
        List<string> usedOptions = new List<string>();
        List<int> EnabledEI = new List<int>();
        List<int> EnabledCI = new List<int>();

        Game game;
        public delegate string LinkInfoHandle(string CallString);
        LinkInfoHandle link_InfoCheck;
        CharacterManager cManager = new CharacterManager();

        string[] splitCharacters = new string[] { "\r\n" };
        string[] transformOperators = new string[] { "==", "+=", "-=", "/=", "*=" };

        Random dice = new Random();

        #endregion

        #region Public functions

        public EventProcessor(Game game)
        {
            this.game = game;
            link_InfoCheck = new LinkInfoHandle(game.LinkingInformationLoad);
            BranchPresets.Load(@"..\..\XmlResources\Misc\BranchPresets.xml");
        }

        #endregion
        

        XmlDocument BranchPresets = new XmlDocument();

        #region Commands

        public List<string> Print_PreProcess(XmlNodeList Print_List)
        {
            List<string> preProcessed = new List<string>();
            string windowText = "";

            foreach (XmlNode XmlString in Print_List)
            {
                #region Enable/Disable nodes
                if (XmlString.Name == "Enable")
                {
                    if (XmlString.Attributes["EI"] != null)
                    {
                        if (EnabledEI.Contains(int.Parse(XmlString.Attributes["EI"].Value)))
                        {
                            string pText = XmlString.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = GetProperty(pText);
                            preProcessed.Add(pText);

                            windowText += pText;
                        }
                    }
                    if (XmlString.Attributes["CI"] != null)
                    {
                        if (EnabledCI.Contains(int.Parse(XmlString.Attributes["CI"].Value)))
                        {
                            string pText = XmlString.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = GetProperty(pText);
                            preProcessed.Add(pText);

                            windowText += pText;
                        }
                    }
                }
                else if (XmlString.Name == "Disable")
                {
                    if (XmlString.Attributes["EI"] != null)
                    {
                        if (!EnabledEI.Contains(int.Parse(XmlString.Attributes["EI"].Value)))
                        {
                            string pText = XmlString.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = GetProperty(pText);
                            preProcessed.Add(pText);

                            windowText += pText;
                        }
                    }
                    if (XmlString.Attributes["CI"] != null)
                    {
                        if (!EnabledCI.Contains(int.Parse(XmlString.Attributes["CI"].Value)))
                        {
                            string pText = XmlString.InnerText.TrimStart();
                            pText = pText.TrimEnd(new char[] { ' ' });
                            pText = GetProperty(pText);
                            preProcessed.Add(pText);

                            windowText += pText;
                        }
                    }
                }
                #endregion

                else
                {
                    string pText = XmlString.InnerText.TrimStart();
                    pText = pText.TrimEnd(new char[] { ' ' });
                    pText = GetProperty(pText);
                    preProcessed.Add(pText);

                    windowText += pText;
                }

                if (windowText.Contains("//BREAK//"))
                {
                    string[] breakSplit = windowText.Split(new string[] { "//BREAK//" }, StringSplitOptions.RemoveEmptyEntries);
                    preProcessed.AddRange(breakSplit);
                    windowText = "";
                }
            }
            return preProcessed;
        }

        #region Branch
        public XmlNodeList Condition_Check(XmlNodeList option_XmlList, out bool input_Available, out bool roll_Available)
        {
            input_Available = false;
            roll_Available = false;
            XmlNodeList option_ReturnList = option_XmlList;

            for(int i = 0; i<option_ReturnList.Count; i++)
            {
                XmlNode option_SingleNode = option_ReturnList[i];

                if (option_SingleNode.SelectSingleNode("Conditions") != null)
                {
                    bool option_Validate = false;
                    string[] option_Conditions = option_SingleNode.SelectSingleNode("Conditions").
                                                 InnerText.Split(splitCharacters,
                                                 StringSplitOptions.RemoveEmptyEntries);

                    foreach (string condition in option_Conditions)
                    {
                        //solve
                        string pCondition = GetProperty(condition.Trim());
                        pCondition = link_InfoCheck.Invoke(pCondition);

                        // check list if condition is valid this time
                        if (option_Validate)
                        {
                            option_Validate = StringCalculator.Compare(pCondition);
                        }

                        //Removes invalid option and breaks condition checking
                        if (!option_Validate)
                        {
                            option_ReturnList[i].ParentNode.RemoveChild(option_ReturnList[i]);
                            break;
                        }
                    }
                }
                // checks if Roll and Input are in the option
                if(option_SingleNode.SelectSingleNode("Input") != null)
                {
                    input_Available = true;
                }
                if(option_SingleNode.SelectSingleNode("Roll")!=null)
                {
                    roll_Available = true;
                }
                
                
            }
            return option_ReturnList;
        }

        public void Input_SetTrigger(XmlNodeList option_XmlList, ref EnterHandling enterHandle, ref Dictionary<string, string> Opt_type,ref RichTextBox Opt_TypeBox,
                                                                 ref Label Opt_A, ref Label Opt_B, ref Label Opt_C, ref Label Opt_D)
        {
            Opt_type.Clear();

            foreach (XmlNode option_SingleNode in option_XmlList)
            {
                XmlNode option_inputNode = option_SingleNode.SelectSingleNode("Input");
                if (option_inputNode.Attributes["Tag"].Value == "Click")
                {
                    switch (option_SingleNode.Attributes["Tag"].Value)
                    {
                        case "A":
                            Opt_A.Text = option_inputNode.InnerText.Trim();
                            Opt_A.Visible = true;
                            Opt_A.Enabled = true;
                            break;
                        case "B":
                            Opt_B.Text = option_inputNode.InnerText.Trim();
                            Opt_B.Visible = true;
                            Opt_B.Enabled = true;
                            break;
                        case "C":
                            Opt_C.Text = option_inputNode.InnerText.Trim();
                            Opt_C.Visible = true;
                            Opt_C.Enabled = true;
                            break;
                        case "D":
                            Opt_D.Text = option_inputNode.InnerText.Trim();
                            Opt_D.Visible = true;
                            Opt_D.Enabled = true;
                            break;
                        default:
                            throw new Exception("InputNode: " + option_SingleNode.Attributes["Tag"].Value + ": Is not a valid Tag in context");
                    }
                }
                else if (option_inputNode.Attributes["Tag"].Value == "Type")
                {
                    if (!Opt_TypeBox.Enabled)
                    {
                        enterHandle = EnterHandling.confirmInput;
                        Opt_TypeBox.Enabled = true;
                        Opt_TypeBox.Visible = true;
                    }

                    string option_Tag = option_SingleNode.Attributes["Tag"].Value;
                    string rawTypeConditions = option_inputNode.InnerText.Replace(" ", "");
                    string[] option_typeConditions = rawTypeConditions.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string condition in option_typeConditions)
                    {
                        string pCondition = condition.ToUpper();

                        Opt_type.Add(pCondition, option_Tag);
                    }
                }
            }
        }

        public XmlNodeList Roll_Solve(XmlNodeList option_XmlList, out string Result_Info)
        {
            Result_Info = ""; // Set + index info; if single: set = N
            int diceRoll = -1;
            int returnIndex = -1;
            XmlNodeList option_ReturnList = option_XmlList;

            for (int k = 0; k < option_XmlList.Count; k ++)
            {
                XmlNode option_SingleNode = option_XmlList[k];

                XmlNodeList option_Roll = option_SingleNode.SelectSingleNode("Roll").ChildNodes;
                string[] Roll_Set = null;
                int set_Index = -1;

                #region Multiple-Rollset
                if (option_Roll[0].Name == "SetCondition")
                {
                    // Check which set is to be loaded
                    string[] set_Conditions = option_Roll[0].InnerText.Trim().Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < set_Conditions.Count(); i++)
                    {
                        //solve
                        string pCondition = GetProperty(set_Conditions[i].Trim());
                        pCondition = link_InfoCheck(pCondition);

                        bool Valid = StringCalculator.Compare(pCondition);

                        // returns first valid set
                        if (Valid)
                        {
                            set_Index = i + 1;
                            break;
                        }
                    }
                    // if any set is valid; solve conditions
                    if (set_Index != -1)
                    {
                        // check if set_Index points to a pre-set
                        if (option_Roll[set_Index].Name.Contains("S_"))
                        {
                            string p_Name = option_Roll[set_Index].Name;
                            string raw_Set = BranchPresets.SelectSingleNode("/Game/Roll/" + p_Name).InnerText.Replace(" ", "");
                            Roll_Set = raw_Set.Split(splitCharacters,StringSplitOptions.RemoveEmptyEntries);

                        }
                        else
                        {
                            string raw_Set = option_Roll[set_Index].InnerText.Replace(" ", "");
                            Roll_Set = raw_Set.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                        }


                        bool firstTime = true;
                        string rString = "";
                        // Check conditions in Roll_Set
                        for (int j = 0; j < Roll_Set.Count(); j++)
                        {
                            string condition = Roll_Set[j].Trim();

                            // solve condition

                            while (condition.Contains("D("))
                            {
                                int first = condition.IndexOf("D(") + 2;
                                int length = condition.IndexOf(")", first) - first;
                                int eyeMax = int.Parse(condition.Substring(first,length));
                                diceRoll = dice.Next(1, eyeMax + 1); 
                                condition = condition.Replace("D(" + eyeMax + ")", diceRoll + "");
                            }

                            condition = GetProperty(condition);

                            if (StringCalculator.Compare(condition))
                            {
                                if (firstTime)
                                {
                                    firstTime = false;
                                    rString += option_Roll[set_Index].Name;
                                }
                                rString += ("-" + j);
                            }
                        }
                        // if any result is valid stop foreach
                        if (rString != "")
                        {
                            Result_Info = rString;
                            returnIndex = k;
                            break;
                        }

                    }

                }
                #endregion

                #region Single-Rollset
                else
                {
                    bool firstTime = true;
                    string rString = "";
                    for (int j = 0; j<option_Roll.Count;j++)
                    {
                        string condition = option_Roll[j].InnerText.Trim();

                        // solve condition

                        if (condition.Contains("Dice"))
                        {
                            condition = condition.Replace("Dice", diceRoll + "");
                        }

                        condition = GetProperty(condition);

                        if (StringCalculator.Compare(condition))
                        {
                            if (firstTime)
                            {
                                firstTime = false;
                                rString += "N";
                            }
                            rString += ("-" + j);
                        }

                    }
                    // if any result is valid stop foreach
                    if (rString != "")
                    {
                        Result_Info = rString;
                        returnIndex = k;
                        break;
                    }
                }
                #endregion
            }
            // remove unuses/false options
            for (int i = 0; i < option_ReturnList.Count; i++)
            {
                if (i != returnIndex)
                {
                    option_ReturnList[i].ParentNode.RemoveChild(option_ReturnList[i]);
                }
            }
            return option_ReturnList;
        }

        public void Result_Solve(XmlNodeList option_XmlList, ref string Result_Info, ref int EI_Current, ref int CI_Current, ref List<int> EI_Enabled)
        {
            if (option_XmlList.Count == 1)
            {
                XmlNodeList Result_NodeList = option_XmlList[0].SelectSingleNode("Result").ChildNodes;
                string[] Result_Set = null;
                string[] Result_InfoList = Result_Info.Split(new char[] { '-' });
                int ReturnedValue = -1;

                foreach (XmlNode ResultList_Node in Result_NodeList)
                {
                    if (Result_InfoList[0] == ResultList_Node.Name)
                    {
                        if (ResultList_Node.Name.Contains("S_"))
                        {
                            string p_Name = ResultList_Node.Name;
                            string raw_Set = BranchPresets.SelectSingleNode("/Game/Result/" + p_Name).InnerText.Replace(" ", "");
                            Result_Set = raw_Set.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                            break;

                        }
                        else
                        {
                            string raw_Set = ResultList_Node.InnerText.Replace(" ", "");
                            Result_Set = raw_Set.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                            break;
                        }
                    }
                    else if (Result_InfoList[0] == "N")
                    {
                        string raw_Set = Result_NodeList[0].InnerText.Replace(" ", "");
                        Result_Set = raw_Set.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
                }
                bool firstRound = true;

                foreach (string indexInfo in Result_InfoList)
                {
                    if (!firstRound)
                    {
                        string result = Result_Set[int.Parse(indexInfo)];
                        SetValueProperty(result.Trim());
                        ReturnedValue = EventIdentificationLinking(result.Trim(), ref EI_Current, ref CI_Current);

                        if (ReturnedValue != -1)
                        {
                            EI_Enabled.Add(ReturnedValue);
                        }

                    }
                    // To NOT process the Result_Set name as Result
                    else
                    {
                        firstRound = false;
                    }
                }

            }
            else
            {
                throw new Exception("Error: optionlist not one: " + option_XmlList.Count);
            }
        }
        #endregion

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
                        int indexEnd = pString.IndexOfAny(new char[] { ' ', '+', '-','*','/','>','<','=','!', }, indexStart);
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

                    string[] itemArray = GetPropertyArray(sCall[0]);//
                    double newValue = double.NaN;

                    string pOp = op;
                    if (pOp == "==")
                    {
                        newValue = pArgument;
                    }
                    else
                    {
                        pOp = op.Replace("=", "");
                        newValue = StringCalculator.Solve(itemArray[2] + pOp + pArgument);
                    }

                    

                    SetProperty(itemArray[0], itemArray[1], newValue);
                    break;
                }
            }
        }



        #endregion

  

        //
        private int EventIdentificationLinking(string call, ref int EI_Current, ref int CI_Current)
        {
            if (call.IndexOf('(') != -1)
            {
                string callID = call.Substring(call.IndexOf('('));
                string callIdentifier = call.Replace(callID, "").Trim();
                callID = callID.Replace("(", "");
                callID = callID.Replace(")", "");
                switch (callIdentifier)
                {
                    case "Link":
                        EI_Current = int.Parse(callID);
                        CI_Current = -1;
                        return int.Parse(callID);

                    case "Enable":
                        int EI_Enabled = int.Parse(callID);
                        return EI_Enabled;

                    case "To":
                        CI_Current = int.Parse(callID);
                        return -1;

                    default:
                        return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        // keep: usefull info
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
