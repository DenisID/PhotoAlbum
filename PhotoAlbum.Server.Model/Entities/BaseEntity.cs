using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
