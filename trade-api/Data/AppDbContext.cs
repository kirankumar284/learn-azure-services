using Microsoft.EntityFrameworkCore;
using TradeApp.Entity;

namespace TradeApp.Data;

public class TradeDbContext : DbContext
{
    public TradeDbContext(DbContextOptions<TradeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trade> Trades => Set<Trade>();
}