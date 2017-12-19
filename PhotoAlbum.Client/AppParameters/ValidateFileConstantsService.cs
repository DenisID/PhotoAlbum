using PhotoAlbum.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.AppParameters
{
    public class ValidateFileConstantsService : ConfigurationSection, IValidateFileConstantsService
    {
        [ConfigurationProperty(nameof(MaxContentLength), IsRequired = true)]
        public int MaxContentLength
        {
            get { return (int)base[nameof(MaxContentLength)]; }
            set { base[nameof(MaxContentLength)] = value; }
        }

        [ConfigurationProperty(nameof(AllowedFileExtensions), IsRequired = true)]
        public string AllowedFileExtensions
        {
            get
            {
                return ((string)this[nameof(AllowedFileExtensions)]);
            }
            set { this[nameof(AllowedFileExtensions)] = value; }
        }
    }
}