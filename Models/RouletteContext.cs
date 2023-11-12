using Microsoft.EntityFrameworkCore;

public class RouletteContext : DbContext
{
    public RouletteContext(DbContextOptions<RouletteContext> options) : base(options) { }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Spin> Spins { get; set; }
}