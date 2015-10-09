using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Infraestrutura.Helpers;
using BakeryManager.Infraestrutura.Helpers.Security;
using NHibernate;
using NHibernate.Event;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Config.Auditory
{
    internal class AuditDeleteEventListener : AuditEventListenerBase, IPostDeleteEventListener
    {
        public AuditDeleteEventListener()
        {
            Console.Write("Log Delete Ativado");
        }
        public void OnPostDelete(PostDeleteEvent e)
        {
            if (Params.SkipTrakingList.Contains(e.Entity.GetType().FullName)
                || e.Entity.GetType().Name.Equals("AuditRegister"))
                return;


            var entityFullName = e.Entity.GetType().FullName;

            var aud = new AuditRegister()
            {
                ContextId = long.Parse(e.Id.ToString()),
                OperationDate = DateTime.Now,
                OperationType = AuditOperationType.Delete,
                ObjectName = AnnotationsAttributes.GetClassTitle(e.Entity.GetType()),
                ContextUser = GetUsuarioLogado(),
            };

            e.Session.GetSession(EntityMode.Poco).Save(aud);

           
        }

    }
}
