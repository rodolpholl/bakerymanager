using BakeryManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class ProducaoPorPedidoController : Controller
    {
        // GET: ProducaoPorPedido
        public ActionResult Index()
        {
            setViewData();
            return View();
        }

        private void setViewData()
        {
            using (var producaoPorPedido = new ProducaoPorPedido())
            {
                ViewData["ListaCliente"] = producaoPorPedido.GetListaCliente().OrderBy(x => x.Nome).Select(x => new SelectListItem()
                {
                    Text = string.Concat(x.Nome, " - ", x.TipoCliente == Entities.TipoCliente.Fisica ? x.CPF : x.CNPJ),
                    Value = x.IdCliente.ToString()
                }).ToList();

                ViewData["ListaProduto"] = producaoPorPedido.GetListaProduto().OrderBy(x => x.Nome).Select(x => new SelectListItem()
                {
                    Text = x.Nome,
                    Value = x.IdProduto.ToString()
                }).ToList();
            }
        }
    }
}