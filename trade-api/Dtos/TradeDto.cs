namespace TradeApp.Dtos;

using TradeApp.Entity;

// Returned by GET endpoints
public record TradeDto(
    long Id,
    string Commodity,
    decimal Quantity,
    decimal Price,
    TradeSide BuySell,
    DateTime TradeDate,
    string Counterparty
);