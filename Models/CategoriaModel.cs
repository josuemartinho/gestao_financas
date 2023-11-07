using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace sistemaFinanceiro.Models
{
    public class CategoriaModel
    {
        [Key]
        public int CategoriaId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nome { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string Icone { get; set; } = "";
        [Column(TypeName = "nvarchar(10)")]
        public string Tipo { get; set; } = "Despesa";
        [NotMapped]
        public string? IconeComNome
        {
            get
            {
                return this.Icone + " " + this.Nome;
                
            }
            set
            { 
            }
        }
    }
}
