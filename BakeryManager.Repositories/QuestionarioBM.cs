using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class QuestionarioBM : BusinessManagementBase<Questionario>
    {
        public IList<Questionario> GetQuestionariosAtivos()
        {
            var lista = Query().Where(x => x.Ativo).ToList();

            var listaExpurgo = lista.Where(x => x.UsaPrazoExpiracao && x.DataExpiracao.Value < DateTime.Now.Date).Select(x => x.IdQuestionario).ToList();

            return lista.Where(x => !listaExpurgo.Contains(x.IdQuestionario)).ToList();

        }
    }

}
