﻿using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class IngredienteFormulaBM : BusinessManagementBase<IngredienteFormula>
    {
        public IList<IngredienteFormula> GetByFormula(Formula formula)
        {
            return Query().Where(x => x.Formula.IdFormula == formula.IdFormula).ToList();
        }
    }
}
