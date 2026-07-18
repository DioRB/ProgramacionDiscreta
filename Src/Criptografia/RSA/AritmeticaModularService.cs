using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.RSA
{
    public class AritmeticaModularService
    {
        // La encriptación y desencriptación usando el metodo ModPow de BigInteger
        public BigInteger Encrypt(BigInteger message, BigInteger e, BigInteger n)
        {
            return BigInteger.ModPow(message, e, n);
        }

        public BigInteger Decrypt(BigInteger cipher, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(cipher, d, n);
        }
    }
}
