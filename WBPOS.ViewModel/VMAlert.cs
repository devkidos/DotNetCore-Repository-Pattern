using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.ViewModel
{
    public class Alert
    {
        public string Message;
        public string Type;

        public Alert(string message, string type)
        {
            Message = message;
            Type = type;
        }
    }
}
