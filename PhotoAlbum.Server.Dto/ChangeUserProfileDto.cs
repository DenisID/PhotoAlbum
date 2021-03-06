﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class ChangeUserProfileDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
