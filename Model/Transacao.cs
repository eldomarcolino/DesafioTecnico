using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaDeRecarga.Model
{
    [Table("Transaction")]
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdUser")]
        public int IdUser { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required]
        public string Type { get; set; } // "Recarga" , "Débido" ou "Transferência"

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public User? Destinatario { get; set; }
    }

    public class TransferenciaRequest
    {
        public int IdRemetente { get; set; }
        public int IdDestinatario { get; set; }
        public decimal Valor { get; set; }
        public string Description { get; set; }
    }
}
