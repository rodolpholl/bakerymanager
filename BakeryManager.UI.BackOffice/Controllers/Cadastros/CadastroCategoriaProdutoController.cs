using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.Entities;
using BakeryManager.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.BackOffice.Models;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroCategoriaProdutoController : Controller
    {
        // GET: CadastroCategoriaProduto
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadCategoria = new CadastroCategoriaProduto())
            {
                return Json(cadCategoria.GetCategoriaProdutoAll().Select(x => new CategoriaProdutoModel()
                {
                    Nome = x.Nome,
                    IdCategoriaProduto = x.IdCategoriaProduto,
                    PermiteExclusao = cadCategoria.ValidaProdutoContidoCategoriaProduto(x.IdCategoriaProduto)
                }).OrderBy(x => x.Nome).AsEnumerable().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }



        // POST: CadastroCategoriaProduto/Create
        [HttpPost]
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoriaProdutoModel> ListaCategoriaProdutoModel)
        {

            using (var cadCategoria = new CadastroCategoriaProduto())
            {
                
                foreach (var CategoriaProdutoModel in ListaCategoriaProdutoModel)
                {

                    var categoriaProduto = new CategoriaProduto()
                    {
                        Nome = CategoriaProdutoModel.Nome
                    };

                    cadCategoria.InserirCategoriaProduto(categoriaProduto);

                    CategoriaProdutoModel.IdCategoriaProduto = categoriaProduto.IdCategoriaProduto;
                }

               

                return Json(ListaCategoriaProdutoModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }


        }
        

        // POST: CadastroCategoriaProduto/Edit/5
        [HttpPost]
        public JsonResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoriaProdutoModel> ListaCategoriaProdutoModel)
        {
            using (var cadCategoria = new CadastroCategoriaProduto())
            {

                foreach (var CategoriaProdutoModel in ListaCategoriaProdutoModel)
                {
                    var categoriaProduto = cadCategoria.GetCategoriaProdutoById(CategoriaProdutoModel.IdCategoriaProduto);
                    categoriaProduto.Nome = CategoriaProdutoModel.Nome;
                    
                    cadCategoria.AlterarCategoriaProduto(categoriaProduto);
                }
              

                return Json(ListaCategoriaProdutoModel, JsonRequestBehavior.AllowGet);

            }

        }



        // POST: CadastroCategoriaProduto/Delete/5
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                using (var cadCategoria = new CadastroCategoriaProduto())
                {

                    var categoriaProduto = cadCategoria.GetCategoriaProdutoById(Id);
                    cadCategoria.ExcluirCategoriaProduto(categoriaProduto);

                    return Json(new { Mensagem = "Registro Excluído com Sucesso!", TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = ex.Message, TipoMensagem = TipoMensagemRetorno.Erro }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
