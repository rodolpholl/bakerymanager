using System;
using System.ComponentModel.DataAnnotations;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config.Auditory
{
    public class AuditRegister
    {

        public virtual long Id { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual AuditOperationType OperationType { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual DateTime OperationDate { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual string ContextUser { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual long ContextId { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual string ColumnName { get; set; }

        [Required(ErrorMessage = "* Required field", AllowEmptyStrings = false)]
        public virtual string ObjectName { get; set; }

        public virtual string ColumnTitle { get; set; }

        public virtual string OldValue { get; set; }

        public virtual string NewValue { get; set; }

        
    }

    public enum AuditOperationType
    {
        Insert,
        Update,
        Delete
    }

    
}
