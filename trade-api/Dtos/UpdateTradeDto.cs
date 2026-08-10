using System.ComponentModel.DataAnnotations;
using TradeApp.Entity;

namespace TradeApp.Dtos;

// Used by PUT /trades/{id}
public record UpdateTradeDto(
    [Required]
    [StringLength(50)]
    string Commodity,

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    decimal Quantity,

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    decimal Price,

    [Required]
    TradeSide BuySell,

    [Required]
    DateTime TradeDate,

    [Required]
    [StringLength(100)]
    string Counterparty
);