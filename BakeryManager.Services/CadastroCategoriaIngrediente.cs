using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroCategoriaIngrediente : BusinessProcessBase, IDisposable
    {
        private CategoriaIngredienteBM categoriaIngredieteBm;

        public CadastroCategoriaIngrediente()
        {
            categoriaIngredieteBm = GetObject<CategoriaIngredienteBM>();
        }

        public void InserirCategoriaIngrediente(CategoriaIngrediente Categoria)
        {
            categoriaIngredieteBm.Insert(Categoria);
        }

        public void AlterarCategoriaIngrediente(CategoriaIngrediente Categoria)
        {
            categoriaIngredieteBm.Update(Categoria);
        }

        public void ExcluirCategoriaIngrediente(int IdCategoria)
        {
            categoriaIngredieteBm.Delete(categoriaIngredieteBm.GetByID(IdCategoria));
        }

        public IList<CategoriaIngrediente> GetCategoriaIngredienteAll()
        {
            return categoriaIngredieteBm.GetAll();
        }

        public bool VerificaDependenciaCategoriaIngrediente(int idCategoriaIngrediente)
        {
            return categoriaIngredieteBm.Query().Any(x => x.IdCategoriaIngrediente == idCategoriaIngrediente);
        }

        public void Dispose()
        {
            categoriaIngredieteBm.Dispose();
        }

        public CategoriaIngrediente GetCategoriaIngredienteById(int idCategoriaIngrediente)
        {
            return categoriaIngredieteBm.GetByID(idCategoriaIngrediente);
        }
    }
}
