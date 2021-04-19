using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebTextAnalyzer.Models
{
    /**
     * number of char                                           x
     * number of char without whitespace                        x
     * number of char without digits                            x
     * number of digit
     * number of words                                          x
     * number of sentences                                      x
     * longest words                                            x
     * shortest word                                            x
     * most commons word and their number ocurencies            
     * number of different word                                 
     * number of character per word                             
     * number of syllable                                       x
     * number of syllable per word                              
     */

    
    public class TextOperation
    {
        public string Text { get; private set; }
        private string[] Words { get; set; }
        public Dictionary<string, int> MostCommonWordsDictionary { get; set; }

        public TextOperation(string text)
        {
            this.Text = text;
            this.Words = Regex.Replace(text.Replace("\t", " ").Replace("\n", " "), @"([^a-zA-Z\s])", "").Split(' ');

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
            return Words.Length;
        }

        public int CountSentences()
        {
            char[] delimiterChars = { '.', '?', '!' };
            string[] row = Text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Split(delimiterChars);

            int counter = 0;
            for (int i = 0; i < row.Length - 2; i++)
            {
                if (i == 0 && Char.IsUpper(row[i][0]))
                {
                    counter++;
                }
                if (Char.IsUpper(row[i + 1][0]))
                {
                    counter++;
                }
            }
            return counter;
        }

        public List<string> ListOfLongestWords()
        {
            List<string> longestWords = new List<string>();
            int biggestLenght = 0;

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

            return longestWords;
        }

        public List<string> ListOfShortestWords()
        {
            List<string> longestWords = new List<string>();
            int biggestLenght = 0;

            foreach (var word in Words)
            {
                if (word.Length < biggestLenght)
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

            return longestWords;
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
            MostCommonWordsDictionary = dictionary;

            sortedDictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            var count = sortedDictionary.Count();
            dictionary.Clear();

            for (int i = 0; i < count && i < 10 ; i++)
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

    }
}