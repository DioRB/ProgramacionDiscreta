using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.RSA
{
    public class ExtendedEuclidService
    {
        // Calcula el maximo común divisor
        public BigInteger Gcd(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp;
            }

            return BigInteger.Abs(a);
        }

        // Calcula el inverso modular
        public BigInteger ModInverse(BigInteger e, BigInteger phi)
        {
            var (gcd, x, _) = ExtendedEuclid(e, phi);

            if (gcd != 1)
                throw new InvalidOperationException("No existe inverso modular.");

            return (x % phi + phi) % phi;
        }

        private (BigInteger gcd, BigInteger x, BigInteger y) ExtendedEuclid(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return (a, 1, 0);

            var (gcd, x1, y1) = ExtendedEuclid(b, a % b);

            BigInteger x = y1;
            BigInteger y = x1 - (a / b) * y1;

            return (gcd, x, y);
        }
    }
}
