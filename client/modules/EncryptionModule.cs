using client.models;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Windows;

namespace client.modules
{
    class EncryptionModule
    {

        private byte[] encryptionKey; // TODO get from settings Module
        public EncryptionModule() 
        {
            encryptionKey = getEncryptionKey();
        }

        private byte[] getEncryptionKey()
        {
            //TODO to get from Settings Module
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }

        private void fileEncryptAlgorithm(string inputPath, string outputPath, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;

                // Initialization Vector
                aesAlg.GenerateIV();

                // Encryptor class
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (FileStream fsInput = new FileStream(inputPath, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputPath, FileMode.Create))
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    // Encrypt the file contents
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        private void fileDecryptAlgorithm(string inputPath, string outputPath, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;

                // Read IV from the beginning of the file
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                using (FileStream fsInput = new FileStream(inputPath, FileMode.Open))
                {
                    fsInput.Read(iv, 0, iv.Length);
                }

                aesAlg.IV = iv;

                // Create decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (FileStream fsInput = new FileStream(inputPath, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputPath, FileMode.Create))
                using (CryptoStream csDecrypt = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                {
                    // Skip IV
                    fsInput.Seek(iv.Length, SeekOrigin.Begin);

                    // Decrypt the file contents
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsOutput.Write(buffer, 0, bytesRead);
                    }
                }
            }

        }

        public Media encryptFile(Media file)
        {
            String encryptedFilePath = Environment.CurrentDirectory + @"\" + Guid.NewGuid().ToString() + Path.GetExtension(file.FilePath);
            fileEncryptAlgorithm(file.FilePath, encryptedFilePath, encryptionKey);

            if (file.GetType() == typeof(PhotoMedia))
            {
                return new PhotoMedia(encryptedFilePath);
            }
            else
            {
                return new VideoMedia(encryptedFilePath);
            }
        } 

        public Media decryptFile(Media file)
        {
            String decryptedFilePath = Environment.CurrentDirectory + @"\" + Guid.NewGuid().ToString() + Path.GetExtension(file.FilePath);
            fileDecryptAlgorithm(file.FilePath, decryptedFilePath, encryptionKey);

            if (file.GetType() == typeof(PhotoMedia))
            {
                return new PhotoMedia(decryptedFilePath);
            }
            else
            {
                return new VideoMedia(decryptedFilePath);
            }
        }
    }
}
