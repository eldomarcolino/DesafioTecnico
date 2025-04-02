using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaDeRecarga.Model
{
    [Table("Balance")]
    public class Balance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } = 0.00m;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User? User { get; set; }
    }

    public class AddBalanceRequest
    {
        public int IdUser { set; get; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
