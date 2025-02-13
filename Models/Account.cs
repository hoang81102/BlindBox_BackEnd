

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("Accounts")] 

    
    public class Account 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }


        [StringLength(50)]
        public string Role { get; set; }


        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsVerify { get; set; } = false;

        public virtual ICollection<Address>? Address { get; set; }
    }
}
