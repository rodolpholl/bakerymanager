using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroFornecedor: BusinessProcessBase, IDisposable
    {
        private FornecedorBM fornecedorBm;
        private FornecedorContatoBM fornecedorContatoBm;
        private FornecedorQuestionarioConfigBM fornecedorQuestionarioConfigBm;
        private QuestionarioBM QuestionarioBm;
        private CategoriaIngredienteBM categoriaIngredienteBm;
        private IngredienteBM ingredienteBm;
        private CredenciamentoFornecedorIngredienteBM credenciamentoFornecedorIngredienteBm;

        public CadastroFornecedor()
        {
            fornecedorBm = GetObject<FornecedorBM>();
            fornecedorContatoBm = GetObject<FornecedorContatoBM>();
            fornecedorQuestionarioConfigBm = GetObject<FornecedorQuestionarioConfigBM>();
            QuestionarioBm = GetObject<QuestionarioBM>();
            categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
            ingredienteBm = GetObject<IngredienteBM>();
            credenciamentoFornecedorIngredienteBm = GetObject<CredenciamentoFornecedorIngredienteBM>();
        }

        public void Dispose()
        {
            fornecedorBm.Dispose();
            fornecedorContatoBm.Dispose();
            fornecedorQuestionarioConfigBm.Dispose();
            QuestionarioBm.Dispose();
            categoriaIngredienteBm.Dispose();
            ingredienteBm.Dispose();
            credenciamentoFornecedorIngredienteBm.Dispose();
        }

        public IList<Fornecedor> GetFornecedores()
        {
            return fornecedorBm.GetAll();
        }

        public void InserirFornecedor(Fornecedor Fornecedor)
        {
            Fornecedor.Ativo = true;
            fornecedorBm.Insert(Fornecedor);
        }

        public Fornecedor GetFornecedorById(int IdFornecedor)
        {
            return fornecedorBm.GetByID(IdFornecedor);
        }

        public void AlterarFornecedor(Fornecedor Fornecedor)
        {
            fornecedorBm.Update(Fornecedor);
        }

        public void DesativarFornecedor(Fornecedor Fornecedor)
        {
            Fornecedor.Ativo = false;
            AlterarFornecedor(Fornecedor);
        }

        public void ReativarFornecedor(Fornecedor Fornecedor)
        {
            Fornecedor.Ativo = true;
            AlterarFornecedor(Fornecedor);
        }

        public IList<FornecedorContato> GetContatosFornecedor(int idFornecedor)
        {
            var retorno = fornecedorContatoBm.GetByFornecedor(fornecedorBm.GetByID(idFornecedor));
            return retorno ?? new List<FornecedorContato>();

        }

        public void AtualizarContato(IList<FornecedorContato> ListaContato, int idFornecedor)
        {


            var listaAtual = fornecedorContatoBm.GetByFornecedor(fornecedorBm.GetByID(idFornecedor));

            foreach (var contatoAtual in listaAtual)
                fornecedorContatoBm.Delete(contatoAtual);


            foreach(var contato in ListaContato)
            {
                contato.Fornecedor = fornecedorBm.GetByID(idFornecedor);
                fornecedorContatoBm.Insert(contato);
            }
        }

        public IList<FornecedorQuestionarioConfig> GetQuestionarioFornecedor(Fornecedor fornecedor)
        {
            if (fornecedor == null)
                return new List<FornecedorQuestionarioConfig>();
            else
                return fornecedorQuestionarioConfigBm.GetForneceodrQuestionarioByFornecedor(fornecedor);
            
        }

        public IList<Questionario> GetQuestionarioAtivo()
        {
            return QuestionarioBm.GetQuestionariosAtivos();
        }

        public Questionario GetQuestionarioById(int idQuestionario)
        {
            return QuestionarioBm.GetByID(idQuestionario);
        }

        public void AtualizarQuestionario(List<FornecedorQuestionarioConfig> ListaQuestionario, int idFornecedor)
        {
            var listaAtual = fornecedorQuestionarioConfigBm.GetForneceodrQuestionarioByFornecedor(fornecedorBm.GetByID(idFornecedor));

            foreach (var questionarioAtual in listaAtual)
                fornecedorQuestionarioConfigBm.Delete(questionarioAtual);


            foreach (var questionario in ListaQuestionario)
            {
                questionario.Fornecedor = fornecedorBm.GetByID(idFornecedor);
                fornecedorQuestionarioConfigBm.Insert(questionario);
            }
        }

        public IList<CategoriaIngrediente> GetCategoriaIngredientes()
        {
            return categoriaIngredienteBm.GetAll();
        }

        public IList<Ingrediente> GetCategoriaIngredientesByCategoria(int IdCategoria)
        {
            if (IdCategoria == 0)
                return ingredienteBm.GetAll();
            else
                return ingredienteBm.GetByCategoria(categoriaIngredienteBm.GetByID(IdCategoria));
        }

        public IList<CredenciamentoFornecedorIngrediente> GetCredenciamentoByFornecedor(int idFornecedor)
        {
            if (idFornecedor == 0)
                return new List<CredenciamentoFornecedorIngrediente>();
            else
                return credenciamentoFornecedorIngredienteBm.GetCredenciamentoByFornecedor(fornecedorBm.GetByID(idFornecedor));
        }

        public void AtualizarCredenciamento(List<CredenciamentoFornecedorIngrediente> listaCredenciamento, int idFornecedor)
        {
            var listaAtual = credenciamentoFornecedorIngredienteBm.GetCredenciamentoByFornecedor(fornecedorBm.GetByID(idFornecedor));

            foreach (var atual in listaAtual)
                credenciamentoFornecedorIngredienteBm.Delete(atual);

            foreach(var credenciamento in listaCredenciamento)
            {
                credenciamento.Ingrediente = ingredienteBm.GetByID(credenciamento.Ingrediente.IdIngrediente);
                credenciamento.Fornecedor = fornecedorBm.GetByID(credenciamento.Fornecedor.IdFornecedor);
                credenciamentoFornecedorIngredienteBm.Insert(credenciamento);
            }
        }
    }
}
