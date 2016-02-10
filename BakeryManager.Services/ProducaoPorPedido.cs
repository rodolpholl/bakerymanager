using BakeryManager.Entities;
using BakeryManager.Repositories;
using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ProducaoPorPedido : BusinessProcessBase, IDisposable
    {
        private PedidoProdutoProduzidoBM pedidoProdutoProduzidoBm;
        private ClienteBM clienteBm;
        private ProdutoBM produtoBm;
        private PedidoBM  pedidoBm;
        private PedidoProdutoBM pedidoProdutoBm;
        private PedidoProdutoProduzidoHistoricoProducaoBM pedidoProdutoProduzidoHistoricoProducaoBm;
        private PedidoHistoricoStatusBM pedidoHistoricoStatusBm;
        private UsuarioBM usuarioBm;

        public ProducaoPorPedido()
        {
            pedidoProdutoProduzidoBm = GetObject<PedidoProdutoProduzidoBM>();
            clienteBm = GetObject<ClienteBM>();
            produtoBm = GetObject<ProdutoBM>();
            pedidoBm  = GetObject<PedidoBM>();
            pedidoProdutoBm = GetObject<PedidoProdutoBM>();
            pedidoProdutoProduzidoHistoricoProducaoBm = GetObject<PedidoProdutoProduzidoHistoricoProducaoBM>();
            usuarioBm = GetObject<UsuarioBM>();
            pedidoHistoricoStatusBm = GetObject<PedidoHistoricoStatusBM>();
        }

        public void Dispose()
        {
            pedidoProdutoProduzidoBm.Dispose();
            clienteBm.Dispose();
            produtoBm.Dispose();
            pedidoBm.Dispose();
            pedidoProdutoBm.Dispose();
            usuarioBm.Dispose();
            pedidoProdutoProduzidoHistoricoProducaoBm.Dispose();
            pedidoHistoricoStatusBm.Dispose();
        }

        public IList<Cliente> GetListaCliente()
        {

            return pedidoBm.GetListaPedidosEmProducao().Select(x => x.Cliente).Distinct().ToList();

        }

        public IList<Produto> GetListaProduto()
        {
            return produtoBm.GetProdutosAtivos();
        }

        public Pedido GetPedidoByNumero(string NumeroPedido)
        {
            return pedidoBm.getPedidoByNumero(NumeroPedido);
        }

        public IList<Pedido> GetPedidosByFiltro(DateTime DataEntrega, DateTime? HorarioEntrega, int? IdCliente, string NumeroPedido)
        {
            return pedidoBm.GetPedidosByFiltro(DataEntrega, HorarioEntrega, clienteBm.GetByID(IdCliente.HasValue ? IdCliente.Value : 0), NumeroPedido)
                   .Where(x => x.StatusAtual == StatusPedido.EmProducao).ToList();
        }

        public IList<PedidoProdutoProduzido> GetProdutosProducaoByPedido(int IdPedido)
        {
            var pedido = pedidoBm.GetByID(IdPedido);
            var listaRetorno = pedidoProdutoProduzidoBm.GetProdutosByPedido(pedido);

            if (listaRetorno.Count > 0)
            {


                var listaIdsExistentes = listaRetorno.Select(x => x.Produto.IdProduto).ToArray();


                var listaProdNaoAtualizado = pedidoProdutoBm.GetPedidoProdutoByPedido(pedido)
                    .Where(x => !listaIdsExistentes.Contains(x.Produto.IdProduto))
                    .Select(x => new PedidoProdutoProduzido()
                    {
                        Produto = x.Produto,
                        Pedido = pedido,
                        Quantidade = x.Quantidade,
                        StatusAtual = StatusProducaoProduto.AgardandoInicio
                    }).ToList();


                if (listaProdNaoAtualizado.Count > 0)
                    foreach (var item in listaProdNaoAtualizado)
                        listaRetorno.Add(item);

                return listaRetorno;

            }
            else
            {
                var listaProduto = pedidoProdutoBm.GetPedidoProdutoByPedido(pedido).Select(x => new PedidoProdutoProduzido()
                {
                    Produto = x.Produto,
                    Pedido = pedido,
                    Quantidade = x.Quantidade,
                    StatusAtual = StatusProducaoProduto.AgardandoInicio
                }).ToList();

                return listaProduto;
            }
        }

        public void IniciarProducaoPedido(int idProduto, int IdPedido, int Quantidade, string LoginUsuario, string IpAtualzacao)
        {
            var pedido = pedidoBm.GetByID(IdPedido);
            var produto = produtoBm.GetByID(idProduto);

            var pedidoProdutoProduzido = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);

            if (pedidoProdutoProduzido != null)
            {
                pedidoProdutoProduzido.StatusAtual = StatusProducaoProduto.Produzindo;
                pedidoProdutoProduzido.Quantidade = Quantidade;
                pedidoProdutoProduzidoBm.Update(pedidoProdutoProduzido);
            }
            else
            {
                pedidoProdutoProduzido = new PedidoProdutoProduzido()
                {
                    DataHoraInicioFabricacao = DateTime.Now,
                    Pedido = pedido,
                    Quantidade = Quantidade,
                    Produto = produtoBm.GetByID(idProduto),
                    StatusAtual = StatusProducaoProduto.Produzindo
                };

                pedidoProdutoProduzidoBm.Insert(pedidoProdutoProduzido);
            }
            

            AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Inicio,LoginUsuario, IpAtualzacao, pedidoProdutoProduzido);

            
        }

        public void PausarProducao(int idProduto, int idPedido, int TempoDecorrido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var pedido = pedidoBm.GetByID(idPedido);

            var PedidoProduzido = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedidoBm.GetByID(idPedido), produtoBm.GetByID(idProduto));

            PedidoProduzido.StatusAtual = StatusProducaoProduto.Pausa;
            PedidoProduzido.TempoProducao = TempoDecorrido;
            pedidoProdutoProduzidoBm.Update(PedidoProduzido);

            AtualiarHistoricoStatusProducao( StatusProducaoHistorico.Pausa,UsuarioAtualizacao, IpAtualizacao, PedidoProduzido);

        }

        public void ContinuarProducao(int idProduto, int idPedido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var pedido = pedidoBm.GetByID(idPedido);

            var PedidoProduzido = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedidoBm.GetByID(idPedido), produtoBm.GetByID(idProduto));

            PedidoProduzido.StatusAtual = StatusProducaoProduto.Produzindo;
            pedidoProdutoProduzidoBm.Update(PedidoProduzido);

            AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Continuacao, UsuarioAtualizacao, IpAtualizacao, PedidoProduzido);

        }

        public void FinalizarProducao(PedidoProdutoProduzido PedidoProduzido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            

            PedidoProduzido.StatusAtual = StatusProducaoProduto.Concluido;
            PedidoProduzido.DataHoraInicioFabricacao = DateTime.Now;
            
            pedidoProdutoProduzidoBm.Update(PedidoProduzido);

            AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Finalizacao, UsuarioAtualizacao, IpAtualizacao, PedidoProduzido);

            


            if (VerificaPedidoFinalizado(PedidoProduzido.Pedido))
            {
                var pedido = pedidoBm.GetByID(PedidoProduzido.Pedido.IdPedido);
                pedido.StatusAtual = StatusPedido.AguardandoEntrega;
                pedidoBm.Update(pedido);

                var pedidoHistorico = new PedidoHistoricoStatus()
                {
                    DataHoraMudança = DateTime.Now,
                    StatusDe = StatusPedido.EmProducao,
                    Pedido = pedidoBm.GetByID(PedidoProduzido.Pedido.IdPedido),
                    StatusPara = StatusPedido.AguardandoEntrega,
                    UsuarioResponsavel = usuarioBm.GetByLogin(UsuarioAtualizacao)
                };

                pedidoHistoricoStatusBm.Insert(pedidoHistorico);

            }
        }

        public bool VerificaPedidoFinalizado(Pedido pedido)
        {
            var listaPedidos = pedidoProdutoProduzidoBm.GetProdutosByPedido(pedido);

            var listaIdPedidosProducao = listaPedidos.Select(x => x.Produto.IdProduto);

            var listaNaoINcluso = pedidoProdutoBm.GetPedidoProdutoByPedido(pedido).Where(x => !listaIdPedidosProducao.Contains(x.Produto.IdProduto)).Select(x => new PedidoProdutoProduzido()
            {
                Produto = x.Produto,
                StatusAtual = StatusProducaoProduto.AgardandoInicio
            }).ToList();

            if (listaNaoINcluso.Count > 0)
                foreach (var item in listaNaoINcluso)
                    listaPedidos.Add(item);


            return !listaPedidos.Any(x => x.StatusAtual != StatusProducaoProduto.Concluido && x.StatusAtual != StatusProducaoProduto.Cancelado);

        }

        public void CancelarProducao(int idProduto, int idPedido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var pedido = pedidoBm.GetByID(idPedido);

            var PedidoProduzido = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedidoBm.GetByID(idPedido), produtoBm.GetByID(idProduto));

            PedidoProduzido.StatusAtual = StatusProducaoProduto.Cancelado;
            pedidoProdutoProduzidoBm.Update(PedidoProduzido);

            AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Cancelado, UsuarioAtualizacao, IpAtualizacao, PedidoProduzido);

            

            if (VerificaPedidoFinalizado(pedido))
            {

                pedido.StatusAtual = StatusPedido.AguardandoEntrega;
                pedidoBm.Update(pedido);

                var pedidoHistorico = new PedidoHistoricoStatus()
                {
                    DataHoraMudança = DateTime.Now,
                    StatusDe = StatusPedido.EmProducao,
                    Pedido = pedidoBm.GetByID(PedidoProduzido.Pedido.IdPedido),
                    StatusPara = StatusPedido.Cancelado,
                    UsuarioResponsavel = usuarioBm.GetByLogin(UsuarioAtualizacao)
                };

                pedidoHistoricoStatusBm.Insert(pedidoHistorico);

            }

        }
        
        public PedidoProdutoProduzido PausarProducao(int idProduto, int idPedido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var pedido = pedidoBm.GetByID(idPedido);

            var PedidoProduzido = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedidoBm.GetByID(idPedido), produtoBm.GetByID(idProduto));

            PedidoProduzido.StatusAtual = StatusProducaoProduto.Produzindo;
            pedidoProdutoProduzidoBm.Update(PedidoProduzido);

            AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Continuacao, UsuarioAtualizacao, IpAtualizacao, PedidoProduzido);

            return PedidoProduzido;
        }

        private void AtualiarHistoricoStatusProducao(StatusProducaoHistorico Status, string UsuarioAtualizacao, string IpAtualizacao, PedidoProdutoProduzido PedidoProduzido)
        {
            pedidoProdutoProduzidoHistoricoProducaoBm.Insert(new PedidoProdutoProduzidoHistoricoProducao
            {
                DataHoraIncluscai = DateTime.Now,
                IpAtualizacao = IpAtualizacao,
                UuarioResponsável = usuarioBm.GetByLogin(UsuarioAtualizacao),
                PedidoProdutoProduzido = PedidoProduzido,
                Status = Status
            });
        }

        public Pedido GetPedidoById(int idPedido)
        {
            return pedidoBm.GetByID(idPedido);
        }

        public Produto GetProdutoById(int idProduto)
        {
            return produtoBm.GetByID(idProduto);
        }

        public void IncluirProducaoPedido(PedidoProdutoProduzido produtoProduzido)
        {
            var pedidoProduto = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(produtoProduzido.Pedido, produtoProduzido.Produto);

            if (pedidoProduto != null)
            {
                pedidoProduto.Quantidade = produtoProduzido.Quantidade;
                pedidoProduto.TempoProducao = produtoProduzido.TempoProducao;
                pedidoProduto.StatusAtual = produtoProduzido.StatusAtual;
                
                pedidoProdutoProduzidoBm.Update(pedidoProduto);
                
            }
            else {
                produtoProduzido.DataHoraInicioFabricacao = DateTime.Now;
                pedidoProdutoProduzidoBm.Insert(produtoProduzido);
            }

        }
    }
}
