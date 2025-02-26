using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CartDTO
    {
            public string UserId { get; set; }
            public int? BlindBoxId { get; set; }
            public int? PackageId { get; set; }
            public int Quantity { get; set; }
    }
}
