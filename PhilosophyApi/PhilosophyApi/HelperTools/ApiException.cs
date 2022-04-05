using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhilosophyApi.HelperTools
{
    public class ApiException : Exception
    {
        public string Error { get; set; }
        public string Msg { get; set; }

        public ApiException(Exception ex = null, string message = "", string msgHelp = "")
        {
            Error = (ex != null) ? ex.Message : msgHelp;
            Msg = message;
        }

    }
}
