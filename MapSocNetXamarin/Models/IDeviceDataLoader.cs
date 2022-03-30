using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MapSocNetXamarin.Models
{
    public interface IDeviceDataLoader
    {
        Task<Stream> GetImageStreamAsync();
    }
}
