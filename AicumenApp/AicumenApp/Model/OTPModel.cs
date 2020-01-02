using System;
using System.Collections.Generic;
using System.Text;

namespace AicumenApp.Model
{
   
    
   public class OTPModel
    {
        public string sender { set; get; }
        public string route { set; get; }
        public string country { set; get; }

        public List<Sms> sms { set; get; } = new List<Sms> { };
    }
    public class Sms
    {
        public string message { set; get; }
        public List<string> to { set; get; }
        
    }
    public class OTPResponse
    {
        public string message { set; get; }
        public string type { set; get; }
    }
}
