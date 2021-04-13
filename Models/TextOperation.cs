using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebTextAnalyzer.Models
{
    public class TextOperation
    {
        public StringBuilder Text { get; set; }


        public int NumberOfCharacters()
        {

            return Text.Length;
        }

    }
}