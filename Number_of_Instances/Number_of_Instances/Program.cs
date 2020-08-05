using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Number_of_Instances
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * PART A
             * Retrieve from URL the text
             */
            WebClient webClient = new WebClient();
            string content = webClient.DownloadString("https://archive.org/stream/LordOfTheRingsApocalypticProphecies/Lord%20of%20the%20Rings%20Apocalyptic%20Prophecies_djvu.txt");

            /*Since the file is given thus it is known what the text is we can adjust accordingly.
              If text was unknown with a link then general rules would be applied (not shown) to eliminated HTML tags
             */
            string startPoint = "* if *";
            string endPoint = "* y -*";

            /*
             * Retrieve index of starting point and the point which we wish to terminate
             */
            int indexStartingPoint = content.IndexOf(startPoint);
            int indexEndingPoint = content.IndexOf(endPoint)- indexStartingPoint;

            /*
             * The newly string will have just the text of interest
             */
            string text = content.Substring(indexStartingPoint, indexEndingPoint);


            /*
             * PART B
             * Optimizing text
             */


            string[] textArray = Regex.Split(text, @"\W+");

            Dictionary<string, int> wordOccurances = new Dictionary<string, int>();  
            
            for(int i=0; i < textArray.Length; i++)
            {
                if (wordOccurances.ContainsKey(textArray[i]))                           // Check if word is already in dictionary update the count  
                {
                    int value = wordOccurances[textArray[i]];
                    wordOccurances[textArray[i]] = value + 1;
                }
                else                                                                     // If we found the same word we just increase the count in the dictionary 
                {
                    wordOccurances.Add(textArray[i], 1);
                }
            }

            Console.WriteLine("The number of counts for each words are:");
            foreach (KeyValuePair<string, int> kvp in wordOccurances)
            {
                Console.WriteLine("Counts: " + kvp.Value + " for " + kvp.Key);   // Print the number of counts for each word

            }

            var sortedDict = (from entry in wordOccurances orderby entry.Value descending select entry)
               .ToDictionary(pair => pair.Key, pair => pair.Value).Take(10);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Top 10 words: ");
            foreach (var item in sortedDict)
            {
                
                Console.WriteLine(item);
            }
           
            Console.ReadLine();


            


        }
    }
}
