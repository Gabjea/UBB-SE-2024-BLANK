using client.models;
using System.IO;

namespace client.modules
{
    class EncryptionModule
    {
        public void encryptFile(Media file)
        {
            File.Encrypt(file.FilePath);
        } 

        public void decryptFile(Media file)
        {
            File.Decrypt(file.FilePath);
        }
    }
}
