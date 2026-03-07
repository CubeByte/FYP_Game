using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    private static List<WordPair> wordPairList = new ()
        {
            new ("cube", "cube", false),
            new ("yes", "tak",true),
            new ("i", "ya",false),
            new ("may", "może",true),
            new ("kill", "zabić",true),
            new ("you", "cię",false),
            new ("different", "różny",true),
            new ("words", "słowa",false), 
            new ("that", "żeby",false),
            new ("added", "dodałem",true),
            new ("here", "tutaj",false),
            new ("this", "to",true),
            new ("is", "jest",true), 
            new ("an", "na",false), 
            new ("example", "przykład",true),
            new ("slash", "tnący",true),
            new ("blunt", "tępy",false),
            new ("pierce", "przebijający",true),
            new ("holy", "święty",true),
            new ("undead", "nieumarły",true),
            new ("item", "przedmiot",true),
            new ("fire", "ogień",true),
            new ("water", "woda",true),
            new ("magic", "magia",true),
            
        };
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

    public bool WordIsKnown(string word)
    {
        foreach (WordPair wordPair in wordPairList)
        {
            if (word == wordPair.English && wordPair.IsKnown)
                return true;
        }
        return false;
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

    public string ReturnWordPair(string English)
    {
        foreach (var word in wordPairList)
        {
            if (word.English == English)
            {
                return word.Polish;
            }
        }
        return null;
    }
}
