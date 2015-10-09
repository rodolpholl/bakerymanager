using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Infraestrutura.Base.BusinessProcess
{
    public interface IBusinessProcessBase : IDisposable
    {
        T BusinessObject<T>();
    }
}
