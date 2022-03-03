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
                    return "Ismalia";
                case 95:
                    return "Luxor";
                case 65:
                    return "Red Sea";
                case 45:
                    return "Behira";
                case 50:
                    return "Dakahliya";
                case 62:
                    return "Suez";
                case 55:
                    return "Sharkia";
                case 15:
                    return "10th of Ramadan";
                case 40:
                    return "Al Gharbya";
                case 84:
                    return "Fayoum";
                case 48:
                    return "Menoufia";
                case 86:
                    return "Menia";
                case 92:
                    return "Wadi Gadid";
                case 82:
                    return "Beni Souif";
                case 66:
                    return "Port Said";
                case 69:
                    return "South Sinai";
                case 57:
                    return "Damietta";
                case 93:
                    return "Souhag";
                case 68:
                    return "North Sinai";
                case 96:
                    return "Quina";
                case 47:
                    return "Kafr El-Sheikh";
                case 46:
                    return "Matroh";
                case 13:
                    return "Qaliobia";

                default:
                    return "wrong";
            }
        }
    }
}
