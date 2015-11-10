using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories.Seguranca
{
    public class SenhaDiaBM : BusinessManagementBase<SenhaDia>
    {
        public SenhaDia GetMascaraSenhaDiaAtiva()
        {
            return Query().FirstOrDefault(x => x.Ativo);
        }
    }
}
