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
        private IngredienteBM ingreditenteBm;
        private TabelaNutricionalBM tabelaNutricionalBm;
        private IngredienteHistoricoDesativacaoBM historicoDesativacaoReativacaoBm;
        private UsuarioBM usuarioBm;
        private CategoriaIngredienteBM categoriaIngredienteBm;

        public CadastroIngredientes()
        {
            ingreditenteBm = base.GetObject<IngredienteBM>();
            tabelaNutricionalBm = base.GetObject<TabelaNutricionalBM>();
            historicoDesativacaoReativacaoBm = GetObject<IngredienteHistoricoDesativacaoBM>();
            usuarioBm = GetObject<UsuarioBM>();
            categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
        }

        public void InserirIngrediente(Ingrediente Ingrediente)
        {
            ingreditenteBm.Insert(Ingrediente);
        }

        public IList<CategoriaIngrediente> GetCategoriaAll()
        {
            return categoriaIngredienteBm.GetAll().OrderBy(x => x.Nome).ToList();
        }

        public void InserirIngrediente(Ingrediente Ingrediente, TabelaNutricional pTabelaNutricioanl)
        {
            ingreditenteBm.Insert(Ingrediente);
            AtualizarTAbelaNutricional(Ingrediente, pTabelaNutricioanl);
        }

        public IList<Ingrediente> GetListaIngredientes()
        {
            return ingreditenteBm.GetAll();
        }

        public IList<TabelaNutricional> GetTabelaNutricionalAll()
        {
            return tabelaNutricionalBm.GetAll();
        }

        public IList<Ingrediente> GetListaIngredientesByFiltro(string textoPesquisa)
        {
            if (textoPesquisa.Length > 0 && textoPesquisa.Length < 3)
                return new List<Ingrediente>();

            return ingreditenteBm.GetListaIngredientesByFiltro(textoPesquisa);

        }

        public void AlterarIngrediente(Ingrediente Ingrediente, TabelaNutricional pTabelaNutricioanl)
        {
            ingreditenteBm.Update(Ingrediente);
            AtualizarTAbelaNutricional(Ingrediente, pTabelaNutricioanl);
        }

        private void AtualizarTAbelaNutricional(Ingrediente ingrediente, TabelaNutricional pTabelaNutricional)
        {
            

            var tabelaAntiga = tabelaNutricionalBm.GetByIngrediente(ingrediente.IdIngrediente);

            if (tabelaAntiga != null)
                tabelaNutricionalBm.Delete(tabelaAntiga);
            
            pTabelaNutricional.Ingrediente = GetIngredienteById(ingrediente.IdIngrediente);
            tabelaNutricionalBm.Insert(pTabelaNutricional);
            
        }

        public void CarregarTabelaNutricional(string FileName)
        {


            var excelFile = new ExcelQueryFactory(FileName);
            var result = from a in excelFile.Worksheet("Tabela_TACO") select a;
            foreach (var r in result)
            {


                var ingrediente = ingreditenteBm.GetByCodigoTACO(r["CodigoTACO"]);

                if (ingrediente == null)
                {
                    ingrediente = new Ingrediente()
                    {
                        CodigoTACO = int.Parse(r["CodigoTACO"]),
                        NomeTACO = r["NomeTACO"],
                        Ativo = true
                    };

                    ingreditenteBm.Insert(ingrediente);
                }

                var tabelaNutricional = tabelaNutricionalBm.GetByIngrediente(ingrediente.IdIngrediente);

                if (tabelaNutricional == null)
                {
                    tabelaNutricional = new TabelaNutricional()
                    {
                        Ingrediente = ingrediente
                    };

                    tabelaNutricionalBm.Insert(tabelaNutricional);
                }

                tabelaNutricional.Umidade = TratarInformacaoTAbela(r["Umidade"]);
                tabelaNutricional.EnergiaKCAL = TratarInformacaoTAbela(r["EnergiaKcal"]);
                tabelaNutricional.EnergiaKJ = TratarInformacaoTAbela(r["EnergiaKJ"]);
                tabelaNutricional.Proteina = TratarInformacaoTAbela(r["Proteina"]);
                tabelaNutricional.Lipidio = TratarInformacaoTAbela(r["Lipideos"]);
                tabelaNutricional.Colesterol = TratarInformacaoTAbela(r["Colesterol"]);
                tabelaNutricional.Carbidrato = TratarInformacaoTAbela(r["Carboidrato"]);
                tabelaNutricional.FibraAlimentar = TratarInformacaoTAbela(r["FibrasAlimentares"]);
                tabelaNutricional.Cinzas = TratarInformacaoTAbela(r["Cinzas"]);
                tabelaNutricional.Calcio = TratarInformacaoTAbela(r["Calcio"]);
                tabelaNutricional.Magnesio = TratarInformacaoTAbela(r["Magnesio"]);
                tabelaNutricional.Manganes = TratarInformacaoTAbela(r["Manganes"]);
                tabelaNutricional.Fosforo = TratarInformacaoTAbela(r["Fosforo"]);
                tabelaNutricional.Ferro = TratarInformacaoTAbela(r["Ferro"]);
                tabelaNutricional.Sodio = TratarInformacaoTAbela(r["Sodio"]);
                tabelaNutricional.Potassio = TratarInformacaoTAbela(r["Potassio"]);
                tabelaNutricional.Cobre = TratarInformacaoTAbela(r["Cobre"]);
                tabelaNutricional.Zinco = TratarInformacaoTAbela(r["Zinco"]);
                tabelaNutricional.Retinol = TratarInformacaoTAbela(r["Retinol"]);
                tabelaNutricional.RE = TratarInformacaoTAbela(r["RE"]);
                tabelaNutricional.RAE = TratarInformacaoTAbela(r["REA "]);
                tabelaNutricional.Tiamina = TratarInformacaoTAbela(r["Tiamina"]);
                tabelaNutricional.Riboflavina = TratarInformacaoTAbela(r["Riboflavina"]);
                tabelaNutricional.Piridoxina = TratarInformacaoTAbela(r["Piridoxina"]);
                tabelaNutricional.Niacina = TratarInformacaoTAbela(r["Niacina"]);
                tabelaNutricional.VitaminaC = TratarInformacaoTAbela(r["VitaminaC"]);
                tabelaNutricionalBm.Update(tabelaNutricional);

            }



        }

        public CategoriaIngrediente GetCategoriaById(int idCategoriaIngrediente)
        {
            return categoriaIngredienteBm.GetByID(idCategoriaIngrediente);
        }

        public TabelaNutricional GetTabelaNutricionalByIdIngrediente(int idIngrediente)
        {
            return tabelaNutricionalBm.GetByIngrediente(idIngrediente);
        }

        public Ingrediente GetIngredienteById(int pIdIngrediente)
        {
            return ingreditenteBm.GetByID(pIdIngrediente);
        }

        private double TratarInformacaoTAbela(string Texto)
        {
            return Texto.ToUpper() == "NA" ? 0 : 
                   Texto.ToUpper() == "TR" ? 0 :
                   Texto           == "*"  ? 0 :
                   string.IsNullOrWhiteSpace(Texto) ? 0 : 
                   double.Parse(Texto);
        }

        public void Dispose()
        {
            ingreditenteBm.Dispose();
            tabelaNutricionalBm.Dispose();
            historicoDesativacaoReativacaoBm.Dispose();
            usuarioBm.Dispose();
            categoriaIngredienteBm.Dispose();
        }

        public Usuario GetUsuarioByLogin(string name)
        {
            return usuarioBm.GetByLogin(name);
        }

        public void DesativarIngrediente(Ingrediente pIngrediente, Usuario pUsuario, string Ip)
        {
            pIngrediente.Ativo = false;
            ingreditenteBm.Update(pIngrediente);
            RegistrarHistóricoDesabilitarHabilitar(pIngrediente.IdIngrediente, pUsuario.IdUsuario, Ip, TipoOpracaoDesativacaoIngrediente.Desativar);
        }

       
        public void ReativarIngrediente(Ingrediente pIngrediente, Usuario pUsuario, string Ip)
        {
            pIngrediente.Ativo = true;
            ingreditenteBm.Update(pIngrediente);
            RegistrarHistóricoDesabilitarHabilitar(pIngrediente.IdIngrediente, pUsuario.IdUsuario, Ip, TipoOpracaoDesativacaoIngrediente.Reativar);
        }

        private void RegistrarHistóricoDesabilitarHabilitar(int pIngrediente, int pUsuario, string Ip, TipoOpracaoDesativacaoIngrediente TipoOperacao)
        {
            var hist = new IngredienteHistoricoDesativacao()
            {
                DataHoraOperacao = DateTime.Now,
                Ingrediente = ingreditenteBm.GetByID(pIngrediente),
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
    }
}
