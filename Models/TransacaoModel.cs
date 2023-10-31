using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistemaFinanceiro.Models
{
    public class TransacaoModel
    {
        [Key]
        public int TransacaoId { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaModel Categoria { get; set; }
        public int Valor { get; set; }
        [Column(TypeName="nvarchar(75)")]
        public string? Descricao { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
