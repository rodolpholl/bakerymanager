using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.InfraEstrutura.Helpers
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DisplayNameClass : System.Attribute
    {
        private string _name;

        public DisplayNameClass(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
