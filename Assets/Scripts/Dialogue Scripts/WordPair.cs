public class WordPair
{
       public string Known { get; set; }
       public string Unknown { get; set; }
       public bool IsKnown { get; set; }

       public WordPair(string known, string unknown, bool isKnown)
       {
              Known = known;
              Unknown = unknown;
              IsKnown = isKnown;
       }
}