using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaFinanceiro.Models
{
    public class TransacaoModel
    {
        [Key]
        public int TransacaoId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, escolha a Categoria")]
        public int CategoriaId { get; set; }
        public CategoriaModel Categoria { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valor deve ser maior que ZERO")]
        public int Valor { get; set; }

        [Column(TypeName = "nvarchar(75)")]

        public string? Descricao { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        [NotMapped]
        public string? FormatadoValor
        {
            get
            {
                return ((Categoria == null || Categoria.Tipo == "Despesa") ? "- " : "+ ") + Valor.ToString("C0");

            }

        }
    }
}
