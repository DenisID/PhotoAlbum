using PhotoAlbum.Client.Filters;
using PhotoAlbum.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }

    public class CreatePhotoViewModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [ValidateFile]
        public HttpPostedFileBase Image { get; set; }
    }

    public class EditPhotoViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }

    public class PhotoVoteViewModel
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
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
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}