using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;
using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessProcess;
using BakeryManager.BackOffice.Models;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroCategoriaIngredientesController : Controller
    {
        // GET: CadastroCategoriaIngredientes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadCategoria = new CadastroCategoriaIngrediente())
            {
                var listaCategoria = cadCategoria.GetCategoriaIngredienteAll();
                return Json(listaCategoria.Select(x => new CategoriaIngredienteModel()
                {
                    Nome = x.Nome,
                    IdCategoriaIngrediente = x.IdCategoriaIngrediente,
                    PermiteExclusao = cadCategoria.VerificaDependenciaCategoriaIngrediente(x.IdCategoriaIngrediente)
                }).AsEnumerable().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);



            }


        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoriaIngredienteModel> ListacategoriaModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var categoriaModel in ListacategoriaModel)
                {
                    using (var cadCategoria = new CadastroCategoriaIngrediente())
                    {
                        var categoria = new CategoriaIngrediente()
                        {
                            Nome = categoriaModel.Nome.Trim()
                        };

                        cadCategoria.InserirCategoriaIngrediente(categoria);
                        categoriaModel.IdCategoriaIngrediente = categoria.IdCategoriaIngrediente;
                    }
                }
            }

            return Json(ListacategoriaModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoriaIngredienteModel> ListacategoriaModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var categoriaModel in ListacategoriaModel)
                {
                    using (var cadCategoria = new CadastroCategoriaIngrediente())
                    {
                        var categoria = cadCategoria.GetCategoriaIngredienteById(categoriaModel.IdCategoriaIngrediente);
                        categoria.Nome = categoriaModel.Nome.Trim();
                        cadCategoria.AlterarCategoriaIngrediente(categoria);


                    }
                }
            }

            return Json(ListacategoriaModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Id)
        {

            try
            {
                using (var cadCategoria = new CadastroCategoriaIngrediente())
                {

                    cadCategoria.ExcluirCategoriaIngrediente(Id);
                    return Json(new { Mensagem = "Registro Excluído com Sucesso!", TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (BusinessProcessException ex)
            {
                return Json(new { Mensagem = ex.Message, TipoMensagem = TipoMensagemRetorno.Erro }, "text/html", JsonRequestBehavior.AllowGet);
            }

        }
    }
}