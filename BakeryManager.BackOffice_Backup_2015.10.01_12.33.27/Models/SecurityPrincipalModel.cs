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

                var usuarioLogado = (SecurityIdentityModel) this.Identity;

                if (usuarioLogado.Name.Equals("USR_ADMIN") && role.Equals("Administracao"))
                    return true;

                if (role.Equals("RedeCredenciada"))
                    return usuarioLogado.Model.TipoUsuario == TipoUsuarioEnum.RedeCredenciada;

            }

            return false;

        }

        #endregion

    }

}