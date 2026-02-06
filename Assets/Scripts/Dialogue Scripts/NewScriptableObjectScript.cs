using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    
    [TextArea]
    public String text;
    
    private static DialogueLine dialogue = new DialogueLine(new List<WordPair>
    {
        new WordPair("yes", "tak",true),
        new WordPair("i", "ya",false),
        new WordPair("may", "może",true),
        new WordPair("kill", "zabić",true),
        new WordPair("you", "cię",false),
        new WordPair("different", "różny",true),
        new WordPair("words", "słowa",false),
        new WordPair("that", "żeby",false),
        new WordPair("added", "dodałem",true),
        new WordPair("here", "tutaj",false),
        new WordPair("this", "to",true),
        new WordPair("is", "jest",true),
        new WordPair("an", "na",false),
        new WordPair("example", "przykład",true),
    });

    public static void setIsKnown(string word)
    {
        foreach (WordPair wordPair in dialogue)
        {
            if (word == wordPair.Known)
            {
                wordPair.IsKnown = true;
            }
        }
    }
    public List<string> getText()
    {
        string[] stringSplit = text.Split(' ');
        List<string> hashText = new List<string>(stringSplit);
        return hashText;
    }

    public DialogueLine getDialogue()
    {
        return dialogue;
    }
}
