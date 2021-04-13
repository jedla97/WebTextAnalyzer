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
    public class Form
    {
        [DataType(DataType.MultilineText)]
        public string Input { get; set; }
        public List<int> Result { get; set; }

        private TextOperation textOperations = new TextOperation();

        public Form()
        {
            Result = new List<int>();
        }

        public void Compute()
        {
            textOperations.Text = new StringBuilder(Input);
            Result.Add(textOperations.NumberOfCharacters());
            Result.Add(textOperations.NumberOfCharacters());
        }

    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}