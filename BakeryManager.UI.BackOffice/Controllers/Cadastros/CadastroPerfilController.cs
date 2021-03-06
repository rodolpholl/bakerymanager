﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using BakeryManager.Services.Seguranca;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Controllers.Cadastros;
using Kendo.Mvc.Extensions;
using BakeryManager.BackOffice.Models.Cadastros;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroPerfilController : System.Web.Mvc.Controller
    {
        // GET: CadastroPerfil
        public ActionResult Index()
        {


            var listaAtribuicaoPerfil = new AtribuicaoPerfilModel[]
           { new AtribuicaoPerfilModel() {
               Nome = "Administrativo",
               IdAtribuicaoPerfil = 1
           },
           new AtribuicaoPerfilModel() {
               Nome = "Operador",
               IdAtribuicaoPerfil = 2
           },
           new AtribuicaoPerfilModel() {
               Nome = "Cliente",
               IdAtribuicaoPerfil = 3
           },
           new AtribuicaoPerfilModel() {
               Nome = "Fornecedor",
               IdAtribuicaoPerfil = 4
           }
           };

            ViewData["ListaAtribuicaoPerfil"] = listaAtribuicaoPerfil.AsEnumerable();

            return View();
        }



        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadPerfil = new CadastroPerfil())
            {

                var listaPerfil = cadPerfil.GetListaPerfil().Select(x => new CadastroPerfilModel()
                {
                    IdPerfil = x.IdPerfil,
                    Ativo = x.Ativo,
                    Atribuicao = GetAtribuicaoPerfil(x.Atribuicao),
                    Nome = x.Nome
                }).ToList();

                return Json(listaPerfil.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }


        private AtribuicaoPerfilModel GetAtribuicaoPerfil(Rule atribuicao)
        {
            switch (atribuicao)
            {

                case Rule.Administrador:
                    return new AtribuicaoPerfilModel()
                    {
                        Nome = "Administrativo",
                        IdAtribuicaoPerfil = 1
                    };

                case Rule.Operador:
                    return new AtribuicaoPerfilModel()
                    {
                        Nome = "Operador",
                        IdAtribuicaoPerfil = 2
                    };

                case Rule.Cliente:
                    return new AtribuicaoPerfilModel()
                    {
                        Nome = "Cliente",
                        IdAtribuicaoPerfil = 3
                    };

                default:
                    return new AtribuicaoPerfilModel()
                    {
                        Nome = "Fornecedor",
                        IdAtribuicaoPerfil = 4
                    };

            }
        }



        // POST: CadastroPerfil/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CadastroPerfilModel> ListaPerfil)
        {
            try
            {
                if (ListaPerfil != null && ModelState.IsValid)
                {

                    using (var cadastroPerfil = new CadastroPerfil())
                    {

                        foreach (var p in ListaPerfil)
                        {


                            var perfil = new Perfil()
                            {
                                Ativo = p.Ativo,
                                Atribuicao = (Rule)p.Atribuicao.IdAtribuicaoPerfil,
                                Nome = p.Nome
                            };

                            cadastroPerfil.InserirPerfil(perfil);
                            p.IdPerfil = perfil.IdPerfil;

                        }

                    };

                }

                return Json(ListaPerfil.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }



        // POST: CadastroPerfil/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CadastroPerfilModel> ListaPerfil)
        {
            try
            {
                // TODO: Add update logic here
                if (ListaPerfil != null && ModelState.IsValid)
                {

                    using (var cadastroPerfil = new CadastroPerfil())
                    {

                        foreach (var p in ListaPerfil)
                        {

                            var perfil = cadastroPerfil.GetPerfilById(p.IdPerfil);


                            perfil.Ativo = p.Ativo;
                            perfil.Atribuicao = (Rule)p.Atribuicao.IdAtribuicaoPerfil;
                            perfil.Nome = p.Nome;


                            cadastroPerfil.AlterarPerfil(perfil);

                        }

                    };

                }
                return Json(ListaPerfil.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }



        // POST: CadastroPerfil/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CadastroPerfilModel> ListaPerfil)
        {
            try
            {
                // TODO: Add update logic here
                if (ListaPerfil != null && ModelState.IsValid)
                {

                    using (var cadastroPerfil = new CadastroPerfil())
                    {

                        foreach (var p in ListaPerfil)
                            cadastroPerfil.ExcluirPerfil(p.IdPerfil);
                        

                    }

                }

                return Json(ListaPerfil.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }
    }
}
