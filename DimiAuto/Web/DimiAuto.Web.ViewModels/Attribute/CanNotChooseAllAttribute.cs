namespace DimiAuto.Web.ViewModels.Attribute
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CanNotChooseAllAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value.ToString() != "All";
        }
    }
}
