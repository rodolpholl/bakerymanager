using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.UI.DeliciaDiPane
{
    public static class ServiceParams
    {

        public static string ServiceRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["EnderecoAPIRemoto"];
            }
        }

    }
}
