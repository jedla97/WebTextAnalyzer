
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.Web.Mvc.CompareAttribute;

namespace WebTextAnalyzer.Models
{
    /**
     * number of char                                           x
     * number of char without whitespace                        x
     * number of char without digits                            x
     * number of digit                                          x
     * number of words                                          x
     * number of sentences                                      x
     * longest words                                            x
     * shortest word                                            x
     * most commons word and their number ocurencies            -
     * number of different word                                 x    
     * average number of character per word                     x       
     * number of syllable                                       x
     * number of syllable per word                              x
     */
    public class Form
    {
        [DataType(DataType.MultilineText)]
        public string Input { get; set; }
        public string Output { get; private set; }
        private int NumberOfChar { get; set; }
        private int NumberOfCharWithoutWhiteSpace { get; set; }
        private int NumberOfCharWithoutSpaceAndDigit { get; set; }
        private int NumberOfWords { get; set; }
        private int NumberOfSentences { get; set; }
        public List<string> ListOfLongestWords { get; set; }
        public List<string> ListOfShortestWords { get; set; }
        public Dictionary<string, int> MostCommonWords { get; set; }
        private int NumberOfDifferentWords { get; set; }
        private int NumberOfSyllables { get; set; }
        private double AverageCharPerWord { get; set; }
        private double AverageSyllablePerWord { get; set; }


        public List<string> Result { get; set; }


        public Form()
        {
            Result = new List<string>();
            ListOfLongestWords = new List<string>();
            ListOfShortestWords = new List<string>();
        }

        public void ComputeStats()
        {
            TextOperation textOperations = new TextOperation(Input);
            NumberOfChar = textOperations.CountCharacters();
            NumberOfCharWithoutWhiteSpace = textOperations.CountCharWithoutSpace();
            NumberOfCharWithoutSpaceAndDigit = textOperations.CountCharWithoutSpaceAndDigit();
            NumberOfWords = textOperations.CountWords();
            NumberOfSentences = textOperations.CountSentences();
            ListOfLongestWords = textOperations.ListOfLongestWords();
            ListOfShortestWords = textOperations.ListOfShortestWords();
            NumberOfSyllables = textOperations.CountSyllable();

            // next two items must be in this order otherwise NumberOfDifferntWords be 0
            MostCommonWords = textOperations.MostCommonWords();
            NumberOfDifferentWords = textOperations.NumberOfDifferentWords();

            AverageCharPerWord = (double)NumberOfCharWithoutWhiteSpace / NumberOfWords;
            AverageSyllablePerWord = (double)NumberOfSyllables / NumberOfWords;

            this.CheckIfAverageIsNumber();
            this.FillOutput();
            this.SetOutputOfMaxLenght();
        }

        private void FillOutput()
        {
            Result.Add(String.Format("Word count: {0}", NumberOfWords));
            Result.Add(String.Format("Different word count: {0}", NumberOfDifferentWords));
            Result.Add(String.Format("Total character count: {0}", NumberOfChar));
            Result.Add(String.Format("Character count (spaces not included): {0}", NumberOfCharWithoutWhiteSpace));
            Result.Add(String.Format("Character count (spaces and digits not included): {0}", NumberOfCharWithoutSpaceAndDigit));
            Result.Add(String.Format("Number of digits: {0}", (NumberOfCharWithoutWhiteSpace - NumberOfCharWithoutSpaceAndDigit)));
            Result.Add(String.Format("Sentence count: {0}", NumberOfSentences));
            Result.Add(String.Format("Syllable count: {0}", NumberOfSyllables));
            Result.Add(String.Format("Average character per word: {0:F2}", AverageCharPerWord));
            Result.Add(String.Format("Average syllable per word: {0:F2}", AverageSyllablePerWord));
        }

        private void SetOutputOfMaxLenght()
        {
            if(Input.Length <= 500)
            {
                Output = Input;
            }
            else
            {
                Output = String.Format("{0}...", Input.Substring(0, 500));
            }
        } 

        // when not a number change it to 0
        private void CheckIfAverageIsNumber()
        {
            if (Double.IsNaN(AverageCharPerWord) || Double.IsInfinity(AverageCharPerWord))
            {
                AverageCharPerWord = 0;
            }
            if (Double.IsNaN(AverageSyllablePerWord) || Double.IsInfinity(AverageSyllablePerWord))
            {
                AverageSyllablePerWord = 0;
            }
        }

    }

}