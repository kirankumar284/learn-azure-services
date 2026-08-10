using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeApp.Data;
using TradeApp.Dtos;
using TradeApp.Entity;

namespace TradeApp.Controllers;

[ApiController]
[Route("trades")]
public class TradesController : ControllerBase
{
    private readonly TradeDbContext _db;

    public TradesController(TradeDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrades()
    {
        var trades = await _db.Trades
            .Select(t => new TradeDto(
                t.Id,
                t.Commodity,
                t.Quantity,
                t.Price,
                t.BuySell,
                t.TradeDate,
                t.Counterparty))
            .ToListAsync();

        return Ok(trades);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetTrade(long id)
    {
        var trade = await _db.Trades
            .Where(t => t.Id == id)
            .Select(t => new TradeDto(
                t.Id,
                t.Commodity,
                t.Quantity,
                t.Price,
                t.BuySell,
                t.TradeDate,
                t.Counterparty))
            .FirstOrDefaultAsync();

        return trade is null
            ? NotFound()
            : Ok(trade);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrade(CreateTradeDto dto)
    {
        var trade = new Trade
        {
            Commodity = dto.Commodity,
            Quantity = dto.Quantity,
            Price = dto.Price,
            BuySell = dto.BuySell,
            TradeDate = dto.TradeDate,
            Counterparty = dto.Counterparty
        };

        _db.Trades.Add(trade);
        await _db.SaveChangesAsync();

        var result = new TradeDto(
            trade.Id,
            trade.Commodity,
            trade.Quantity,
            trade.Price,
            trade.BuySell,
            trade.TradeDate,
            trade.Counterparty);

        return CreatedAtAction(
            nameof(GetTrade),
            new { id = trade.Id },
            result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateTrade(
        long id,
        UpdateTradeDto dto)
    {
        var trade = await _db.Trades.FindAsync(id);

        if (trade is null)
        {
            return NotFound();
        }

        trade.Commodity = dto.Commodity;
        trade.Quantity = dto.Quantity;
        trade.Price = dto.Price;
        trade.BuySell = dto.BuySell;
        trade.TradeDate = dto.TradeDate;
        trade.Counterparty = dto.Counterparty;

        await _db.SaveChangesAsync();

        var result = new TradeDto(
            trade.Id,
            trade.Commodity,
            trade.Quantity,
            trade.Price,
            trade.BuySell,
            trade.TradeDate,
            trade.Counterparty);

        return Ok(result);
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> PatchTrade(
        long id,
        PatchTradeDto dto)
    {
        var trade = await _db.Trades.FindAsync(id);

        if (trade is null)
        {
            return NotFound();
        }

        if (dto.Commodity is not null)
            trade.Commodity = dto.Commodity;

        if (dto.Quantity is not null)
            trade.Quantity = dto.Quantity.Value;

        if (dto.Price is not null)
            trade.Price = dto.Price.Value;

        if (dto.BuySell is not null)
            trade.BuySell = dto.BuySell.Value;

        if (dto.TradeDate is not null)
            trade.TradeDate = dto.TradeDate.Value;

        if (dto.Counterparty is not null)
            trade.Counterparty = dto.Counterparty;

        await _db.SaveChangesAsync();

        var result = new TradeDto(
            trade.Id,
            trade.Commodity,
            trade.Quantity,
            trade.Price,
            trade.BuySell,
            trade.TradeDate,
            trade.Counterparty);

        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteTrade(long id)
    {
        var trade = await _db.Trades.FindAsync(id);

        if (trade is null)
        {
            return NotFound();
        }

        _db.Trades.Remove(trade);

        await _db.SaveChangesAsync();

        return NoContent();
    }

}