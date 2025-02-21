using Models;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    public int? BlindBoxId { get; set; }
    public BlindBox? BlindBox { get; set; }

    public int? PackageId { get; set; }
    public Package? Package { get; set; }

    public int Quantity { get; set; } // Dùng chung cho cả BlindBox & Package

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
     public virtual ApplicationUser? applicationUser { get; set; }
}
