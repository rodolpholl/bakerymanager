using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Entities;
using BakeryManager.Repositories;

namespace BakeryManager.Services.WebsiteServices
{
    public class EnviarMensagemContato : BusinessProcessBase, IDisposable
    {

        private AssuntoMensagemContatoBM assuntoMensagemContatoBm;
        private MensagemContatoBM mensagemContatoBm;
        public EnviarMensagemContato()
        {
            assuntoMensagemContatoBm = GetObject<AssuntoMensagemContatoBM>();
            mensagemContatoBm = GetObject<MensagemContatoBM>();
        }
        public void Dispose()
        {
            assuntoMensagemContatoBm.Dispose();
            mensagemContatoBm.Dispose();
        }

        public IList<AssuntoMensagemContato> GetListaAssuntos()
        {
            return assuntoMensagemContatoBm.GetAll();
        }

        public AssuntoMensagemContato GetAssuntoById(int idAssuntoMensagemContato)
        {
            return assuntoMensagemContatoBm.GetByID(idAssuntoMensagemContato);
        }

        public void InserirMensagem(MensagemContato mensagemContato)
        {
            throw new NotImplementedException();
        }
    }
}
