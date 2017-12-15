using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Filters
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 10; //10 MB
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = Resources.ResourceEN.PleaseUploadYourPhotoOfType + " " + string.Join(", ", AllowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = Resources.ResourceEN.YourPhotoIsTooLargeMaximumAllowedSizeIs + " " + (MaxContentLength / 1024).ToString() + Resources.ResourceEN.MB;
                return false;
            }
            else
                return true;
        }
    }
}