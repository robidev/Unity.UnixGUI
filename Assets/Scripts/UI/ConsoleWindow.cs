using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleWindow : MonoBehaviour
{
    private TMP_InputField TextField = null;
    private string TextVariable = "";

    void Awake()
    {
        TextField = GetComponent<TMP_InputField>();
        PrintText("bin-bash: # ",true);
        TextField.ActivateInputField();
        TextField.stringPosition = TextVariable.Length;
    }

    void OnEnable()
    {
        if(TextField != null)
        {
            TextField.ActivateInputField();
            TextField.stringPosition = TextVariable.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TextField.stringPosition < TextVariable.Length)
        {
            TextField.stringPosition = TextVariable.Length;
        }
    }

    public void filterInput(string text)
    {
        if(text.Length <= TextVariable.Length)
        {
            TextField.text = TextVariable;
            return;
        }
        text = text.Replace("\r","");
        int totalFinds = 0;
        int found = 0;
        int oldTextLenght = TextVariable.Replace("\r","").Length;
        for (int i = oldTextLenght-1; i < text.Length; i++)
        {
            found = text.IndexOf("\n", i);
            if (found > 0)
            {
                totalFinds++;
                i = found; 
                OnEnter(text.Substring(0,found));
                oldTextLenght = TextVariable.Replace("\r","").Length;
            }
        }
    }

    public void OnEnter(string text)
    {
        //print comand
        string input = text.Substring(TextVariable.Length);
        PrintText(input + "\n");

        //handle command
        if(input == "ls")
        {
            PrintText(".\n..");
        }
        else if(input == "exit")
        {
            CloseSession();
        }
        else if(input == "help")
        {
            PrintText("Example shell, version 1.0.0(1)-release\n" +
             "These shell commands are defined internally.  Type `help' to see this list.\n" +
             "\nA star (*) next to a name means that the command is disabled.\n\n" +
             "  *cd [dir]\tchange current directory\n" +
             "  exit\t\tclose this prompt\n" +
             "  help\t\tprint this help\n" +
             "  ls\t\tlist the current directory\n" +
             "  sudo [command]\texecute command as super user\n");
        }
        else if(input == "make")
        {
            PrintText("what would you want to make?");
        }
        else if(input == "make me a sandwich")
        {
            PrintText("What? Make it yourself");
        }
        else if(input.StartsWith("sudo"))
        {
            if(input == "sudo")
            {
                PrintText("what are you trying to sudo?");
            }
            else if(input.StartsWith("sudo "))
            {
                string subcommand = input.Substring(5);
                if(subcommand == "make me a sandwich!")
                {
                    PrintText("Okay");
                }
                else
                {
                    PrintText("You cannot sudo '" + subcommand + "'");
                }
            }
        }
        else
        {
            PrintText(input + ": command not found");
        }
        //print commandline
        PrintText("\nbin-bash: # ");
        TextField.ActivateInputField();
    }

    void PrintText(string text, bool clear = false)
    {
        if(clear == true)
        {
            TextVariable = text;
        }
        else
        {
            TextVariable += text;
        }
        TextField.text = TextVariable;
    }

    public void CloseSession()
    {
        PrintText("bin-bash: # ",true);
        transform.parent.gameObject.SetActive(false);
    }
}
