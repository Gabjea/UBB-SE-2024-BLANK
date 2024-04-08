using System.IO;

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
