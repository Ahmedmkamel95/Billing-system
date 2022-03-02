using ADSL;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_ADSL
{
    class ADSLDriver : IDriver
    {
        public IWebDriver Driver { get; set; }

        public ADSLDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");
           // Driver = new ChromeDriver(chromeDriverService, options);
            Driver = new ChromeDriver(chromeDriverService);
        }

        private bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Client Inject(Client ic)
        {
            ADSLClient c = ic as ADSLClient;
            try
            {
                if (CheckForInternetConnection())
                {
                    Driver.Navigate().GoToUrl("https://adsl.te.eg/ISP/ISP-tel.aspx");

                    string code = ConvertCodeToGovernate(c.Code);
                    Driver.FindElement(By.XPath(string.Format("//option[text()='{0}']", code))).Click();

                    Driver.FindElement(By.Id("ctl00_ContentPlaceHolderBody_txt_ClientCircuitNum")).SendKeys(c.Landline.ToString());

                    TessnetOCR ocr = new TessnetOCR();
                    IWebElement CaptchImage = Driver.FindElement(By.Id("ctl00_ContentPlaceHolderBody_imCaptcha"));
                    string captcha = ocr.ConvertCaptchaIntoString(Driver, CaptchImage);

                    Driver.FindElement(By.Id("ctl00_ContentPlaceHolderBody_txtVerify")).SendKeys(captcha);

                    Driver.FindElement(By.Id("ctl00_ContentPlaceHolderBody_btnAvailabilityRequest")).Click();

                    var r = Driver.FindElements(By.XPath("//td[@style='width:90%;']//following::tr[7]"));

                    c.State = r[0].Text.Trim();
                    c.Area = r[1].Text.Contains(':') ? r[1].Text.Split(':')[1] : "";
                    c.Central = r[2].Text.Contains(':') ? r[2].Text.Split(':')[1] : "";
                    c.MSANCode = r[3].Text.Contains(':') ? r[3].Text.Split(':')[1] : "";
                    c.DataService = r[4].Text.Contains(':') ? r[4].Text.Split(':')[1] : "";

                    return c;
                }
                else
                {
                    OpenQA.Selenium.Support.UI.WebDriverWait internetWait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromMinutes(500));
                    internetWait.Until(x => CheckForInternetConnection() == true);
                    return Inject(ic);
                }
            }
            catch (Exception)
            {
                WebDriverWait internetWait = new WebDriverWait(Driver, TimeSpan.FromMinutes(200));
                internetWait.Until(x => CheckForInternetConnection() == true);
                //3eed el lafa elly bazt fel if
                return Inject(ic);
            }
        }
        private string ConvertCodeToGovernate(int code)
        {
            switch (code)
            {
                case 02:
                    return "القاهره";
                case 97:
                    return "أسوان";
                case 88:
                    return "أسيوط";
                case 3:
                    return "الأسكندريه";
                case 64:
                    return "الإسماعيلية";
                case 95:
                    return "الأقصر";
                case 65:
                    return "البحر الأحمر";
                case 45:
                    return "البحيرة";
                case 50:
                    return "الدقهلية";
                case 62:
                    return "السويس";
                case 55:
                    return "الشرقية";
                case 15:
                    return "العاشر من رمضان";
                case 40:
                    return "الغربية";
                case 84:
                    return "الفيوم";
                case 48:
                    return "المنوفية";
                case 86:
                    return "المنيا";
                case 92:
                    return "الوادى الجديد";
                case 82:
                    return "بنى سويف";
                case 66:
                    return "بورسعيد";
                case 69:
                    return "جنوب سيناء";
                case 57:
                    return "دمياط";
                case 93:
                    return "سوهاج";
                case 68:
                    return "شمال سيناء";
                case 96:
                    return "قنا";
                case 47:
                    return "كفر الشيخ";
                case 46:
                    return "مرسى مطروح";
                case 13:
                    return "القليوبية";

                default:
                    return "خطأ";
            }
        }
    }
}
