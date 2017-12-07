using PhotoAlbum.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class PagingParametersDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public SortOrder Sorting { get; set; } = SortOrder.ByCreationDate;
        //public int TotalNumberOfPages { get; set; }
    }
}
