public interface IBettingService
{
    void LoseBet(Session session, double amount);
    void WinBet(Session session, double amount);
}