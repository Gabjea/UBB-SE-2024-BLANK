using client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace client.modules
{
    class CompressionModule
    {
        private int compressQuality = 60;

        private void compressImage(Media photo)
        {
            Image compressedImage = changeQuality(photo.FilePath, compressQuality);
            compressedImage.Save(photo.FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void decompressImage(Media photo)
        {
            Image compressedImage = changeQuality(photo.FilePath, 100);
            compressedImage.Save(photo.FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private Image changeQuality(string fileName, int newQuality)
        {
            using (Image image = Image.FromFile(fileName))
            using (Image memImage = new Bitmap(image))
            {
                ImageCodecInfo myImageCodecInfo;
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, newQuality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                MemoryStream memStream = new MemoryStream();
                memImage.Save(memStream, myImageCodecInfo, myEncoderParameters);
                Image newImage = Image.FromStream(memStream);
                ImageAttributes imageAttributes = new ImageAttributes();
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  //**
                    g.DrawImage(newImage, new Rectangle(System.Drawing.Point.Empty, newImage.Size), 0, 0,
                      newImage.Width, newImage.Height, GraphicsUnit.Pixel, imageAttributes);
                }
                return newImage;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in encoders)
                if (ici.MimeType == mimeType) return ici;

            return null;
        }


        public void compressFile(Media mediaFile)
        {
            if (mediaFile.GetType() == typeof(PhotoMedia))
            {
                compressImage(mediaFile);
                MessageBox.Show("Photo compressed succesfully!");
            }
            else
            {
                MessageBox.Show("ToDO");
            }
        }

        public void decompressFile(Media mediaFile)
        {
            if (mediaFile.GetType() == typeof(PhotoMedia))
            {
                decompressImage(mediaFile);
                MessageBox.Show("Photo decompressed succesfully!");
            }
            else
            {
                MessageBox.Show("ToDO");
            }
        }
    }
}
