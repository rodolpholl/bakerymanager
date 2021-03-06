﻿using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories.Seguranca
{
    public class PerfilBM : BusinessManagementBase<Perfil>
    {
        public IList<Perfil> GetPerfisFornecedor()
        {
            return Query().Where(x => x.Atribuicao == Rule.Fornecedor).ToList();
        }
    }
}
