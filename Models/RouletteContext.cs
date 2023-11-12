using Microsoft.EntityFrameworkCore;

public class RouletteContext : DbContext
{
    public RouletteContext() { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=Roulette.db");
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Spin> Spins { get; set; }
}