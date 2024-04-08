using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.modules
{
    internal class ValidationModule
    {
        private readonly string[] allowedPictureTypes = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] allowedVideoTypes = { ".mp4", ".avi", ".mov", ".mkv" };
        private readonly long maxSizeInBytes = 50 * 1024 * 1024; // 50 MB

        public void ValidateFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File does not exist.", filePath);
                }

                FileInfo fileInfo = new FileInfo(filePath);

                // Check file size
                if (fileInfo.Length > maxSizeInBytes)
                {
                    throw new FileLoadException("File size exceeds the maximum allowed size.", filePath);
                }

                // Check file type
                string fileExtension = Path.GetExtension(filePath);
                bool isValidType = false;

                // Check if it's a picture format
                foreach (string allowedType in allowedPictureTypes)
                {
                    if (fileExtension.Equals(allowedType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isValidType = true;
                        break;
                    }
                }

                // Check if it's a video format if not a picture format
                if (!isValidType)
                {
                    foreach (string allowedType in allowedVideoTypes)
                    {
                        if (fileExtension.Equals(allowedType, StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValidType = true;
                            break;
                        }
                    }
                }

                if (!isValidType)
                {
                    throw new FileFormatException("Invalid file format.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.FileName}");
                throw;
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine($"File load error: {ex.Message}");
                throw;
            }
            catch (FileFormatException ex)
            {
                Console.WriteLine($"Invalid file format: {ex.Message}");
                throw;
            }
        }
    }

}
