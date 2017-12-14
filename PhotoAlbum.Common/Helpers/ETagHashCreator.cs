using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace PhotoAlbum.Common.Helpers
{
    public class ETagHashCreator
    {
        public static string ComputeHash(object instance)
        {
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            var serializer = new DataContractSerializer(instance.GetType());

            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());

                return String.Join("", cryptoServiceProvider.Hash.Select(c => c.ToString("x2")));
            }
        }
    }
}
