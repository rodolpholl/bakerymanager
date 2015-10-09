using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models
{
    public enum TipoMensagemRetorno
    {
        Alerta = 0,
        Erro = 1,
        Confirmacao = 2,
        Ok = 3,
        Informacao = 4
    }
}