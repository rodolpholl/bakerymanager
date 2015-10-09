using System;

namespace BakeryManager.BackOffice.Models
{

    public enum TipoUsuarioEnum
    {
        Admin = 0,
        Operador = 1,
        Cliente = 2
    }

    [Serializable()]
    public class UserIdentityModel
    {

        public TipoUsuarioEnum TipoUsuario { get; set; }
        public string Login { get; set; }
        public string Perfil { get; set; }
        public DateTime ExpiraEm { get; set; }
    }

}