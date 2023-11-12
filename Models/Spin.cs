public class Spin
{
    public int SpinId { get; set; }
    public int SessionId { get; set; }
    public virtual Session? Session { get; set; }
    public double BetAmmount { get; set; }
    public bool IsWin { get; set; }
    public DateTime SpinDate { get; set; }
    public int DrawNumber { get; set; }
    public double Balance { get; set; }
}