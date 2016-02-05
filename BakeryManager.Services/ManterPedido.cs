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
    public class ManterPedido : BusinessProcessBase, IDisposable
    {

        private PedidoBM pedidoBm;
        private PedidoHistoricoStatusBM pedidoHistoricoStatusBm;
        private PedidoMaterialAdiconalBM pedidoMaterialAdiconalBm;
        private PedidoProdutoBM pedidoProdutoBm;
        private CategoriaProdutoBM categoriaProdutoBm;
        private ProdutoBM produtoBm;
        private ClienteBM clienteBm;
        private PedidoMaterialAdicionalPreConfigBM PreConfigPedidoMaterialAdicionalBm;
        private TipoPedidoBM tipoPedidoBm;
        private CondicaoPagamentoBM condicaoPagamentoBm;
        private MaterialAdicionalBM materialAdicionalBm;
        private FuncionarioBM functionarioBm;
        private UsuarioBM usuarioBm;
        private PedidoCancelamentoBM pedidoCancelamentoBm;
        private PedidoProdutoProduzidoBM pedidoProdutoProduzidoBm;
      

        public ManterPedido()
        {
            pedidoHistoricoStatusBm = GetObject<PedidoHistoricoStatusBM>();
            pedidoBm = GetObject<PedidoBM>();
            pedidoMaterialAdiconalBm = GetObject<PedidoMaterialAdiconalBM>();
            pedidoProdutoBm = GetObject<PedidoProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
            PreConfigPedidoMaterialAdicionalBm = GetObject<PedidoMaterialAdicionalPreConfigBM>();
            clienteBm = GetObject<ClienteBM>();
            tipoPedidoBm = GetObject<TipoPedidoBM>();
            condicaoPagamentoBm = GetObject<CondicaoPagamentoBM>();
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            materialAdicionalBm = GetObject<MaterialAdicionalBM>();
            functionarioBm = GetObject<FuncionarioBM>();
            usuarioBm = GetObject<UsuarioBM>();
            pedidoCancelamentoBm = GetObject<PedidoCancelamentoBM>();
            pedidoProdutoProduzidoBm = GetObject<PedidoProdutoProduzidoBM>();
            
        }

        public IList<TipoPedido> GetListaTipoPedido()
        {
            return tipoPedidoBm.GetAll();
        }

        public IList<CategoriaProduto> getListaCategoriaProduto()
        {
            return categoriaProdutoBm.GetAll();
        }

        public IList<CondicaoPagamento> GetListaCondicaoPagamento()
        {
            return condicaoPagamentoBm.GetAll();
        }

        public IList<Cliente> GetListaClientesByFiltro()
        {
            return clienteBm.GetAll();
        }

        public IList<Produto> GetProdutoByCategoria(int? idCategoriaProduto)
        {
            if (!idCategoriaProduto.HasValue)
                return produtoBm.GetAll();
            else
                return produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(idCategoriaProduto.Value));
        }

        public void Dispose()
        {
            pedidoBm.Dispose();
            pedidoHistoricoStatusBm.Dispose();
            pedidoMaterialAdiconalBm.Dispose();
            pedidoProdutoBm.Dispose();
            produtoBm.Dispose();
            PreConfigPedidoMaterialAdicionalBm.Dispose();
            clienteBm.Dispose();
            tipoPedidoBm.Dispose();
            condicaoPagamentoBm.Dispose();
            categoriaProdutoBm.Dispose();
            materialAdicionalBm.Dispose();
            functionarioBm.Dispose();
            usuarioBm.Dispose();
            pedidoCancelamentoBm.Dispose();
            pedidoProdutoProduzidoBm.Dispose();
        }

        public IList<Funcionario> GetListaFuncionarios()
        {
            return functionarioBm.GetListaFuncionariosAtivos();
        }

        public IList<PedidoProduto> GetProdutosByPedido(int idPedido)
        {
            if (idPedido == 0)
                return new List<PedidoProduto>();
            else
                return pedidoProdutoBm.GetPedidoProdutoByPedido(pedidoBm.GetByID(idPedido));
        }

        public Produto GetProdutoById(int idProduto)
        {
            return produtoBm.GetByID(idProduto);
        }

        public IList<MaterialAdicional> GetListaMaterialAdiconal(int?[] listaMaterialSelect)
        {
            if (listaMaterialSelect == null || listaMaterialSelect.Length == 0)
                return materialAdicionalBm.GetAll().Where(x => x.Ativo).ToList();
            else
                return materialAdicionalBm.GetMaterialForaLista(listaMaterialSelect.Select(x => x.Value).ToArray()).Where(x => x.Ativo).ToList();
        }

        public IList<PedidoMaterialAdiconal> GetMaterialAdicionalByPedido(int idPedido, int idTipoPedido, bool ignoraIdPedido)
        {
            if (idTipoPedido == 0 || !ignoraIdPedido)
            {
                if (idPedido == 0)
                    return pedidoMaterialAdiconalBm.GetAll().Where(x => x.Material.Ativo).ToList();
                else
                    return pedidoMaterialAdiconalBm.GetMaterialAdicionalByPedido(pedidoBm.GetByID(idPedido));
            }

            else
                return PreConfigPedidoMaterialAdicionalBm.GetPreConfiguracaoByTipoPedido(tipoPedidoBm.GetByID(idTipoPedido)).Select(x => new PedidoMaterialAdiconal()
                {
                    Material = x.Material,
                    Pedido = idPedido == 0 ? new Pedido()
                    {
                        IdPedido = 0
                    } : pedidoBm.GetByID(idPedido),
                    Quantidade = x.Quantidade,
                    TipoAquisicao = x.TipoAquisicao
                }).ToList();
        }

        public Cliente GetListaClienteById(int idCliente)
        {
            return clienteBm.GetByID(idCliente);
        }

        public CondicaoPagamento GetCondicaoPagamentoById(int idCondicaoPagamento)
        {
            return condicaoPagamentoBm.GetByID(idCondicaoPagamento);
        }

        public Funcionario GetFuncionarioById(int idFuncionario)
        {
            return functionarioBm.GetByID(idFuncionario);
        }

        public TipoPedido GetTipoPedidoById(int idTipoPedido)
        {
            return tipoPedidoBm.GetByID(idTipoPedido);
        }

        public void InserirPedido(Pedido pedido)
        {
            pedido.Natureza = NaturezaPedido.Encomenda;
            pedido.DataInclusao = DateTime.Now;
            pedidoBm.Insert(pedido);
            pedido.StatusAtual = StatusPedido.Encaminhado;
            pedido.NumeroPedido = pedido.IdPedido.ToString().PadLeft(6, '0');
            pedidoBm.Update(pedido);
        }

        public Pedido GetPedidoById(int idPedido)
        {
            return pedidoBm.GetByID(idPedido);
        }

        public MaterialAdicional GetMaterialAdcionalById(int idMaterialAdicional)
        {
            return materialAdicionalBm.GetByID(idMaterialAdicional);
        }

        public void AtualizarProdutoMaterialAdicional(List<PedidoProduto> listaProduto, List<PedidoMaterialAdiconal> listaMaterialAdicional, Pedido pedido)
        {
            //atualizando produto

            foreach (var produto in pedidoProdutoBm.GetPedidoProdutoByPedido(pedido))
                pedidoProdutoBm.Delete(produto);

            foreach (var prodNovo in listaProduto)
                pedidoProdutoBm.Insert(prodNovo);

            //Inserindo Material Adicional
            foreach (var material in pedidoMaterialAdiconalBm.GetMaterialAdicionalByPedido(pedido))
                pedidoMaterialAdiconalBm.Delete(material);

            foreach (var materialNovo in listaMaterialAdicional)
                pedidoMaterialAdiconalBm.Insert(materialNovo);
        }

        public IList<Pedido> GetListaPedidosEncaminhados()
        {
            return pedidoBm.GetListaPedidosEncaminhados();
        }

        public void EncaminharPedidoParaProducao(int idPedido,string UsuarioMudanca)
        {
            var pedido = pedidoBm.GetByID(idPedido);
            AtualizaMudancaStatu(pedido,pedido.StatusAtual, StatusPedido.AguardandoInicioProducao, UsuarioMudanca);
            pedido.StatusAtual = StatusPedido.AguardandoInicioProducao;
            pedidoBm.Update(pedido);
            
        }

        private void AtualizaMudancaStatu(Pedido pedido, StatusPedido StatusDe, StatusPedido StatusPara, string usuarioMudanca)
        {
            pedidoHistoricoStatusBm.Insert(new PedidoHistoricoStatus()
            {
                DataHoraMudança = DateTime.Now,
                StatusDe = StatusDe,
                StatusPara = StatusPara,
                UsuarioResponsavel = usuarioBm.GetByLogin(usuarioMudanca)
            });
        }

        public void AlterarPedido(Pedido pedido)
        {
            pedidoBm.Update(pedido);
        }

        public void CancelarPedido(PedidoCancelamento PedidoCancelamento)
        {
            AtualizaMudancaStatu(PedidoCancelamento.Pedido, PedidoCancelamento.Pedido.StatusAtual, StatusPedido.Cancelado, PedidoCancelamento.UsuarioCancelamento.Login);
            PedidoCancelamento.Pedido.StatusAtual = StatusPedido.Cancelado;
            pedidoBm.Update(PedidoCancelamento.Pedido);
            pedidoCancelamentoBm.Insert(PedidoCancelamento);
        }

        public Usuario GetUsuarioByLogin(string Login)
        {
            return usuarioBm.GetByLogin(Login);
        }

        public IList<Pedido> GetListaPedidosAguardandoProducao()
        {
            return pedidoBm.GetListaPedidosAguardandoProducao();
        }

        public void EnviarParaLinhadeProducao(int idPedido, string UsuarioMudanca)
        {
            var pedido = pedidoBm.GetByID(idPedido);
            AtualizaMudancaStatu(pedido, pedido.StatusAtual, StatusPedido.EmProducao, UsuarioMudanca);
            pedido.StatusAtual = StatusPedido.EmProducao;
            pedidoBm.Update(pedido);
        }

        public IList<Pedido> GetListaPedidosEmProducao()
        {
            return pedidoBm.GetListaPedidosEmProducao();
        }

        public bool VerificaProdutosProduzidos(Pedido pedido)
        {

            var retorno = pedidoProdutoProduzidoBm.GetProdutosByPedido(pedido);

            if (retorno == null || retorno.Count == 0)
                return false;


            return !retorno.Any(x => x.StatusAtual != StatusProducaoProduto.Concluido &&
                                     x.StatusAtual != StatusProducaoProduto.Cancelado);
        }
    }
}
