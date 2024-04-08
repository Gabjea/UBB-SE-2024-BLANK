using System.IO;
using System.Windows;

namespace client.models
{
    class Media
    {
        public String FilePath { get; set; }

        public Media(String filePath, String fileExtension)
        {
            string workingDirectory = Environment.CurrentDirectory;
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            String newFilePath = workingDirectory + @"\" + myuuidAsString + fileExtension;
            File.Copy(filePath, newFilePath);
            MessageBox.Show(newFilePath);

            this.FilePath = newFilePath;
        }
    }
}
