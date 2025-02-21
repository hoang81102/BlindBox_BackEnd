using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("WalletTransaction")]
    public class WalletTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletTransactionId { get; set; }

        [Required]
        public int WalletId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string TransactionType { get; set; }
        [Required]
        public string TransactionStatus { get; set; }
        [Required]
        public string TransactionDate { get; set; }
        [Required]
        public string TransactionBalance { get; set; }
        [Required]
        public int? OrderId { get; set; }

        public virtual Wallet? Wallet { get; set; }
        public virtual Order? Order { get; set; }
    }
}
