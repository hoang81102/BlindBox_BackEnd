using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BlindBoxImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlindBoxImageId { get; set; }

        [Required]
        public int BlindBoxId { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        public virtual BlindBox? BlindBox { get; set; }
    }
}
