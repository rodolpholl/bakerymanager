

namespace BakeryManager.Entities
{
    public class Ingrediente
    {
        public virtual int IdIngrediente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string NomeTACO { get; set; }
        public virtual string Abreviatura { get; set; }
        public virtual int CodigoTACO { get; set; }
        public virtual bool Ativo { get; set; }
    }
}
