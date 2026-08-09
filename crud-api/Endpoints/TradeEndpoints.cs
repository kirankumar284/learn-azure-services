using TradeApp.Dtos;

namespace TradeApp.Endpoints;

// Extension Methods
public static class TradeEndpoints
{
    static long id = 1;
    private static readonly List<TradeDto> trades =
    [
        new TradeDto(id++, "Gold", 8m, 9m, TradeSide.Buy, DateTime.UtcNow, "ABC Ltd"),
        new TradeDto(id++, "Silver", 12m, 25.5m, TradeSide.Sell, DateTime.UtcNow, "XYZ Ltd"),
        new TradeDto(id++, "Copper", 5m, 100m, TradeSide.Buy, DateTime.UtcNow, "DEF Ltd")
    ];

    public static RouteGroupBuilder MapTradesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("trades");

        group.MapGet("/", () => Results.Ok(trades));

        group.MapGet("/{id:long}", (long id) =>
        {
            TradeDto? trade = trades.FirstOrDefault(t => t.Id == id);
            return trade is null ? Results.NotFound() : Results.Ok(trade);
        });

        group.MapPost("/", (CreateTradeDto trade) =>
        {
            TradeDto newTrade = new TradeDto(id++, trade.Commodity, trade.Quantity, trade.Price, trade.BuySell, trade.TradeDate, trade.Counterparty);
            trades.Add(newTrade);
            return Results.Created($"/trades/{newTrade.Id}", newTrade);
        });

        group.MapPut("/{id:long}", (long id, UpdateTradeDto trade) =>
        {
            int index = trades.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            TradeDto updatedTrade = new TradeDto(id, trade.Commodity, trade.Quantity, trade.Price, trade.BuySell, trade.TradeDate, trade.Counterparty);
            trades[index] = updatedTrade;
            return Results.Ok(updatedTrade);
        });

        group.MapPatch("/{id:long}", (long id, PatchTradeDto trade) =>
        {
            int index = trades.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            TradeDto targetTrade = trades[index];

            // update trade with what values received
            TradeDto updatedTrade = new TradeDto(id, trade.Commodity ?? targetTrade.Commodity, trade.Quantity ?? targetTrade.Quantity, trade.Price ?? targetTrade.Price, trade.BuySell ?? targetTrade.BuySell, trade.TradeDate ?? targetTrade.TradeDate, trade.Counterparty ?? targetTrade.Counterparty);

            trades[index] = updatedTrade;
            return Results.Ok(updatedTrade);
        });

        group.MapDelete("/{id:long}", (long id) =>
        {
            int index = trades.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            trades.RemoveAt(index);
            return Results.NoContent();
        });

        return group;
    }

}