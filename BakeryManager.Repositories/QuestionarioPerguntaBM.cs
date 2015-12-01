using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class QuestionarioPerguntaBM : BusinessManagementBase<QuestionarioPergunta>
    {
        public IList<QuestionarioPergunta> GetByQuestionario(Questionario pQuestionario)
        {
            return Query().Where(x => x.Questionario.IdQuestionario == pQuestionario.IdQuestionario).ToList();
        }
    }
}
