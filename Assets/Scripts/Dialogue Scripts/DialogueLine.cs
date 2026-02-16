using System.Collections;
using System.Collections.Generic;

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
                        if (text.Equals(word.English) && word.IsKnown)
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
                                if (text.Equals(word.English) && word.IsKnown)
                                {
                                        return word.English;
                                }

                                if (text.Equals(word.English) && !word.IsKnown)
                                {
                                        return "???";
                                }
                        }
                        return text;
                }
                foreach (var word in Words)
                {
                        if (text.Equals(word.English))
                        {
                                return word.Polish;
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