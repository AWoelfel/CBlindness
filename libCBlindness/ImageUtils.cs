using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace libCBlindness
{
    public class ImageUtils
    {
        public static Image ToBnW(Image source)
        {
            var result = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb);


            using (Graphics gr = Graphics.FromImage(result)) // SourceImage is a Bitmap object
            {

                var bw_matrix = new[]
                {
                    new[] { 1.5f, 1.5f, 1.5f, 0f, 0f },
                    new[] { 1.5f, 1.5f, 1.5f, 0f, 0f },
                    new[] { 1.5f, 1.5f, 1.5f, 0f, 0f },
                    new[] { 0f,   0f,   0f,   1f, 0f },
                    new[] { -1f,  -1f,  -1f,  0f, 1f }
                };

                var ia = new ImageAttributes();
                ia.SetColorMatrix(new ColorMatrix(bw_matrix));
                var rc = new Rectangle(0, 0, source.Width, source.Height);
                gr.DrawImage(source, rc, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, ia);
            }

            return result;
        }

        public static void SaveImage(Image img, FileInfo name)
        {
            if (name.Exists)
                name.Delete();

            using (var oStream = name.OpenWrite())
            {
                img.Save(oStream, ImageFormat.Png);
            }

        }
    }
}