using BakeryManager.Entities;
using BakeryManager.Repositories;
using LinqToExcel;
using System;
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
                        NomeTACO = r["NomeTACO"]
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

                tabelaNutricional.Umidade = r["Umidade"];
                tabelaNutricional.EnergiaKCAL = r["EnergiaKcal"];
                tabelaNutricional.EnergiaKJ = r["EnergiaKJ"];
                tabelaNutricional.Proteina = r["Proteina"];
                tabelaNutricional.Lipidio = r["Lipideos"];
                tabelaNutricional.Colesterol = r["Colesterol"];
                tabelaNutricional.Carbidrato = r["Carboidrato"];
                tabelaNutricional.FibraAlimentar = r["FibrasAlimentares"];
                tabelaNutricional.Cinzas = r["Cinzas"];
                tabelaNutricional.Calcio = r["Calcio"];
                tabelaNutricional.Magnesio = r["Magnesio"];
                tabelaNutricional.Manganes = r["Manganes"];
                tabelaNutricional.Fosforo = r["Fosforo"];
                tabelaNutricional.Ferro = r["Ferro"];
                tabelaNutricional.Sodio = r["Sodio"];
                tabelaNutricional.Potassio = r["Potassio"];
                tabelaNutricional.Cobre = r["Cobre"];
                tabelaNutricional.Zinco = r["Zinco"];
                tabelaNutricional.Retinol = r["Retinol"];
                tabelaNutricional.RE = r["RE"];
                tabelaNutricional.RAE = r["REA "];
                tabelaNutricional.Tiamina = r["Tiamina"];
                tabelaNutricional.Riboflavina = r["Riboflavina"];
                tabelaNutricional.Piridoxina = r["Piridoxina"];
                tabelaNutricional.Niacina = r["Niacina"];
                tabelaNutricional.VitaminaC = r["VitaminaC"];
                tabelaNutricionalBm.Update(tabelaNutricional);

            }



        }

        public void Dispose()
        {
            ingreditenteBm.Dispose();
            tabelaNutricionalBm.Dispose();
        }
    }
}
