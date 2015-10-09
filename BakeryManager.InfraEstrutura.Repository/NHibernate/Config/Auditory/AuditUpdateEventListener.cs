using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BakeryManager.Infraestrutura.Helpers;
using BakeryManager.Infraestrutura.Helpers.Security;
using NHibernate;
using NHibernate.Event;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Config.Auditory
{
    internal class AuditUpdateEventListener : AuditEventListenerBase, IPostUpdateEventListener
    {


        public AuditUpdateEventListener()
        {
            Console.Write("Log Update Ativado");
        }

        public void OnPostUpdate(PostUpdateEvent e)
        {
            if (Params.SkipTrakingList.Contains(e.Entity.GetType().FullName)
                || e.Entity.GetType().Name.Equals("AuditRegister"))
                return;


      

            if (e.OldState == null)
            {
                //throw new ArgumentNullException("No old state available for entity type '" + entityFullName +
                //                                "'. Make sure you're loading it into Session before modifying and saving it.");
                return;
            }

            var dirtyFieldIndexes = e.Persister.FindDirty(e.State, e.OldState, e.Entity, e.Session);

            var session = e.Session.GetSession(EntityMode.Poco);

            foreach (var dirtyFieldIndex in dirtyFieldIndexes)
            {
                var oldValue = getStringValueFromStateArray(e.OldState, dirtyFieldIndex);
                var newValue = getStringValueFromStateArray(e.State, dirtyFieldIndex);

                if (oldValue == newValue)
                {
                    continue;
                }

                var aud = new AuditRegister()
                {
                    ColumnName = e.Persister.PropertyNames[dirtyFieldIndex],
                    ContextId = long.Parse(e.Id.ToString()),
                    NewValue = newValue.ToString(),
                    OldValue = oldValue == null ? string.Empty : oldValue.ToString(),
                    OperationDate = DateTime.Now,
                    OperationType = AuditOperationType.Update,
                    ColumnTitle = AnnotationsAttributes.GetPropertyTitle(e.Entity.GetType(), e.Persister.PropertyNames[dirtyFieldIndex]),
                    ContextUser = GetUsuarioLogado(),
                    ObjectName = AnnotationsAttributes.GetClassTitle(e.Entity.GetType())
                };

                session.Save(aud);
            }

            session.Flush();

            return;
        }

       
    }
}
