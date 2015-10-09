using System;

namespace BakeryManager.BackOffice.Models
{

    public enum TipoUsuarioEnum
    {
        Cliente = 0,
        RedeCredenciada = 1,
        Administracao = 2
    }

    [Serializable()]
    public class UserIdentityModel
    {

        public TipoUsuarioEnum TipoUsuario { get; set; }
        public string Login { get; set; }
        public DateTime ExpiraEm { get; set; }
    }

}