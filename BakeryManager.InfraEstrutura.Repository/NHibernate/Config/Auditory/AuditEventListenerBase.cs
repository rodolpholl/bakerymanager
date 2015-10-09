using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BakeryManager.Infraestrutura.Helpers;
using BakeryManager.Infraestrutura.Helpers.Security;
using NHibernate.Param;
using BakeryManager.InfraEstrutura.Repository;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Config.Auditory
{
    internal class AuditEventListenerBase
    {
        public RepositoryParameter Params { get; set; }
        private const string _noValueString = "*Vazio*";

        

        protected static object getStringValueFromStateArray(object[] stateArray, int position)
        {
            var value = stateArray[position];

            return value == null || value.ToString() == String.Empty
                ? _noValueString
                : value;
        }

        
        protected string GetUsuarioLogado()
        {

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated) 
                    return HttpContext.Current.User.Identity.Name;
                else
                    throw new Exception("Usuário Logado Não Informado.");
            }
            else
            {
                return string.IsNullOrWhiteSpace( Params.UsuarioLogado) ? Environment.UserName : Params.UsuarioLogado;
            }

            /*if (string.IsNullOrWhiteSpace(Params.UsuarioLogado))
                throw new Exception("Usuário Logado Não Informado.");

            return Params.UsuarioLogado;*/
        }
    }
}
