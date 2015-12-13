using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class AvaliacaoFornecedor : BusinessProcessBase, IDisposable
    {

        private FornecedorBM fornecedorBm;
        private FornecedorQuestionarioConfigBM configBm;
        private QuestionarioBM questionarioBm;
        private QuestionarioPerguntaBM questionarioPerguntaBm;
        private FornecedorAvaliacaoQuestionarioBM fornecedorAvaliacaoQuestionarioBm;
        private FornecedorAvaliacaoQuestionarioRespostaBM fornecedorAvaliacaoQuestionarioRespostaBm;
        private FornecedorAvaliacaoBM fornecedorAvaliacaoBm;

        public IList<Fornecedor> GetListaFornecedorComQuestionarioAssociado()
        {
            var fornAtivos = fornecedorBm.getFornecedoresAtivos().Select(x => x.IdFornecedor).ToList();

            return configBm.GetAll().Where(x => fornAtivos.Contains(x.Fornecedor.IdFornecedor)).Select(x => x.Fornecedor).Distinct().ToList();

        }

        public AvaliacaoFornecedor()
        {
            fornecedorBm = GetObject<FornecedorBM>();
            configBm = GetObject<FornecedorQuestionarioConfigBM>();
            questionarioBm = GetObject<QuestionarioBM>();
            questionarioPerguntaBm = GetObject<QuestionarioPerguntaBM>();
            fornecedorAvaliacaoQuestionarioBm = GetObject<FornecedorAvaliacaoQuestionarioBM>();
            fornecedorAvaliacaoQuestionarioRespostaBm = GetObject<FornecedorAvaliacaoQuestionarioRespostaBM>();
            fornecedorAvaliacaoBm = GetObject<FornecedorAvaliacaoBM>();
        }

        public IList<FornecedorAvaliacao> GetListaAvaliacaoByFornecedor(int idFornecedor)
        {
            return fornecedorAvaliacaoBm.GetListaAvaliacaoByFornecedor(fornecedorBm.GetByID(idFornecedor));
        }

        public bool HabilitaEdicaoAvaliacao(int IdFornecedoraAvaliacao)
        {
            var listaQuestionario = fornecedorAvaliacaoQuestionarioBm.getFornecedorQuestionarioByAvaliacao(fornecedorAvaliacaoBm.GetByID(IdFornecedoraAvaliacao));

            return listaQuestionario.Any(x => x.Questionario.Ativo &&
                                               (!x.Questionario.UsaPrazoExpiracao || (x.Questionario.UsaPrazoExpiracao && x.Questionario.DataExpiracao.Value.Date > DateTime.Now.Date)));


        }

        public IList<FornecedorAvaliacaoQuestionarioResposta> GetQuestionarioFornecedor(int idFornecedor)
        {
            var listaQuestionarioFornecedor = fornecedorAvaliacaoQuestionarioRespostaBm.GetForneceodrQuestionarioByFornecedor(fornecedorBm.GetByID(idFornecedor)).ToList();

            //Retornar apenas os questionarios que estejam ativos e que não usam prazo de expiração. Se usarem, a data de expiração deve ser maior que a data do dia.
            var listaQuestionario = configBm.GetForneceodrQuestionarioByFornecedor(fornecedorBm.GetByID(idFornecedor))
                                    .Where(x => x.Questionario.Ativo && 
                                               (!x.Questionario.UsaPrazoExpiracao || (x.Questionario.UsaPrazoExpiracao && x.Questionario.DataExpiracao.Value.Date > DateTime.Now.Date)))
                                    .Select(x => x.Questionario.IdQuestionario).ToList();

            if (listaQuestionarioFornecedor == null || listaQuestionarioFornecedor.Count == 0)
                return questionarioPerguntaBm.GetAll().Where(x => listaQuestionario.Contains(x.Questionario.IdQuestionario))
                    .Select(x => new FornecedorAvaliacaoQuestionarioResposta()
                {
                    Pergunta = x
                }).ToList();
            else
                
                return questionarioPerguntaBm.GetAll().Where(x => listaQuestionario.Contains(x.Questionario.IdQuestionario))
                    .Select(x => new FornecedorAvaliacaoQuestionarioResposta()
                    {
                        Avaliacao = listaQuestionarioFornecedor.FirstOrDefault(y => y.Pergunta.IdQuestionarioPergunta == x.IdQuestionarioPergunta).Avaliacao,
                        Pergunta = x,
                        Verdadeiro = listaQuestionarioFornecedor.FirstOrDefault(y => y.Pergunta.IdQuestionarioPergunta == x.IdQuestionarioPergunta).Verdadeiro,
                        FornecedorQuestionario = listaQuestionarioFornecedor.FirstOrDefault(y => y.Pergunta.IdQuestionarioPergunta == x.IdQuestionarioPergunta).FornecedorQuestionario

                    })
                    .ToList();


        }

        public FornecedorAvaliacao getAvaliacaoById(int idFornecedorAvaliacao)
        {
            return fornecedorAvaliacaoBm.GetByID(idFornecedorAvaliacao);
        }

        public void Dispose()
        {
            fornecedorBm.Dispose();
            questionarioBm.Dispose();
            configBm.Dispose();
            questionarioPerguntaBm.Dispose();
            fornecedorAvaliacaoQuestionarioRespostaBm.Dispose();
            fornecedorAvaliacaoQuestionarioBm.Dispose();
            fornecedorAvaliacaoBm.Dispose();
        }
    }
}
