class RouletteProgram : IRouletteProgram
{
    private readonly IRouletteService _rouletteService;
    private readonly RouletteContext _rouletteContext;
    public RouletteProgram(
        IRouletteService rouletteService,
        RouletteContext rouletteContext
        )
    {
        _rouletteService = rouletteService;
        _rouletteContext = rouletteContext;
    }

    public void Run()
    {
        _rouletteContext.Database.EnsureCreated();
        Console.WriteLine("Welcome to the Roulette Table!");
        Console.WriteLine("Please enter your Initial Balance: ");
        double initialBalance;
        double.TryParse(Console.ReadLine(), out initialBalance);
        var session = new Session
        {
            Balance = initialBalance,
        };
        _rouletteContext.Sessions.Add(session);
        _rouletteContext.SaveChanges();
        _rouletteService.Start(session);
    }
}