using System.ComponentModel.DataAnnotations;
using TradeApp.Entity;

namespace TradeApp.Dtos;

// Used by PATCH /trades/{id}
// All properties are nullable because only the fields being updated are sent.
public record PatchTradeDto(
    [StringLength(50)]
    string? Commodity,

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    decimal? Quantity,

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    decimal? Price,

    TradeSide? BuySell,

    DateTime? TradeDate,

    [StringLength(100)]
    string? Counterparty
);