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
    public class CadastroFornecedor: BusinessProcessBase, IDisposable
    {
        private FornecedorBM fornecedorBm;
        private FornecedorContatoBM fornecedorContatoBm;
        private FornecedorQuestionarioConfigBM fornecedorQuestionarioConfigBm;
        private QuestionarioBM QuestionarioBm;
        private CategoriaIngredienteBM categoriaIngredienteBm;
        private IngredienteBM ingredienteBm;
        private CredenciamentoFornecedorIngredienteBM credenciamentoFornecedorIngredienteBm;
        private FornecedorUsuarioBM fornecedorUsuarioBm;
        private RegistroAcessoBM registroAcessoBm;
        private UsuarioBM usuarioBm;
        private PerfilBM perfilBm;
        private UsuarioPerfilBM usuarioPerfilBm;

        public CadastroFornecedor()
        {
            fornecedorBm = GetObject<FornecedorBM>();
            fornecedorContatoBm = GetObject<FornecedorContatoBM>();
            fornecedorQuestionarioConfigBm = GetObject<FornecedorQuestionarioConfigBM>();
            QuestionarioBm = GetObject<QuestionarioBM>();
            categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
            ingredienteBm = GetObject<IngredienteBM>();
            credenciamentoFornecedorIngredienteBm = GetObject<CredenciamentoFornecedorIngredienteBM>();
            fornecedorUsuarioBm = GetObject<FornecedorUsuarioBM>();
            registroAcessoBm = GetObject<RegistroAcessoBM>();
            usuarioBm = GetObject<UsuarioBM>();
            perfilBm = GetObject<PerfilBM>();
            usuarioPerfilBm = GetObject<UsuarioPerfilBM>();
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
            fornecedorUsuarioBm.Dispose();
            registroAcessoBm.Dispose();
            usuarioBm.Dispose();
            perfilBm.Dispose();
            usuarioPerfilBm.Dispose();
        }

        public IList<Perfil> GetPerfilFornecedor()
        {
            return perfilBm.GetPerfisFornecedor().Where(x => x.Ativo).ToList();
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

        public IList<FornecedorUsuario> GetUsuarioFornecedor(int idFornecedor)
        {
            if (idFornecedor == 0)
                return new List<FornecedorUsuario>();
            else
                return fornecedorUsuarioBm.GetFornecedorUsuarioByFornecedor(fornecedorBm.GetByID(idFornecedor));
        }

        public bool VerificaPossibilidadeEdicao(FornecedorUsuario pFornecedorUsuario)
        {
            return !registroAcessoBm.VerificaExistenciaAcessoComSucesso(pFornecedorUsuario.Usuario);
        }

        public void AtualizarUsuario(List<FornecedorUsuario> listaFornecedorUsuario, int idFornecedor)
        {
            var listAtual = fornecedorUsuarioBm.GetFornecedorUsuarioByFornecedor(fornecedorBm.GetByID(idFornecedor));

            var listaExclusao = listAtual.Where(x => !listAtual.Select(y => y.Usuario.IdUsuario).Contains(x.Usuario.IdUsuario)).ToList();

            foreach (var excluir in listaExclusao)
            {
                if (VerificaPossibilidadeEdicao(excluir))
                {
                    var idExcluir = excluir.Usuario.IdUsuario;
                    fornecedorUsuarioBm.Delete(excluir);
                    listAtual.Remove(listAtual.FirstOrDefault(x => x.Usuario.IdUsuario == idExcluir));
                }
            }

            foreach(var usuario in listaFornecedorUsuario)
            {
                
                var usuarioNovo = listAtual.FirstOrDefault(x => x.Usuario.IdUsuario == usuario.Usuario.IdUsuario) ?? new FornecedorUsuario()
                {
                   DataInclusão = DateTime.Now
                };

                
                usuarioNovo.Fornecedor = fornecedorBm.GetByID(usuario.Fornecedor.IdFornecedor);
                usuarioNovo.Usuario = usuarioBm.GetByID(usuario.Usuario.IdUsuario) ?? new Usuario()
                {
                    DataCriacao = DateTime.Now,
                    Ativo = true,
                    AutenticaSenhaDia = false,
                    
                };
                usuarioNovo.Usuario.Login = usuario.Usuario.Login;
                usuarioNovo.Usuario.Email = usuario.Usuario.Email;
                usuarioNovo.Usuario.Nome = usuario.Usuario.Nome;

                usuarioNovo.UtilizaEmailComunicacao = usuario.UtilizaEmailComunicacao;

                usuarioNovo.Perfil = perfilBm.GetByID(usuario.Perfil.IdPerfil);

                if (usuarioNovo.IdFornecedorUsuario == 0)
                {
                    fornecedorUsuarioBm.Insert(usuarioNovo);
                    usuarioPerfilBm.Insert(new UsuarioPerfil()
                    {
                        Ativo = true,
                        DataAssociacao = DateTime.Now,
                        Perfil = perfilBm.GetByID(usuario.Perfil.IdPerfil),
                        Usuario = usuarioBm.GetByID(usuario.Usuario.IdUsuario)
                    });

                }
                else
                {
                    fornecedorUsuarioBm.Update(usuarioNovo);
                    var perfilAtual = usuarioPerfilBm.GetPerfilByUsuario(usuarioBm.GetByID(usuario.Usuario.IdUsuario)).FirstOrDefault(x => x.Ativo);
                    if (perfilAtual != null)
                    {
                        if (perfilAtual.Perfil.IdPerfil != usuario.Perfil.IdPerfil)
                        {
                            perfilAtual.Ativo = false;
                            usuarioPerfilBm.Update(perfilAtual);
                        }
                    }
                    usuarioPerfilBm.Insert(new UsuarioPerfil()
                    {
                        Ativo = true,
                        DataAssociacao = DateTime.Now,
                        Perfil = perfilBm.GetByID(usuario.Perfil.IdPerfil),
                        Usuario = usuarioBm.GetByID(usuario.Usuario.IdUsuario)
                    });
                }
                
            }

                
        }
    }
}
