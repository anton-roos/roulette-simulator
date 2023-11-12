internal sealed class RouletteService : IRouletteService
{
    private readonly IRouletteWheelService _rouletteWheelService;
    private readonly IBettingService _bettingService;
    private readonly RouletteContext _context;
    private int _betAmmount = 1;
    public RouletteService(IRouletteWheelService rouletteWheelService, RouletteContext rouletteContext, IBettingService bettingService)
    {
        _rouletteWheelService = rouletteWheelService;
        _context = rouletteContext;
        _bettingService = bettingService;
    }

    public void Start(Session session)
    {
        for (int i = 0; i < 10000; i++)
        {
            if (session.Balance <= _betAmmount)
            {
                break;
            }
            var rolledNumber = _rouletteWheelService.Spin();
            ReportRolledNumber(rolledNumber, session);
        }
    }

    private void ReportRolledNumber(int rolledNumber, Session session)
    {
        if (rolledNumber == 0)
        {
            var spin = new Spin
            {
                DrawNumber = rolledNumber,
                SessionId = session.SessionId,
                BetAmmount = _betAmmount,
                SpinDate = DateTime.Now,
                IsWin = false,
                Balance = session.Balance - _betAmmount
            };
            _context.Spins.Add(spin);
            _context.SaveChanges();

            _bettingService.LoseBet(session, _betAmmount);

            _betAmmount *= 2;
        }
        else if (RouletteNumbers.RedWinningNumbers.Contains(rolledNumber))
        {
            var spin = new Spin
            {
                DrawNumber = rolledNumber,
                SessionId = session.SessionId,
                BetAmmount = _betAmmount,
                SpinDate = DateTime.Now,
                IsWin = false,
                Balance = session.Balance - _betAmmount
            };
            _context.Spins.Add(spin);
            _context.SaveChanges();

            _bettingService.LoseBet(session, _betAmmount);

            _betAmmount *= 2;

        }
        else if (RouletteNumbers.BlackWinningNumbers.Contains(rolledNumber))
        {
            var winningAmmount =  _betAmmount * 2;
            var balanceIncrease = session.Balance + winningAmmount;
            var spin = new Spin
            {
                DrawNumber = rolledNumber,
                SessionId = session.SessionId,
                BetAmmount = _betAmmount,
                SpinDate = DateTime.Now,
                IsWin = true,
                Balance = balanceIncrease
            };
            _context.Spins.Add(spin);
            _context.SaveChanges();

            _bettingService.WinBet(session, _betAmmount);

            _betAmmount = 1;
            
        }
        else
        {
            throw new Exception("Rolled number is not a valid roulette number");
        }
    }
}