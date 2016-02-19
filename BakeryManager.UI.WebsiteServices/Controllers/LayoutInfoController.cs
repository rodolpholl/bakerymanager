using BakeryManager.UI.WebsiteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BakeryManager.Services.WebsiteServices;

namespace BakeryManager.UI.WebsiteServices.Controllers
{
    public class LayoutInfoController : ApiController
    {
        //// GET: api/LayoutInfo
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/LayoutInfo/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpGet]
        public IEnumerable<TipoServicoModel> GetListaTipoServico()
        {
            using (var layoutInfo = new LayouInfo())
            {
                var listaRetorno = layoutInfo.GetListaTipoServico().Select(x => new TipoServicoModel()
                {
                    IdTipoServico = x.IdTipoPedido,
                    Nome = x.Descricao
                }).OrderBy(x => x.Nome).ToList();

                return listaRetorno;
            }
        }

        //// POST: api/LayoutInfo
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/LayoutInfo/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/LayoutInfo/5
        //public void Delete(int id)
        //{
        //}
    }
}
