internal sealed class BettingService : IBettingService
{
    private readonly RouletteContext _context;
    public BettingService(RouletteContext context)
    {
        _context = context;
    }

    public void LoseBet(Session session, double ammount)
    {
        session.Balance -= ammount;
        _context.Update(session);
        _context.SaveChangesAsync();
    }

    public void WinBet(Session session, double ammount)
    {
        session.Balance += ammount;
        _context.Update(session);
        _context.SaveChangesAsync();
    }
}