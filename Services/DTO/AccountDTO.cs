using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public decimal WalletBalance { get; set; }
    }
}
