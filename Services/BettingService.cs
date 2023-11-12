internal sealed class BettingService : IBettingService
{
    private readonly RouletteContext _context;
    public BettingService(RouletteContext context)
    {
        _context = context;
    }

    public void LoseBet(Session session, double amount)
    {
        session.Balance -= amount;
        _context.Update(session);
        _context.SaveChanges();
    }
    
    public void WinBet(Session session, double amount)
    {
        session.Balance += amount * 2;
        _context.Update(session);
        _context.SaveChanges();
    }
}