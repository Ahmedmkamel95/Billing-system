using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_ADSL
{
    interface IDriver
    {
        IWebDriver Driver { get; set; }

        Client Inject(Client c);
    }
}
