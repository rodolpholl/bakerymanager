using BakeryManager.Entities;
using BakeryManager.Repositories;
using BakeryManager.Repositories.Seguranca;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BakeryManager.Services
{
    public class CadastroIngredientes : BusinessProcessBase, IDisposable
    {
        private IngredienteBM ingredienteBm;
        private TabelaNutricionalBM tabelaNutricionalBm;
        private IngredienteHistoricoDesativacaoBM historicoDesativacaoReativacaoBm;
        private UsuarioBM usuarioBm;
        private CategoriaIngredienteBM categoriaIngredienteBm;
        private IngredienteTabelaNutricionalBM ingredienteTabelaNutricionalBm;

        public CadastroIngredientes()
        {
            ingredienteBm = base.GetObject<IngredienteBM>();
            tabelaNutricionalBm = base.GetObject<TabelaNutricionalBM>();
            historicoDesativacaoReativacaoBm = GetObject<IngredienteHistoricoDesativacaoBM>();
            usuarioBm = GetObject<UsuarioBM>();
            categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
            ingredienteTabelaNutricionalBm = GetObject<IngredienteTabelaNutricionalBM>();
        }

        public void Dispose()
        {
            ingredienteBm.Dispose();
            tabelaNutricionalBm.Dispose();
            historicoDesativacaoReativacaoBm.Dispose();
            usuarioBm.Dispose();
            categoriaIngredienteBm.Dispose();
            ingredienteTabelaNutricionalBm.Dispose();
        }

        public void InserirIngrediente(Ingrediente Ingrediente)
        {
            ingredienteBm.Insert(Ingrediente);
        }

        public IList<CategoriaIngrediente> GetCategoriaAll()
        {
            return categoriaIngredienteBm.GetAll().OrderBy(x => x.Nome).ToList();
        }

        public void InserirIngrediente(Ingrediente Ingrediente, IList<IngredienteTabelaNutricional> pTabelaNutricioanl)
        {
            ingredienteBm.Insert(Ingrediente);
           
        }

        public IList<Ingrediente> GetListaIngredientes()
        {
            return ingredienteBm.GetAll();
        }

        public IList<TabelaNutricional> GetTabelaNutricionalAll()
        {
            return tabelaNutricionalBm.GetAll();
        }

        public IList<Ingrediente> GetListaIngredientesByFiltro(string textoPesquisa)
        {
            if (textoPesquisa.Length > 0 && textoPesquisa.Length < 3)
                return new List<Ingrediente>();

            return ingredienteBm.GetListaIngredientesByFiltro(textoPesquisa);

        }

        public void AlterarIngrediente(Ingrediente Ingrediente)
        {
            ingredienteBm.Update(Ingrediente);
           
        }

        

        public void CarregarTabelaNutricional(string FileName)
        {
            
            throw new NotImplementedException();
            
        }

        public CategoriaIngrediente GetCategoriaById(int idCategoriaIngrediente)
        {
            return categoriaIngredienteBm.GetByID(idCategoriaIngrediente);
        }

       

        public Ingrediente GetIngredienteById(int pIdIngrediente)
        {
            return ingredienteBm.GetByID(pIdIngrediente);
        }

        private double TratarInformacaoTAbela(string Texto)
        {
            return Texto.ToUpper() == "NA" ? 0 : 
                   Texto.ToUpper() == "TR" ? 0 :
                   Texto           == "*"  ? 0 :
                   string.IsNullOrWhiteSpace(Texto) ? 0 : 
                   double.Parse(Texto);
        }

        

        public Usuario GetUsuarioByLogin(string name)
        {
            return usuarioBm.GetByLogin(name);
        }

        public void DesativarIngrediente(Ingrediente pIngrediente, Usuario pUsuario, string Ip)
        {
            pIngrediente.Ativo = false;
            ingredienteBm.Update(pIngrediente);
            RegistrarHistóricoDesabilitarHabilitar(pIngrediente.IdIngrediente, pUsuario.IdUsuario, Ip, TipoOpracaoDesativacaoIngrediente.Desativar);
        }

       
        public void ReativarIngrediente(Ingrediente pIngrediente, Usuario pUsuario, string Ip)
        {
            pIngrediente.Ativo = true;
            ingredienteBm.Update(pIngrediente);
            RegistrarHistóricoDesabilitarHabilitar(pIngrediente.IdIngrediente, pUsuario.IdUsuario, Ip, TipoOpracaoDesativacaoIngrediente.Reativar);
        }

        private void RegistrarHistóricoDesabilitarHabilitar(int pIngrediente, int pUsuario, string Ip, TipoOpracaoDesativacaoIngrediente TipoOperacao)
        {
            var hist = new IngredienteHistoricoDesativacao()
            {
                DataHoraOperacao = DateTime.Now,
                Ingrediente = ingredienteBm.GetByID(pIngrediente),
                UsuarioOperacao = usuarioBm.GetByID(pUsuario),
                IpOperacao = Ip,
                TipoOperacao = (int)TipoOperacao
            };

            historicoDesativacaoReativacaoBm.Insert(hist);
        }

        public IList<IngredienteHistoricoDesativacao> GetHistoricoDesativacaoReativacaoById(int idIngrediente)
        {
            return historicoDesativacaoReativacaoBm.GetHistoricoDesativacaoByIngrediente(GetIngredienteById(idIngrediente));
        }

        public IList<IngredienteTabelaNutricional> GetInformacaoNutricional(int idIngrediente)
        {

            if (idIngrediente == 0)
                return tabelaNutricionalBm.GetAll().Select(x => new IngredienteTabelaNutricional()
                {
                    Componente = x,
                    Valor = 0,
                    Ingrediente = new Ingrediente()
                }).ToList();
            else
                return ingredienteTabelaNutricionalBm.GetInformacoesNutricionaisByIngrediente(GetIngredienteById(idIngrediente));
        }

        public TabelaNutricional GetTabelaNutricionalById(int idTabelaNutricionalModel)
        {
            return tabelaNutricionalBm.GetByID(idTabelaNutricionalModel);
        }

        public void AtualizarTabelaNutricional(List<IngredienteTabelaNutricional> listaIngredienteTabela)
        {
            foreach(var comp in listaIngredienteTabela)
            {
                comp.PercValorDiario = CalculaPercentualDiario(comp);

                if (comp.IdIngredienteTabelaNutricional != 0)
                    ingredienteTabelaNutricionalBm.Update(comp);
                else
                    ingredienteTabelaNutricionalBm.Insert(comp);
            }
        }

        private double? CalculaPercentualDiario(IngredienteTabelaNutricional pIngrediente)
        {
            if (!pIngrediente.Componente.ValorDiario.HasValue)
                return null;
            else
                return Math.Round((pIngrediente.Valor / pIngrediente.Componente.ValorDiario.Value) * 100,2);
        }
        
    }
}
