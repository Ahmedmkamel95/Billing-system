using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Net;

namespace Billing_ADSL
{
    class BillingDriver : IDriver
    {
        public IWebDriver Driver { get; set; }
        public IJavaScriptExecutor js;

        public BillingDriver()
        {
            //var chromeDriverService = ChromeDriverService.CreateDefaultService();
            //chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");
            Driver = new ChromeDriver(@"C:\Users\Kamel\Downloads\chromedriver");

            //Driver = new ChromeDriver(chromeDriverService);

            js = Driver as IJavaScriptExecutor;
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

        public Client Inject(Client ci)
        {
            BillingClient c = ci as BillingClient;
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(1500));

                c.NonePaidInvoicesCount = 0;
                c.PhoneError = "";

                if (CheckForInternetConnection())
                {
                    Driver.Navigate().GoToUrl("https://my.te.eg/anonymous/AdslPayment");
                    
                    //var btn_langualge = Driver.FindElement(By.XPath("//*[@id='language-btn']/button"));
                    //if(btn_langualge.Displayed)
                    //{
                    //    btn_langualge.Click();
                    //}

                    Driver.FindElement(By.XPath("/html/body/app-root/div/div[1]/app-anonymous-adsl-payment/div/p-card/div/div/div/div/form/div[1]/div[2]/p-inputmask/input")).SendKeys(c.Landline.ToString());
                    Driver.FindElement(By.CssSelector(".p-dropdown.p-component")).Click();
                    Driver.FindElement(By.XPath("/html/body/app-root/div/div[1]/app-anonymous-adsl-payment/div/p-card/div/div/div/div/form/div[2]/div/input")).SendKeys("ahmed.m.kamel38@gmail.com");
                    string code = ConvertCodeToGovernate(c.Code);
                    Driver.FindElement(By.CssSelector(string.Format("[aria-label='{0}']", code))).Click();
                    WebElement waitresponse = (WebElement)wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/app-root/div/div[1]/app-anonymous-adsl-payment/div/p-card/div/div/div/div/form/div[3]/div[1]/div[1]/div/label|/p-toastitem/div/div/div/div[2]")));
                    bool isPaymentDisplayed = Driver.FindElement(By.XPath("/html/body/app-root/div/div[1]/app-anonymous-adsl-payment/div/p-card/div/div/div/div/form/div[3]/div[1]/div[1]/div/label")).Displayed;
                    bool isTostalDisplayed = Driver.FindElement(By.XPath("/p-toastitem/div/div/div/div[2]")).Displayed;

                    if (isPaymentDisplayed)
                    {
                        c.NonePaidInvoicesCount = 1;
                        c.PhoneError = "غير متاح";
                    }
                    else if(isTostalDisplayed)
                    {
                        c.PhoneError = "متاح";
                    }
                    return c;
                }
                else
                {
                    //elnet fasel
                    WebDriverWait internetWait = new WebDriverWait(Driver, TimeSpan.FromMinutes(200));
                    internetWait.Until(x => CheckForInternetConnection() == true);
                    //3eed el lafa elly bazt fel if
                    return Inject(ci);
                }
            }
            catch (WebDriverTimeoutException ex)
            {
                return c;
            }
            catch (Exception ex)
            {
                WebDriverWait internetWait = new WebDriverWait(Driver, TimeSpan.FromMinutes(500));
                internetWait.Until(x => CheckForInternetConnection() == true);
                //3eed el lafa elly bazt fel if
                return Inject(ci);
            }

        }
        private string ConvertCodeToGovernate(int code)
        {
            switch (code)
            {
                case 02:
                    return "Cairo";
                case 97:
                    return "Aswan";
                case 88:
                    return "Assiut";
                case 3:
                    return "Alexandria";
                case 64:
                    return "الإسماعيلية";
                case 95:
                    return "الأقصر";
                case 65:
                    return "البحر الأحمر";
                case 45:
                    return "Behira";
                case 50:
                    return "الدقهلية";
                case 62:
                    return "السويس";
                case 55:
                    return "الشرقية";
                case 15:
                    return "العاشر من رمضان";
                case 40:
                    return "Al Gharbya";
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
