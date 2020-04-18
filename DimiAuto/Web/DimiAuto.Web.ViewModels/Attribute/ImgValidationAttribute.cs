namespace DimiAuto.Web.ViewModels.Attribute
{
    using DimiAuto.Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class ImgValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var img = value as IFormFile;
            var fileFileExtension = Path.GetExtension(img.FileName);
            if (!string.Equals(fileFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!string.Equals(img.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(img.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(img.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(img.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(img.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (img.Length >= GlobalConstants.ImgMaxLength)
            {
                return false;
            }
            return true;
        }
    }
}
