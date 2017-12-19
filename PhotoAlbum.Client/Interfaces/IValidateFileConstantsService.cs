using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Interfaces
{
    public interface IValidateFileConstantsService
    {
        int MaxContentLength { get; }
        string AllowedFileExtensions { get; }
    }
}
