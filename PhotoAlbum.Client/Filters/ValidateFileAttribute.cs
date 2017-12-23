using PhotoAlbum.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Filters
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int maxContentLength = DependencyResolver.Current.GetService<IValidateFileConstantsService>().MaxContentLength;

            var tempData = DependencyResolver.Current.GetService<IValidateFileConstantsService>().AllowedFileExtensions;
            string[] allowedFileExtensions = tempData.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            else if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = Resources.Localization.PleaseUploadYourPhotoOfType + " " + string.Join(", ", allowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > maxContentLength)
            {
                ErrorMessage = Resources.Localization.YourPhotoIsTooLargeMaximumAllowedSizeIs + " " + (maxContentLength / 1024).ToString() + Resources.Localization.MB;
                return false;
            }
            else
                return true;
        }
    }
}