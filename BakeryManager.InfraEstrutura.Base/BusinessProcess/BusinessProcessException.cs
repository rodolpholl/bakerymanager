using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.InfraEstrutura.Base.BusinessProcess
{
    public class BusinessProcessException : Exception
    {
        public BusinessProcessException(string Message) : base(Message)
        {
            
        }
    }
}
