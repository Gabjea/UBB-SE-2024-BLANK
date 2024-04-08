using client.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
