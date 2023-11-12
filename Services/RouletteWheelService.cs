using System.Security.Cryptography;

internal sealed class RouletteWheelService : IRouletteWheelService
{
    public int Spin()
    {
        byte[] randomNumber = new byte[4];

        #pragma warning disable SYSLIB0023
        using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomNumber);
        }
        #pragma warning restore SYSLIB0023

        int rouletteNumber = BitConverter.ToInt32(randomNumber, 0);

        rouletteNumber = Math.Abs(rouletteNumber);

        return  rouletteNumber %= 37;
    }
}