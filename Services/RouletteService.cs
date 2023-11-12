using Microsoft.Extensions.DependencyInjection;

internal sealed class RouletteService : IRouletteService
{
    private readonly IRouletteWheelService _rouletteWheelService;
    private readonly IBettingService _bettingService;
    private readonly RouletteContext _context;
    private readonly IServiceProvider _serviceProvider;
    private int _betAmmount = 1;
    public RouletteService(IRouletteWheelService rouletteWheelService, RouletteContext rouletteContext, IBettingService bettingService, IServiceProvider serviceProvider)
    {
        _rouletteWheelService = rouletteWheelService;
        _context = rouletteContext;
        _bettingService = bettingService;
        _serviceProvider = serviceProvider;
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
            var isWinningNumber = CheckIfWinningNumber(rolledNumber);
            if (isWinningNumber)
            {
                _bettingService.WinBet(session, _betAmmount);
                LogSpin(rolledNumber, session, true);
                _betAmmount = 1;
            }
            else
            {
                _bettingService.LoseBet(session, _betAmmount);
                LogSpin(rolledNumber, session, false);
                _betAmmount *= 2;
            }
        }
        var navigationService = _serviceProvider.GetRequiredService<INavigationService>();
        navigationService.NavigateHome();
    }

    private bool CheckIfWinningNumber(int rolledNumber)
    {
        if (rolledNumber == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(rolledNumber);
            return false;
        }
        else if (RouletteNumbers.RedWinningNumbers.Contains(rolledNumber))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(rolledNumber);
            return false;
        }
        else if (RouletteNumbers.BlackWinningNumbers.Contains(rolledNumber))
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(rolledNumber);
            return true;
        }
        else
        {
            throw new Exception("Rolled number is not a valid roulette number");
        }
    }

    private void LogSpin(int rolledNumber, Session session, bool won)
    {
        var spin = new Spin
        {
            DrawNumber = rolledNumber,
            SessionId = session.SessionId,
            BetAmmount = _betAmmount,
            SpinDate = DateTime.Now,
            IsWin = won,
            Balance = session.Balance
        };
        _context.Spins.Add(spin);
        _context.SaveChanges();
    }
}