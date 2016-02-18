using System.Security.Principal;

namespace BakeryManager.BackOffice.Models
{

    public class SecurityPrincipalModel : IPrincipal
    {

        IPrincipal _principal;
        IIdentity _identity;

        #region Ctor

        public SecurityPrincipalModel(IPrincipal principal, IIdentity identity)
        {
            this._principal = principal;
            this._identity = identity;
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity
        {
            get
            {
                return this._identity;
            }
        }

        public bool IsInRole(string role)
        {

            if (this.Identity.IsAuthenticated)
            {

                var usuarioLogado = (SecurityIdentityModel)this.Identity;

                if (role.Equals("Admin"))
                    return usuarioLogado.Model.TipoUsuario == TipoUsuarioEnum.Admin;

                if (role.Equals("Operador"))
                    return usuarioLogado.Model.TipoUsuario == TipoUsuarioEnum.Operador;

                if (role.Equals("Cliente"))
                    return usuarioLogado.Model.TipoUsuario == TipoUsuarioEnum.Cliente;

            }

            return false;

        }

        #endregion

    }

}