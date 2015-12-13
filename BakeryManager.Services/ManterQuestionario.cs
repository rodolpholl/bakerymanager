using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessProcess;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ManterQuestionario : BusinessProcessBase, IDisposable
    {
        private QuestionarioBM questionarioBm;
        private QuestionarioPerguntaBM questionarioPerguntaBm;


        public ManterQuestionario()
        {
            questionarioBm = GetObject<QuestionarioBM>();
            questionarioPerguntaBm = GetObject<QuestionarioPerguntaBM>();
        }

        public void Dispose()
        {
            questionarioBm.Dispose();
            questionarioPerguntaBm.Dispose();
        }

        public IList<Questionario> GetListaQuestionario()
        {
            return questionarioBm.GetAll();
        }

        public Questionario GetQuestionarioById(int IdQuestionario)
        {
            return questionarioBm.GetByID(IdQuestionario);
        }

        public IList<QuestionarioPergunta> GetPerguntasQuestionario(Questionario pQuestionario)
        {
            if (pQuestionario == null)
                return new List<QuestionarioPergunta>();
            else
                return questionarioPerguntaBm.GetByQuestionario(pQuestionario);
        }

        public void AtualizarContatos(IEnumerable<QuestionarioPergunta> ListaImportante, int IdQuestionario)
        {
            var listaAtual = questionarioPerguntaBm.GetByQuestionario(questionarioBm.GetByID(IdQuestionario));

            foreach (var respostaAtual in listaAtual)
                questionarioPerguntaBm.Delete(respostaAtual);


            foreach (var Resposta in ListaImportante)
            {
                Resposta.Questionario = questionarioBm.GetByID(IdQuestionario);
                questionarioPerguntaBm.Insert(Resposta);
            }
        }

        public void InserirQuestionario(Questionario questionario)
        {
            AtualizarPrazo(questionario);
            
            questionarioBm.Insert(questionario);
        }

        private void AtualizarPrazo(Questionario questionario)
        {
            if (questionario.UsaPrazoExpiracao && !questionario.DataExpiracao.HasValue)
            {
                
                if (questionario.PrazoExpiracao <= 0)
                    throw new BusinessProcessException("O prazo deve ser maior ou igual a que 1 dia");

                questionario.DataExpiracao = DateTime.Now.Date.AddDays(questionario.PrazoExpiracao);

            }

            if (!questionario.UsaPrazoExpiracao)
            {
                questionario.PrazoExpiracao = 0;
                questionario.DataExpiracao = null;
            }
           
            
                
        }

        public void AlterarQuestionario(Questionario questionario)
        {
            AtualizarPrazo(questionario);
            questionarioBm.Update(questionario);
        }

        public void Desativar(Questionario questionario)
        {
            questionario.Ativo = false;
            questionarioBm.Update(questionario);
        }

        public void Reativar(Questionario questionario)
        {
            questionario.Ativo = true;
            questionarioBm.Update(questionario);
        }
    }
}
