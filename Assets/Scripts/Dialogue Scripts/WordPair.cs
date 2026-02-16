public class WordPair
{
       public string English { get; set; }
       public string Polish { get; set; }
       public bool IsKnown { get; set; }

       public WordPair(string known, string unknown, bool isKnown)
       {
              English = known;
              Polish = unknown;
              IsKnown = isKnown;
       }
}