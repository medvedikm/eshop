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
        
        private readonly int contentType;
        public UniqueCharactersAttribute(int contentType)
        {
            this.contentType = contentType;
        }
        /*
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // https://stackoverflow.com/questions/19480916/count-number-of-occurrences-for-each-char-in-a-string
            //var count = myString.Distinct().Count();

            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is IFormFile iff)
            {
                if (iff.ContentType.ToLower().Contains(contentType.ToLower()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage("File"), new List<string> { validationContext.MemberName });
                }
            }
            throw new NotImplementedException($"The attribute {nameof(FileContentTypeAttribute)} is not implemented for object  {value.GetType()}. {nameof(IFormFile)} only is implemented.");
        }

        protected string GetErrorMessage(string memberName)
        {
            return $"{ memberName } must be the type of {contentType}!";
        }

        */
        public void AddValidation(ClientModelValidationContext context)
        {
            //ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            //ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent", GetErrorMessage("File"));
            //ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent-type", contentType);
        }
        
    }
}
