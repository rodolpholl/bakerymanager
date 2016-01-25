using BakeryManager.Entities;
using BakeryManager.Repositories;
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
    }
}
