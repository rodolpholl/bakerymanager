using System.Security.Principal;

namespace BakeryManager.BackOffice.Models
{
    public class SecurityIdentityModel : IIdentity
    {

        IIdentity _identity;
        UserIdentityModel _identityModel;

        #region Ctor

        public SecurityIdentityModel(IIdentity identity, UserIdentityModel identityModel)
        {
            this._identity = identity;
            this._identityModel = identityModel;
        }

        #endregion

        #region IIdentity Members

        public UserIdentityModel Model
        {
            get
            {
                return this._identityModel;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return this._identity.AuthenticationType;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this._identity.IsAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                return this._identity.Name;
            }
        }

        #endregion

    }

}