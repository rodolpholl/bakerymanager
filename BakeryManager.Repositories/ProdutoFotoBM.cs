﻿using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class ProdutoFotoBM : BusinessManagementBase<ProdutoFoto>
    {
        public IList<ProdutoFoto> GetByProduto(Produto produto)
        {
            return Query().Where(x => x.Produto.IdProduto == produto.IdProduto).ToList();
        }
    }
}