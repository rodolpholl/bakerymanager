using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroCliente : BusinessProcessBase, IDisposable
    {

        private ClienteBM clienteBm;
        private ClienteContatoBM clienteContatoBm;
        private CondicaoPagamentoBM condicaoPagamentoBm;

        public CadastroCliente()
        {
            clienteBm = GetObject<ClienteBM>();
            clienteContatoBm = GetObject<ClienteContatoBM>();
            condicaoPagamentoBm = GetObject<CondicaoPagamentoBM>();
        }

        public void Dispose()
        {
            clienteBm.Dispose();
            clienteContatoBm.Dispose();
            condicaoPagamentoBm.Dispose();
        }

     

        public IList<Cliente>  GetListaClienteByTipoCliente(int? idTipoCliente)
        {
            if (idTipoCliente == null || !idTipoCliente.HasValue)
                return clienteBm.GetAll();
            else
                return clienteBm.GetListaClienteByTipoCliente(idTipoCliente.Value);
        }

        public Cliente GetClienteById(int id)
        {
            return clienteBm.GetByID(id);
        }

        public void DesativarCliente(Cliente Cliente)
        {
            var clie = clienteBm.GetByID(Cliente.IdCliente);
            clie.Ativo = false;
            clienteBm.Update(clie);
        }

        public IList<CondicaoPagamento> GetListaCondicaoPagamento()
        {
            return condicaoPagamentoBm.GetAll().OrderBy(x => x.Descricao).ToList();
        }

        public void ReativarCliente(Cliente Cliente)
        {
            var clie = clienteBm.GetByID(Cliente.IdCliente);
            clie.Ativo = true;
            clienteBm.Update(clie);
        }

        public void InserirFornecedor(Cliente cliente)
        {
            cliente.CondicaoPagamentoPreferencial = condicaoPagamentoBm.GetByID(cliente.CondicaoPagamentoPreferencial.IdCondicaoPagamento);
            clienteBm.Insert(cliente);
        }

        public void AlterarCliente(Cliente cliente)
        {
            cliente.CondicaoPagamentoPreferencial = condicaoPagamentoBm.GetByID(cliente.CondicaoPagamentoPreferencial.IdCondicaoPagamento);
            clienteBm.Update(cliente);
        }

        public IList<ClienteContato> GetContatosByCliente(int IdCliente)
        {
            if (IdCliente == 0)
                return new List<ClienteContato>();
            else
                return clienteContatoBm.GetByCliente(GetClienteById(IdCliente));
        }


        public void AtualizarContato(List<ClienteContato> ListaContato, int idCliente)
        {
            var listaAtual = clienteContatoBm.GetByCliente(clienteBm.GetByID(idCliente)) ?? new List<ClienteContato>();

            foreach (var contatoAtual in listaAtual)
                clienteContatoBm.Delete(contatoAtual);


            foreach (var contato in ListaContato)
            {
                contato.Cliente = clienteBm.GetByID(idCliente);
                clienteContatoBm.Insert(contato);
            }
        }
    }
}
