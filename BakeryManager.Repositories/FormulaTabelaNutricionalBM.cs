using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FormulaTabelaNutricionalBM :BusinessManagementBase<FormulaTabelaNutricional>
    {
        public IList<FormulaTabelaNutricional> GetByFormula(Formula pFormula)
        {
            return Query().Where(x => x.Formula.IdFormula == pFormula.IdFormula).ToList();
        }
    }
}
