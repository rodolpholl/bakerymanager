using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class PreConfiguracaoTipoPedido : BusinessProcessBase, IDisposable
    {
        private PedidoMaterialAdicionalPreConfigBM pedidoMaterialAdicionalPreConfigBm;
        private TipoPedidoBM tipoPedidooBm;
        private MaterialAdicionalBM materiaAdicionalBm;
        public PreConfiguracaoTipoPedido()
        {
            pedidoMaterialAdicionalPreConfigBm = GetObject<PedidoMaterialAdicionalPreConfigBM>();
            tipoPedidooBm = GetObject<TipoPedidoBM>();
            materiaAdicionalBm = GetObject<MaterialAdicionalBM>();
        }

        public IList<TipoPedido> GetListaTipoPedido()
        {
            return tipoPedidooBm.GetListaAtivos();
        }

        public void Dispose()
        {
            pedidoMaterialAdicionalPreConfigBm.Dispose();
            tipoPedidooBm.Dispose();
            materiaAdicionalBm.Dispose();
        }

        public IList<PedidoMaterialAdicionalPreConfig> GetPreConfiguracaoByTipoPedido(int idTipoPedido)
        {
            if (idTipoPedido == 0)
                return new List<PedidoMaterialAdicionalPreConfig>();
            else
                return pedidoMaterialAdicionalPreConfigBm.GetPreConfiguracaoByTipoPedido(tipoPedidooBm.GetByID(idTipoPedido));
        }

        public IList<MaterialAdicional> GetListaMateriais(int?[] listaMateriaisSelecionados)
        {
            if (listaMateriaisSelecionados == null || listaMateriaisSelecionados.Length == 0)
                return materiaAdicionalBm.GetAll();
            else
                return materiaAdicionalBm.GetAll().Where(x => !listaMateriaisSelecionados.Contains(x.IdMaterialAdicional)).ToList();
        }

        public void AtualizarConfiguracao(int idTipoPedido, List<PedidoMaterialAdicionalPreConfig> ListaPreConfiguracao)
        {
            var listaAtual = pedidoMaterialAdicionalPreConfigBm.GetPreConfiguracaoByTipoPedido(tipoPedidooBm.GetByID(idTipoPedido));
            foreach (var atual in listaAtual)
                pedidoMaterialAdicionalPreConfigBm.Delete(atual);

            foreach(var preConf in ListaPreConfiguracao)
            {
                preConf.Evento = tipoPedidooBm.GetByID(idTipoPedido);
                preConf.Material = materiaAdicionalBm.GetByID(preConf.Material.IdMaterialAdicional);
                pedidoMaterialAdicionalPreConfigBm.Insert(preConf);
            }
        }

        public MaterialAdicional GetMaterialAdicionalById(int idMaterialAdicional)
        {
            return materiaAdicionalBm.GetByID(idMaterialAdicional);
        }

        public TipoPedido GetTipoPedidoById(int idTipoPedido)
        {
            return tipoPedidooBm.GetByID(idTipoPedido);
        }

        public void InserirPreConfiguracao(PedidoMaterialAdicionalPreConfig preConf)
        {
            pedidoMaterialAdicionalPreConfigBm.Insert(preConf);
        }

        public PedidoMaterialAdicionalPreConfig GetPreConfiguracaoById(int idPedidoMaterialAdicionalPreConfig)
        {
            return pedidoMaterialAdicionalPreConfigBm.GetByID(idPedidoMaterialAdicionalPreConfig);
        }

        public void AtualizarPreConfiguracao(PedidoMaterialAdicionalPreConfig preConfig)
        {
            pedidoMaterialAdicionalPreConfigBm.Update(preConfig);
        }

        public void DeletarPreConfiguracao(PedidoMaterialAdicionalPreConfig preConf)
        {
            pedidoMaterialAdicionalPreConfigBm.Delete(preConf);
        }
    }
}
