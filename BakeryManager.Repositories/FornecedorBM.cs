﻿using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FornecedorBM : BusinessManagementBase<Fornecedor>
    {
        public IList<Fornecedor> getFornecedoresAtivos()
        {
            return Query().Where(x => x.Ativo).ToList();
        }
    }
}
