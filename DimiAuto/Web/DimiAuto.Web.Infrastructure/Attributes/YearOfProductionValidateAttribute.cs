namespace DimiAuto.Web.Infrastructure.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.RegularExpressions;

    public class YearOfProductionValidateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            var regex = "^([0-1][0-9].[1-2][0-9]{3})";
            if (string.IsNullOrEmpty(inputValue))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(inputValue, regex);
            }
        }
    }
}
