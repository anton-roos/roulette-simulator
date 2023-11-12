internal sealed class NavigationService : INavigationService
{
    private readonly IRouletteProgram _rouletteProgram;
    public NavigationService(IRouletteProgram rouletteProgram)
    {
        _rouletteProgram = rouletteProgram;
    }
    public void NavigateHome()
    {
        Console.WriteLine("Welcome to Roulette Simulator!");
        Console.WriteLine("Please select a program:");
        Console.WriteLine("0. Roulette Simulator");
        try
        {
            NavigateTo(Convert.ToInt32(Console.ReadLine()));
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid program");
            NavigateHome();
        }
    }
    public void NavigateTo(int program)
    {
        switch (program)
        {
            case 0:
                _rouletteProgram.Run();
                break;
            default:
                NavigateHome();
                break;
        }
    }
}

internal interface INavigationService
{
    void NavigateHome();
}