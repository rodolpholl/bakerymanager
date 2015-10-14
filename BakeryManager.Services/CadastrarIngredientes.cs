using BakeryManager.Entities;
using BakeryManager.Repositories;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BakeryManager.Services
{
    public class CadastrarIngredientes : BusinessProcessBase, IDisposable
    {
        private IngredienteBM ingreditenteBm;
        private TabelaNutricionalBM tabelaNutricionalBm;

        public CadastrarIngredientes()
        {
            ingreditenteBm = base.GetObject<IngredienteBM>();
            tabelaNutricionalBm = base.GetObject<TabelaNutricionalBM>();
        }

        public void InserirIngrediente(Ingrediente Ingrediente)
        {
            ingreditenteBm.Insert(Ingrediente);
        }

        public IList<Ingrediente> GetListaIngredientes()
        {
            return ingreditenteBm.GetAll();
        }

        public IList<Ingrediente> GetListaIngredientesByFiltro(string textoPesquisa)
        {
            if (textoPesquisa.Length > 0 && textoPesquisa.Length < 3)
                return new List<Ingrediente>();

            return ingreditenteBm.GetListaIngredientesByFiltro(textoPesquisa);

        }

        public void AlterarIngrediente(Ingrediente Ingrediente)
        {
            ingreditenteBm.Update(Ingrediente);
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
        }
    }
}
