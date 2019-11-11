using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace Data
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string tableName, string propertyName)
            : base(string.Format("{0}:{1}", tableName, propertyName)) 
        { }
        public override string DisplayName {
            get
            {
                string displayName = null;
                if (DisplayNameValue.Contains(":"))
                {
                    string[] tokens = DisplayNameValue.Split(':');
                    displayName = LocalizedAttributeHelper.GetMessageFromResource(tokens[0], tokens[1], "LABEL");
                }
                return displayName;
            }
        }
    }
    public class LocalizedValidationAttribute : ValidationAttribute
    {
        public LocalizedValidationAttribute(string tableName, string propertyName)
            : base(string.Format("{0}:{1}", tableName, propertyName)) 
        { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                string errorMessage = null;
                if (ErrorMessageString.Contains(":"))
                {
                    string[] tokens = ErrorMessageString.Split(':');
                    errorMessage = LocalizedAttributeHelper.GetMessageFromResource(tokens[0], tokens[1], "VALIDATION");
                }
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
    public class LocalizedAttributeHelper
    {
        public static string GetMessageFromResource(string tableName, string propertyName, string suffix = null)
        {
            Type resourceType = Type.GetType(string.Format("Resources.Models.{0}, Resources", tableName));
            ResourceManager rm = new ResourceManager(resourceType);
            if(string.IsNullOrEmpty(suffix))
                return rm.GetString(string.Format("{0}", propertyName));
            else
                return rm.GetString(string.Format("{0}_{1}", suffix, propertyName));
        }
    }
}