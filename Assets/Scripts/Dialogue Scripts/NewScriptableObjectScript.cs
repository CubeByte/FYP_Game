using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    private static List<WordPair> wordPairList = new 
    (new List<WordPair>
        {
            new WordPair("cube", "cube", false),
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
        }
    );
    public static void setIsKnown(string word)
    {
        foreach (WordPair wordPair in wordPairList)
        {
            if (word == wordPair.English)
            {
                wordPair.IsKnown = true;
                Debug.Log(wordPair.English + " is now known");
            }
        }
        Debug.Log(word + " is not known");
    }
    public void ResetWordPairList()
    {
        foreach (WordPair wordPair in wordPairList)
        {
            wordPair.IsKnown = false;
        }
    }

    public string ReturnWordPairAsList()
    {
        foreach (WordPair wordPair in wordPairList)
        {
            if (wordPair.IsKnown)
                return wordPair.Polish + " is " + wordPair.English;
        }
        return "???";
    }

    public string ReturnWordPairInPosition(int position)
    {
        if (wordPairList[position].IsKnown)
        {
            return wordPairList[position].Polish + " is " + wordPairList[position].English;
        }
        return "???";
    }
}
