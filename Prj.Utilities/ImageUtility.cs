using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using File = System.IO.File;

namespace Prj.Utilities
{
    public static class ImageUtility
    {
        /// <summary>
        /// ساخت فولدر
        /// </summary>
        /// <param name="pathDirectory">مسیر مطلق ذخیره سازی فولدر</param>
        public static void CreateDirectoryIfNotExist(string pathDirectory)
        {
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);
        }

        /// <summary>
        /// حذف امن فایل
        /// </summary>
        /// <param name="path">مسیر نسبی</param>
        public static void SafeDeleteFile(string path)
        {
            string root;

            if (path.ToLower().StartsWith("~/files"))
            {
                root = HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                throw new Exception("مسیر عکس به درستی ذخیره نشده است");

            }

            if (File.Exists(root))
            {
                File.Delete(root);
            }
        }


        /// <summary>
        ///  تغییر سایز عکس با رعایت نسبت و تناسب اولیه عکس
        /// </summary>
        /// <param name="image">عکس</param>
        /// <param name="maxWidth">سایز حداکثر عرض</param>
        /// <param name="maxHeight">سایز حداکثر ارتفاع</param>
        /// <param name="isAutoRatio"></param>
        /// <returns></returns>
        public static Bitmap ScaleImage(Image image, int maxWidth, int maxHeight, bool isAutoRatio)
        {
            var newWidth = maxWidth;
            var newHeight = maxHeight;

            if (isAutoRatio)
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);
                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);
            }
            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;

        }

        public static MemoryStream SetQualityStream(Image img, int quality, string headerType = "image/jpeg")
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality));


            // Encoder parameter for image quality 
            var qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec 
            var jpegCodec = GetEncoderInfo(headerType);

            var encoderParams = new EncoderParameters(1) { Param = {[0] = qualityParam } };

            var ms = new MemoryStream();
            if (jpegCodec != null)
            {
                img.Save(ms, jpegCodec, encoderParams);
            }
            return ms;
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            var codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            foreach (var t in codecs)
                if (!string.IsNullOrEmpty(t.MimeType))
                {
                    if (t.MimeType == mimeType)
                        return t;
                }
            return null;
        }

        public static readonly List<string> Extentions = new List<string>() { ".jpg", ".jpeg", ".png", "gif" };

        public const string ImageRoot = "~/Files/Images";
        public const string DocumentRoot = "~/Files/Documents";
        public const string VideoRoot = "~/Files/Videos";

        public const string ShopImageRoot = ImageRoot + "/Shops";


        public const string ShopRoot = "~/Files/Shops";
        public const string UserRoot = "~/Files/Users";
        public const string SupportRoot = "~/Files/Support";


    }
}