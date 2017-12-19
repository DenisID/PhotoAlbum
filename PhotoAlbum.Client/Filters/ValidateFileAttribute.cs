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
        //private IValidateFileConstantsService _validateFileConstantsService;

        //public ValidateFileAttribute(IValidateFileConstantsService validateFileConstantsService)
        //    : base()
        //{
        //    _validateFileConstantsService = validateFileConstantsService;
        //}

        public override bool IsValid(object value)
        {
            //int maxContentLength = 1024 * 1024 * 10; //10 MB
            //string[] allowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

            //int maxContentLength = _validateFileConstantsService.MaxContentLength;
            //string[] allowedFileExtensions = _validateFileConstantsService.AllowedFileExtensions;

            int maxContentLength = DependencyResolver.Current.GetService<IValidateFileConstantsService>().MaxContentLength;

            var tempData = DependencyResolver.Current.GetService<IValidateFileConstantsService>().AllowedFileExtensions;
            string[] allowedFileExtensions = tempData.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            else if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = Resources.ResourceEN.PleaseUploadYourPhotoOfType + " " + string.Join(", ", allowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > maxContentLength)
            {
                ErrorMessage = Resources.ResourceEN.YourPhotoIsTooLargeMaximumAllowedSizeIs + " " + (maxContentLength / 1024).ToString() + Resources.ResourceEN.MB;
                return false;
            }
            else
                return true;
        }
    }
}