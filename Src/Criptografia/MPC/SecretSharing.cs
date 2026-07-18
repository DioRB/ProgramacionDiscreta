using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.MPC
{
    public class SecretSharing
    {
        private readonly Random _random = new();
        
        // Divide la nota en dos partes aleatorias y una tercera por la resta.
        public NotaSModel SplitSecret(int value, BigInteger modulus)
        {
            BigInteger share1 = _random.Next(0, (int)modulus);
            BigInteger share2 = _random.Next(0, (int)modulus);

            BigInteger share3 =
                Mod(value - share1 - share2, modulus);

            return new NotaSModel
            {
                OriginalValue = value,
                Share1 = share1,
                Share2 = share2,
                Share3 = share3
            };
        }

        public BigInteger Reconstruct(
            BigInteger share1,
            BigInteger share2,
            BigInteger share3,
            BigInteger modulus)
        {
            // Reconstruye sumando las partes usando el modulo
            return Mod(share1 + share2 + share3, modulus);
        }
        //Modulo
        private BigInteger Mod(BigInteger value, BigInteger modulus)
        {
            return ((value % modulus) + modulus) % modulus;
        }
    }
}
