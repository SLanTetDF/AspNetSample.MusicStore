using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetSampleMusicStore.Infra
{
    public class MaxWordsAttribute:ValidationAttribute
    {
        private readonly int myMaxWords;
        public MaxWordsAttribute(int maxWords)
        {
            myMaxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                var valueAsString = value.ToString();
                if(valueAsString.Split(' ').Length>myMaxWords)
                {
                    return new ValidationResult("To many words");
                }
            }
            return ValidationResult.Success ;
        }
    }
}