using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    class VideoMedia : Media
    {
        public VideoMedia(string filePath) :
            base(filePath, Path.GetExtension(filePath))
        {
        }
    }
}
