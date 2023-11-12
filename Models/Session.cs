public class Session
{
    public int SessionId { get; set; }
    public double Balance { get; set; }
    public virtual List<Spin>? Spins { get; set; }
}