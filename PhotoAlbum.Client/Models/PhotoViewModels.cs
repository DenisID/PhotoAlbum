using PhotoAlbum.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerName { get; set; }
        public double Rating { get; set; }
        //public byte[] Image { get; set; }
        //public string ImageMimeType { get; set; }
    }

    public class CreatePhotoViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

    public class EditPhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class PhotoVoteViewModel
    {
        public int PhotoId { get; set; }
        public int Rating { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class SortPhotoViewModel
    {
        public string Sorting { get; set; }
        public string ByCreationDate { get; } = SortOrder.ByCreationDate.ToString();
        public string ByRating { get; } = SortOrder.ByRating.ToString();
    }

    public class UserPageViewModel
    {
        public SortPhotoViewModel SortPhoto { get; set; } = new SortPhotoViewModel();
        public string UserName { get; set; }
    }
}