using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class DialogueLine : IEnumerable
{
        private List<WordPair> Words{get;set;}

        public DialogueLine(List<WordPair> words)
        {
                Words = words;
        }
        private bool CycleWordPairsForConfirmation(string text)
        {
                foreach (var word in Words)
                {
                        if (text.Equals(word.Known) && word.IsKnown)
                        {
                                return true;
                        }
                }
                return false;
        }
        private string CycleWordPairsToDisplay(string text, bool language)
        {
                if (language)
                {
                        foreach (var word in Words)
                        {
                                if (text.Equals(word.Known) && word.IsKnown)
                                {
                                        return word.Known;
                                }

                                if (text.Equals(word.Known) && !word.IsKnown)
                                {
                                        return "???";
                                }
                        }
                        return text;
                }
                foreach (var word in Words)
                {
                        if (text.Equals(word.Known))
                        {
                                return word.Unknown;
                        }
                }
                return "";
        }
        public string GetDisplayLineKnown(List<string> learnedWords)
        {
                List<string> displayLine = new List<string>();
                foreach (string word in learnedWords)
                {
                        if (CycleWordPairsForConfirmation(word))
                        {
                                displayLine.Add(CycleWordPairsToDisplay(word, true));
                        }
                        else
                        {
                                displayLine.Add(CycleWordPairsToDisplay(word, true));
                        }
                }
                return string.Join(" ", displayLine);
        }
        public string GetDisplayLineUnKnown(List<string> learnedWords)
        {
                List<string> displayLine = new List<string>();
                foreach (string word in learnedWords)
                {
                        if (CycleWordPairsForConfirmation(word))
                        {
                                displayLine.Add(CycleWordPairsToDisplay(word, false));
                        }
                        else
                        {
                                displayLine.Add(CycleWordPairsToDisplay(word, false));
                        }
                }
                return string.Join(" ", displayLine);
        }

        public IEnumerator GetEnumerator()
        {
                throw new System.NotImplementedException();
        }
}