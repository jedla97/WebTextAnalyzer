using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebTextAnalyzer.Models
{
    
    public class TextOperation
    {
        public string Text { get; private set; }
        private string[] Words { get; set; }
        public Dictionary<string, int> MostCommonWordsDictionary { get; set; }

        public TextOperation(string text)
        {
            this.Text = text;
            string textHelp = Regex.Replace(text.Replace("\t", " ").Replace("\n", " "), @"(\s[\w]{1}((?=\s)|(?=\.)|(?=\?)|(?=\!)))|([^\p{Ll}\p{Lu}\s])", "");
            textHelp = Regex.Replace(textHelp, @"(\s+)", " ");
            if (!Regex.Match(textHelp, @"\s{1}(?!\w)").Success)
            {
                this.Words = textHelp.Split(' ');
            }
            else
            {
                this.Words = null;
            }
            this.RemnoveEmptyString();

        }

        public int CountCharacters()
        {
            return Text.Length;
        }
        public int CountCharWithoutSpace()
        {
            return Text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Length;
        }
        public int CountCharWithoutSpaceAndDigit()
        {
            return Regex.Replace(Text.Replace(" ", "").Replace("\t", "").Replace("\n", ""), @"[\d]", string.Empty).Length;
        }

        public int CountWords()
        {
            if (Words == null)
            {
                return 0;
            }
            else
            {
                return Words.Length;
            }
        }

        public int CountSentences()
        {
            char[] delimiterChars = { '.', '?', '!' };
            string[] row = Text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Split(delimiterChars);

            int counter = 0;
            if (row.Count() <= 2 && !row[0].Equals(""))
            {
                if (Char.IsUpper(row[0][0]))
                {
                    counter++;
                }
            }
            else
            {
                for (int i = 0; i < row.Length - 2; i++)
                {
                    if (i == 0 && Char.IsUpper(row[i][0]))
                    {
                        counter++;
                    }
                    if (!row[i + 1].Equals(""))
                    {
                        if (Char.IsUpper(row[i + 1][0]))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }

        public List<string> ListOfLongestWords()
        {
            List<string> longestWords = new List<string>();
            int biggestLenght = 0;
            if (Words != null)
            {
                foreach (var word in Words)
                {
                    if (word.Length > biggestLenght)
                    {
                        biggestLenght = word.Length;
                        longestWords.Clear();
                        longestWords.Add(word);
                    }
                    else if (word.Length == biggestLenght && !longestWords.Contains(word))
                    {
                        longestWords.Add(word);
                    }
                }
            }
            return longestWords;
        }

        public List<string> ListOfShortestWords()
        {
            List<string> shortestWords = new List<string>();
            int shortestLenght = int.MaxValue;

            if (Words != null)
            {
                foreach (var word in Words)
                {
                    if (word.Length < shortestLenght)
                    {
                        shortestLenght = word.Length;
                        shortestWords.Clear();
                        shortestWords.Add(word);
                    }
                    else if (word.Length == shortestLenght && !shortestWords.Contains(word))
                    {
                        shortestWords.Add(word);
                    }
                }
            }

            return shortestWords;
        }

        //https://stackoverflow.com/questions/61033977/how-to-determine-syllables-in-a-word-by-using-regular-expression
        //https://codereview.stackexchange.com/questions/9972/syllable-counting-function
        public int CountSyllable()
        {
            string text = Text.ToLower().Trim();
            bool lastWasVowel = true;
            var vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
            int count = 0;

            //a string is an IEnumerable<char>; convenient.
            foreach (var character in text)
            {
                if (vowels.Contains(character))
                {
                    if (!lastWasVowel)
                        count++;
                    lastWasVowel = true;
                }
                else
                    lastWasVowel = false;
            }

            if ((text.EndsWith("e") || (text.EndsWith("es") || text.EndsWith("ed")))
                  && !text.EndsWith("le"))
                count--;

            return count;
        }

        public Dictionary<string, int> MostCommonWords()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Dictionary<string, int> sortedDictionary = new Dictionary<string, int>();
            if (Words != null)
            {
                foreach (var item in Words)
                {
                    if (dictionary.ContainsKey(item))
                    {
                        dictionary[item]++;
                    }
                    else
                    {
                        dictionary.Add(item, 1);
                    }
                }
            }
            MostCommonWordsDictionary = dictionary;

            sortedDictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            var count = sortedDictionary.Count();
            dictionary.Clear();

            for (int i = 0; i < count && i < 10; i++)
            {
                var item = sortedDictionary.ElementAt(i);
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }

        public int NumberOfDifferentWords()
        {
            return MostCommonWordsDictionary.Count();
        }

        private void RemnoveEmptyString()
        {
            if (Words != null)
            {

                var helpList = new List<string>(Words);
                for (int i = 0; i < helpList.Count; i++)
                {
                    if (helpList[i] == "")
                    {
                        helpList.RemoveAt(i);
                    }
                }
                Words = helpList.ToArray();
            }
        }

    }
}