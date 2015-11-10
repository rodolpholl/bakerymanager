using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class TabelaNutricionalBM : BusinessManagementBase<TabelaNutricional>
    {
        public IList<TabelaNutricional> GetComponentesForaDaLista(IList<int> listaExibicao)
        {
            return  Query().Where(x => !listaExibicao.Contains(x.IdTabelaNutricional)).ToList();
        }

        public TabelaNutricional GetByNomePlanilha(string NomePlanilhaCarga)
        {
            return Query().FirstOrDefault(x => x.NomePlanilhaCarga == NomePlanilhaCarga);
        }
    }
}
