namespace client.models
{
    class PhotoMedia : Media
    {
        public PhotoMedia(string filePath) :
            base(filePath, ".jpg")
        {

        }
    }
}
