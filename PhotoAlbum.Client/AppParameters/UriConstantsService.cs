using PhotoAlbum.Client.BusinessServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.AppParameters
{
    public class UriConstantsService : ConfigurationSection, IUriConstantsService
    {
        [ConfigurationProperty(nameof(CastPhotoVote), IsRequired = true)]
        public string CastPhotoVote
        {
            get { return (string)this[nameof(CastPhotoVote)]; }
            set { this[nameof(CastPhotoVote)] = value; }
        }

        [ConfigurationProperty(nameof(CreatePhoto), IsRequired = true)]
        public string CreatePhoto
        {
            get { return (string)this[nameof(CreatePhoto)]; }
            set { this[nameof(CreatePhoto)] = value; }
        }

        [ConfigurationProperty(nameof(DeletePhotoById), IsRequired = true)]
        public string DeletePhotoById
        {
            get { return (string)this[nameof(DeletePhotoById)]; }
            set { this[nameof(DeletePhotoById)] = value; }
        }

        [ConfigurationProperty(nameof(EditPhoto), IsRequired = true)]
        public string EditPhoto
        {
            get { return (string)this[nameof(EditPhoto)]; }
            set { this[nameof(EditPhoto)] = value; }
        }

        [ConfigurationProperty(nameof(GetAllPhotos), IsRequired = true)]
        public string GetAllPhotos
        {
            get { return (string)this[nameof(GetAllPhotos)]; }
            set { this[nameof(GetAllPhotos)] = value; }
        }

        [ConfigurationProperty(nameof(GetEditPhotoById), IsRequired = true)]
        public string GetEditPhotoById
        {
            get { return (string)this[nameof(GetEditPhotoById)]; }
            set { this[nameof(GetEditPhotoById)] = value; }
        }

        [ConfigurationProperty(nameof(GetImageById), IsRequired = true)]
        public string GetImageById
        {
            get { return (string)this[nameof(GetImageById)]; }
            set { this[nameof(GetImageById)] = value; }
        }

        [ConfigurationProperty(nameof(GetPhotoRating), IsRequired = true)]
        public string GetPhotoRating
        {
            get { return (string)this[nameof(GetPhotoRating)]; }
            set { this[nameof(GetPhotoRating)] = value; }
        }

        [ConfigurationProperty(nameof(GetPhotos), IsRequired = true)]
        public string GetPhotos
        {
            get { return (string)this[nameof(GetPhotos)]; }
            set { this[nameof(GetPhotos)] = value; }
        }

        [ConfigurationProperty(nameof(GetUserPhotos), IsRequired = true)]
        public string GetUserPhotos
        {
            get { return (string)this[nameof(GetUserPhotos)]; }
            set { this[nameof(GetUserPhotos)] = value; }
        }

        [ConfigurationProperty(nameof(GetUserVotes), IsRequired = true)]
        public string GetUserVotes
        {
            get { return (string)this[nameof(GetUserVotes)]; }
            set { this[nameof(GetUserVotes)] = value; }
        }



        [ConfigurationProperty(nameof(RegisterUser), IsRequired = true)]
        public string RegisterUser
        {
            get { return (string)this[nameof(RegisterUser)]; }
            set { this[nameof(RegisterUser)] = value; }
        }

        [ConfigurationProperty(nameof(GetToken), IsRequired = true)]
        public string GetToken
        {
            get { return (string)this[nameof(GetToken)]; }
            set { this[nameof(GetToken)] = value; }
        }

        [ConfigurationProperty(nameof(GetAllUserNames), IsRequired = true)]
        public string GetAllUserNames
        {
            get { return (string)this[nameof(GetAllUserNames)]; }
            set { this[nameof(GetAllUserNames)] = value; }
        }

        [ConfigurationProperty(nameof(GetUserProfile), IsRequired = true)]
        public string GetUserProfile
        {
            get { return (string)this[nameof(GetUserProfile)]; }
            set { this[nameof(GetUserProfile)] = value; }
        }

        [ConfigurationProperty(nameof(EditUserProfile), IsRequired = true)]
        public string EditUserProfile
        {
            get { return (string)this[nameof(EditUserProfile)]; }
            set { this[nameof(EditUserProfile)] = value; }
        }

        [ConfigurationProperty(nameof(ChangePassword), IsRequired = true)]
        public string ChangePassword
        {
            get { return (string)this[nameof(ChangePassword)]; }
            set { this[nameof(ChangePassword)] = value; }
        }

        [ConfigurationProperty(nameof(GetUserFullName), IsRequired = true)]
        public string GetUserFullName
        {
            get { return (string)this[nameof(GetUserFullName)]; }
            set { this[nameof(GetUserFullName)] = value; }
        }
    }
}