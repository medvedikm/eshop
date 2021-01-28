using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    public class UniqueCharactersAttribute : ValidationAttribute, IClientModelValidator
    {
        
        private readonly int uniqueChars;
        public UniqueCharactersAttribute(int uniqueChars)
        {
            this.uniqueChars = uniqueChars;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var count = value.ToString().Distinct().Count();

            if (count >= 6)
            {
                return ValidationResult.Success;
            }
            else 
            {
                return new ValidationResult($"Password must contain at least {uniqueChars} unique characters!", new List<String> { validationContext.MemberName });
            }
            throw new NotImplementedException($"The attribute {nameof(FileContentTypeAttribute)} is not implemented for object  {value.GetType()}. {nameof(IFormFile)} only is implemented.");
        }

        protected string GetErrorMessage()
        {
            return $"Password must contain at least {uniqueChars} unique characters!";
        }

        
        public void AddValidation(ClientModelValidationContext context)
        {
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-uniqchars", GetErrorMessage());
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-uniqchars-count", uniqueChars.ToString());
        }
        
    }
}
