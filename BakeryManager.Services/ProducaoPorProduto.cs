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
    public class ProducaoPorProduto : BusinessProcessBase, IDisposable
    {
        private PedidoProdutoProduzidoBM pedidoProdutoProduzidoBm;
        private ProdutoBM produtoBm;
        private PedidoBM pedidoBm;
        private PedidoProdutoBM pedidoProdutoBm;
        private PedidoProdutoProduzidoHistoricoProducaoBM pedidoProdutoProduzidoHistoricoProducaoBm;
        private UsuarioBM usuarioBm;
        private PedidoHistoricoStatusBM pedidoHistoricoStatusBm;

        public ProducaoPorProduto()
        {
            pedidoProdutoProduzidoBm = GetObject<PedidoProdutoProduzidoBM>();
            produtoBm = GetObject<ProdutoBM>();
            pedidoBm = GetObject<PedidoBM>();
            pedidoProdutoBm = GetObject<PedidoProdutoBM>();
            pedidoProdutoProduzidoHistoricoProducaoBm = GetObject<PedidoProdutoProduzidoHistoricoProducaoBM>();
            usuarioBm = GetObject<UsuarioBM>();
            pedidoHistoricoStatusBm = GetObject<PedidoHistoricoStatusBM>();
        }



        public void Dispose()
        {
            pedidoProdutoProduzidoBm.Dispose();
            produtoBm.Dispose();
            pedidoBm.Dispose();
            pedidoProdutoBm.Dispose();
            usuarioBm.Dispose();
            pedidoProdutoProduzidoHistoricoProducaoBm.Dispose();
            pedidoHistoricoStatusBm.Dispose();
        }

        public IList<PedidoProdutoProduzido> GetListaProdutoEmProducao(DateTime dataEntrega, DateTime? horaEntrega)
        {
            var listaPedidos = pedidoBm.GetPedidosByFiltro(dataEntrega, null, null, string.Empty).Where(x => x.StatusAtual == StatusPedido.EmProducao).ToList();

            var listaProd = pedidoProdutoProduzidoBm.GetProdutosProduzidosByPedidoList(listaPedidos)
                .Distinct()
                .Select(x => new PedidoProdutoProduzido()
                {
                    DataHoraFimFabricacao = x.DataHoraFimFabricacao,
                    DataHoraInicioFabricacao = x.DataHoraInicioFabricacao,
                    Produto = x.Produto,
                    Quantidade = x.Quantidade,
                    StatusAtual = x.StatusAtual,
                    TempoProducao = x.TempoProducao
                }).ToList();


            


            if (listaProd == null || listaProd.Count == 0)
                listaProd = pedidoProdutoBm.GetPedidoProdutoByPedidoList(listaPedidos).Distinct().Select(x => new PedidoProdutoProduzido()
                {
                    Produto = x.Produto,
                    Quantidade = x.Quantidade,
                    StatusAtual = StatusProducaoProduto.AgardandoInicio
                }).ToList();

            else
            {
                var ListaProdutoProduzido = listaProd.Select(x => x.Produto.IdProduto).ToArray();

                var listaProdNaoIniciado = pedidoProdutoBm.GetPedidoProdutoByPedidoList(listaPedidos).Distinct().Where(x => !ListaProdutoProduzido.Contains(x.Produto.IdProduto))
                    .Select(x => new PedidoProdutoProduzido()
                    {
                        Produto = x.Produto,
                        Quantidade = x.Quantidade,
                        StatusAtual = StatusProducaoProduto.AgardandoInicio
                    }).ToList();

                if (listaProdNaoIniciado.Count > 0 )
                {
                    foreach (var prodNaoiniciado in listaProdNaoIniciado)
                        listaProd.Add(prodNaoiniciado);
                    
                }
            }

            var retorno = listaProd.GroupBy(x => x.Produto)
                          .Select(g => new { Produto = g.Key, Quantidade = g.Sum(x => x.Quantidade) })
                          .ToList()
                          .Select(x => new PedidoProdutoProduzido()
                          {
                              Produto = x.Produto,
                              Quantidade = x.Quantidade,
                              TempoProducao = listaProd.FirstOrDefault(y => y.Produto.IdProduto == x.Produto.IdProduto).TempoProducao,
                              StatusAtual = listaProd.FirstOrDefault(y => y.Produto.IdProduto == x.Produto.IdProduto).StatusAtual
                          }).ToList();

            return retorno;


        }

        private IList<Pedido> GetPedidosEmProducaoByProduto(Produto produto)
        {
            return pedidoProdutoBm.GetPedidoProdutoByProdutoAndStatusAtual(produto, StatusPedido.EmProducao).Select(x => x.Pedido).Distinct().ToList();
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

        public void IniciarProducao(int idProduto, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var produto = produtoBm.GetByID(idProduto);

            var ListaPedidos = GetPedidosEmProducaoByProduto(produto);

            foreach (var pedido in ListaPedidos)
            {


                var pedidoProdutoProduzido = new PedidoProdutoProduzido()
                {
                    DataHoraInicioFabricacao = DateTime.Now,
                    Pedido = pedido,
                    Produto = produto,
                    StatusAtual = StatusProducaoProduto.Produzindo,
                    TempoProducao = 0,
                    Quantidade = pedidoProdutoBm.GetPedidoProdutoByPedidoEProduto(pedido, produto).Quantidade

                };

                pedidoProdutoProduzidoBm.Insert(pedidoProdutoProduzido);

                AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Inicio, UsuarioAtualizacao, IpAtualizacao, pedidoProdutoProduzido);

            }

        }



        public void PausarProducao(int idProduto, int tempoDecorrido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var produto = produtoBm.GetByID(idProduto);

            var ListaPedidos = GetPedidosEmProducaoByProduto(produto);

            foreach (var pedido in ListaPedidos)
            {
                var pedidoProdutoProducao = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);
                pedidoProdutoProducao.TempoProducao = tempoDecorrido;
                pedidoProdutoProducao.StatusAtual = StatusProducaoProduto.Pausa;

                pedidoProdutoProduzidoBm.Update(pedidoProdutoProducao);

                AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Pausa, UsuarioAtualizacao, IpAtualizacao, pedidoProdutoProducao);
            }

        }



        public int ContinuarProducao(int idProduto, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var produto = produtoBm.GetByID(idProduto);

            var ListaPedidos = GetPedidosEmProducaoByProduto(produto);

            int tempoDecorrido = 0;
            foreach (var pedido in ListaPedidos)
            {
                var pedidoProdutoProducao = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);
                pedidoProdutoProducao.StatusAtual = StatusProducaoProduto.Produzindo;
                tempoDecorrido = pedidoProdutoProducao.TempoProducao.Value;

                pedidoProdutoProduzidoBm.Update(pedidoProdutoProducao);

                AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Continuacao, UsuarioAtualizacao, IpAtualizacao, pedidoProdutoProducao);
            }

            return tempoDecorrido;
        }

        public void IncluirProducaoPedido(PedidoProdutoProduzido produtoProduzido)
        {
            var produto = produtoBm.GetByID(produtoProduzido.Produto.IdProduto);

            var ListaPedidos = pedidoBm.GetListaPedidosEmProducao();

            foreach (var pedido in ListaPedidos)
            {
                var pedidoProduto = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);

                if (pedidoProduto != null)
                {
                    pedidoProduto.Quantidade = produtoProduzido.Quantidade;
                    pedidoProduto.TempoProducao = produtoProduzido.TempoProducao;
                    pedidoProduto.StatusAtual = produtoProduzido.StatusAtual;

                    pedidoProdutoProduzidoBm.Update(pedidoProduto);

                }
                else {
               
                    if (pedidoProdutoBm.GetPedidoProdutoByPedidoEProduto(pedido, produto) != null)
                    {
                        produtoProduzido.DataHoraInicioFabricacao = DateTime.Now;
                        produtoProduzido.TempoProducao = 0;
                        pedidoProdutoProduzidoBm.Insert(produtoProduzido);
                    }
                    
                }
            }
        }

        public Produto GetProdutoById(int idProduto)
        {
            return produtoBm.GetByID(idProduto);
        }

        public void FinalizarProducao(int idProduto, int tempoDecorrido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var produto = produtoBm.GetByID(idProduto);

            var ListaPedidos = GetPedidosEmProducaoByProduto(produto);


            foreach (var pedido in ListaPedidos)
            {
                var pedidoProdutoProducao = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);
                pedidoProdutoProducao.StatusAtual = StatusProducaoProduto.Concluido;
                pedidoProdutoProducao.TempoProducao = tempoDecorrido;
                pedidoProdutoProducao.DataHoraFimFabricacao = DateTime.Now;


                pedidoProdutoProduzidoBm.Update(pedidoProdutoProducao);

                AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Finalizacao, UsuarioAtualizacao, IpAtualizacao, pedidoProdutoProducao);


                if (VerificaPedidoFinalizado(pedido))
                {
                    var pedidoaltualizacao = pedidoBm.GetByID(pedido.IdPedido);
                    pedidoaltualizacao.StatusAtual = StatusPedido.AguardandoEntrega;
                    pedidoBm.Update(pedidoaltualizacao);

                    var pedidoHistorico = new PedidoHistoricoStatus()
                    {
                        DataHoraMudança = DateTime.Now,
                        StatusDe = StatusPedido.EmProducao,
                        Pedido = pedidoBm.GetByID(pedido.IdPedido),
                        StatusPara = StatusPedido.AguardandoEntrega,
                        UsuarioResponsavel = usuarioBm.GetByLogin(UsuarioAtualizacao)
                    };

                    pedidoHistoricoStatusBm.Insert(pedidoHistorico);

                }

            }
        }

        public void CancelarProducao(int idProduto, int tempoDecorrido, string UsuarioAtualizacao, string IpAtualizacao)
        {
            var produto = produtoBm.GetByID(idProduto);

            var ListaPedidos = GetPedidosEmProducaoByProduto(produto);


            foreach (var pedido in ListaPedidos)
            {
                var pedidoProdutoProducao = pedidoProdutoProduzidoBm.GetByPedidoAndProduto(pedido, produto);
                pedidoProdutoProducao.StatusAtual = StatusProducaoProduto.Cancelado;
                pedidoProdutoProducao.TempoProducao = tempoDecorrido;

                pedidoProdutoProduzidoBm.Update(pedidoProdutoProducao);

                AtualiarHistoricoStatusProducao(StatusProducaoHistorico.Cancelado, UsuarioAtualizacao, IpAtualizacao, pedidoProdutoProducao);


                if (VerificaPedidoFinalizado(pedido))
                {
                    var pedidoaltualizacao = pedidoBm.GetByID(pedido.IdPedido);
                    pedidoaltualizacao.StatusAtual = StatusPedido.Cancelado;
                    pedidoBm.Update(pedidoaltualizacao);

                    var pedidoHistorico = new PedidoHistoricoStatus()
                    {
                        DataHoraMudança = DateTime.Now,
                        StatusDe = StatusPedido.EmProducao,
                        Pedido = pedidoBm.GetByID(pedido.IdPedido),
                        StatusPara = StatusPedido.Cancelado,
                        UsuarioResponsavel = usuarioBm.GetByLogin(UsuarioAtualizacao)
                    };

                    pedidoHistoricoStatusBm.Insert(pedidoHistorico);
                }
            }
        }
    }
}
