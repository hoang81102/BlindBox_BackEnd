using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetailId { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int BlindBoxId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual BlindBox? BlindBox { get; set; }

        public virtual Package? Package { get; set; }
    }
}
