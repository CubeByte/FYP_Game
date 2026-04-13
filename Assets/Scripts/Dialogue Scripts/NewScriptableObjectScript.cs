using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    private static List<WordPair> wordPairList = new ()
        {
            new ("heal", "leczyć",false),
            new ("slash", "tnący",false),
            new ("blunt", "tępy",false),
            new ("pierce", "przebić",false),
            new ("holy", "święty",false),
            new ("death", "śmierć",false),
            new ("fire", "ogień",false),
            new ("water", "woda",false),
            new ("magic", "magia",false),
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
    public string ReturnWordPairInPosition(int position)
    {
        if (position < 0 || position >= wordPairList.Count)
        {
            return null;
        }
        if (wordPairList[position].IsKnown)
        {
            return "'" + wordPairList[position].Polish + "' relates to the word: " + wordPairList[position].English;
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
