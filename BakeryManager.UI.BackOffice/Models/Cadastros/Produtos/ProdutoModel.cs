﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Produtos
{
    public class ProdutoModel
    {
        public  int IdProduto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [StringLength(14,ErrorMessage = "Código GTIN não pode conter 13 ou 14 caracteres",MinimumLength = 13)]
        public string GTIN { get; set; } //Código de Barras

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name ="Categoria")]
        public CategoriaProdutoModel Categoria { get; set; }

        public bool PossuiTabelaNutricional { get; set; }

        [Display(Name = "Dias para contagem da Validade")]
        public int DiasPrazoValidade { get; set; }

        [Display(Name = "Porção para tabela nutricional (g)", Prompt = "Proção utilizada para cáculo da tabela nutricional (em gramas)")]
        public int ProporcaoTabelaNutricional { get; set; }

        [Display(Name = "Preço de Custo")]
        public double PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        public double PrecoVenda { get; set; }
    }
}
