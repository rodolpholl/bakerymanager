using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BakeryManager.InfraEstrutura.Base.Teste;
using BakeryManager.Services;
using BakeryManager.Entities;

namespace BakeryManager.BusinessProcessTests
{
    [TestClass]
    public class CadastrarIngredientesTest : TestBase
    {
        [TestMethod]
        public void InserirIngrediente()
        {
            try {
                using (var cadIngediente = new CadastroIngredientes())
                {
                    var ingrediente = new Ingrediente()
                    {
                        Abreviatura = "Arr",
                        Nome = "Arroz",
                        CodigoTACO = 1,
                        NomeTACO = "Arroz"
                    };
                    cadIngediente.InserirIngrediente(ingrediente);

                    Assert.IsTrue(ingrediente.IdIngrediente > 0, "Erro ao cadastrar o Ingrediente. Verifique os dados informados.");

                }

            } catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CarregarTabelaNutricionalTest()
        {
            try
            {
                using (var cadIngediente = new CadastroIngredientes())
                {
                    
                    cadIngediente.CarregarTabelaNutricional("C:\\Projetos\\BakeryManager\\trunk\\Tabela_TACO_2011.xlsx");

                   
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

      
    }
}
