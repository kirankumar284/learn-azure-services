using System.ComponentModel.DataAnnotations;

namespace TradeApp.Entity;

public class Trade
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Commodity { get; set; } = string.Empty;

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Quantity { get; set; }

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    [Required]
    public TradeSide BuySell { get; set; }

    [Required]
    public DateTime TradeDate { get; set; }

    [Required]
    [StringLength(100)]
    public string Counterparty { get; set; } = string.Empty;

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdatedTime { get; set; } = DateTime.UtcNow;
}
