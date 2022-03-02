using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_ADSL
{
    class BillingClient : Client
    {
        public int NonePaidInvoicesCount { get; set; }
        public string PhoneError { get; set; }
    }
}
