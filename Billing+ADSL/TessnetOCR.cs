using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using tessnet2;

namespace ADSL
{
    class TessnetOCR
    {
        public string ConvertCaptchaIntoString(IWebDriver driver, IWebElement CaptchaImage)
        {
            string[] strDrives = Environment.GetLogicalDrives();

            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot shot = ssdriver.GetScreenshot();
            using (MemoryStream ms = new MemoryStream(shot.AsByteArray))
            using (Image imgShot = Image.FromStream(ms))
            using (Bitmap bitmap = new Bitmap(imgShot))
            {
                bitmap.Save(strDrives[1] + "full.png", ImageFormat.Png);
            }

            Point point = new Point();
            point.X = CaptchaImage.Location.X + 1;
            point.Y = CaptchaImage.Location.Y - 3;
            int width = CaptchaImage.Size.Width;
            int height = CaptchaImage.Size.Height;

            Rectangle section = new Rectangle(point, new Size(width, height));


            using (Bitmap source = new Bitmap(strDrives[1] + "full.png"))
            using (Bitmap final_image = CropImage(source, section))
            {
                final_image.Save(strDrives[1] + "image.png", ImageFormat.Png);
            }
            
            //convert the captcha into string
            string captcha = "";
            using (var image = new Bitmap(strDrives[1] + "image.png"))
            {
                var ocr = new Tesseract();
                ocr.SetVariable("tessedit_char_whitelist", "0123456789");
                //string s = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                ocr.Init(@"C:\Program Files (x86)\BillingADSL\packages\NuGet.Tessnet2.1.1.1\content\Content\tessdata", "eng", true);
                var result = ocr.DoOCR(image, Rectangle.Empty);

                foreach (Word word in result)
                    captcha += word.Text;
            }

            return captcha;
        }
        private Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bmp;
        }

    }
}
