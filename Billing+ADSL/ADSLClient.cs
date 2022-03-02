using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_ADSL
{
    class ADSLClient : Client
    {
        public string State { get; set; }
        public string Area { get; set; }
        public string Central { get; set; }
        public string MSANCode { get; set; }
        public string DataService { get; set; }
    }
}
