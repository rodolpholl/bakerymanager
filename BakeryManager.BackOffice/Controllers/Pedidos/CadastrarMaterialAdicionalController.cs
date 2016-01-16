using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Pedido;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class CadastrarMaterialAdicionalController : Controller
    {
        // GET: CadastrarMaterialAdicional
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using(var cadMaterialAdicional = new CadastrarMaterialAdiconal())
            {
                return Json(cadMaterialAdicional.GetAll().Select(x => new MaterialAdicionalModel()
                {
                    Ativo = x.Ativo,
                    Descricao = x.Descricao,
                    IdMaterialAdicional = x.IdMaterialAdicional
                }).ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<MaterialAdicionalModel> ListaMaterialAdicionalModel)
        {
            using (var cadMaterialAdicional = new CadastrarMaterialAdiconal())
            {
                foreach (var material in ListaMaterialAdicionalModel)
                {
                    var materialAdiconal = cadMaterialAdicional.GetMaterialAdicionalById(material.IdMaterialAdicional);
                    materialAdiconal.Descricao = material.Descricao;
                    cadMaterialAdicional.AlterarMaterialAdicional(materialAdiconal);
                }

                return Json(ListaMaterialAdicionalModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<MaterialAdicionalModel> ListaMaterialAdicionalModel)
        {
            using (var cadMaterialAdicional = new CadastrarMaterialAdiconal())
            {
                foreach (var material in ListaMaterialAdicionalModel)
                {
                    var materialAdicional = new MaterialAdicional()
                    {
                        Ativo = true,
                        Descricao = material.Descricao
                    };

                    cadMaterialAdicional.InserirMaterialAdicional(materialAdicional);
                    material.IdMaterialAdicional = materialAdicional.IdMaterialAdicional;
                }

                return Json(ListaMaterialAdicionalModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Desativar(int Id)
        {
            try
            {
                using (var cadMaterialAdicional = new CadastrarMaterialAdiconal())
                {
                    var materialAdicional = cadMaterialAdicional.GetMaterialAdicionalById(Id);
                    cadMaterialAdicional.DesativarMaterialAdicional(materialAdicional);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Materal Adicional Desativado com sucesso!"

                                       }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              TipoMensagem = TipoMensagemRetorno.Erro,
                              Mensagem = ex.Message

                          }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Reativar(int Id)
        {
            try
            {
                using (var cadMaterialAdicional = new CadastrarMaterialAdiconal())
                {
                    var materialAdicional = cadMaterialAdicional.GetMaterialAdicionalById(Id);
                    cadMaterialAdicional.ReativarMaterialAdicional(materialAdicional);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Materal Adicional Reativado com sucesso!"

                                       }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              TipoMensagem = TipoMensagemRetorno.Erro,
                              Mensagem = ex.Message

                          }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
    }
}