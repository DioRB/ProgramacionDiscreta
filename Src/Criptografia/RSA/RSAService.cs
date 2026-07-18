using ProgramacionDiscreta.Src.Criptografia.RSA;
using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.RSA
{
    public class RSAService
    {
        private readonly ExtendedEuclidService _euclid;
        private readonly AritmeticaModularService _modular;

        public RSAService(
            ExtendedEuclidService euclid,
            AritmeticaModularService modular)
        {
            _euclid = euclid;
            _modular = modular;
        }

        // Hace el proceso de implementación y cálculos, junto con el de Euclides
        public RSAResultModel Process(
            BigInteger p,
            BigInteger q,
            BigInteger e,
            BigInteger message)
        {
            RSAResultModel result = new();

            result.P = p;
            result.Q = q;
            result.E = e;
            result.Message = message;

            result.N = p * q;
            result.Phi = (p - 1) * (q - 1);

            // Verificación de si e es valido
            if (_euclid.Gcd(e, result.Phi) != 1)
            {
                result.IsValid = false;
                result.ValidationMessage =
                    "e no es válido porque gcd(e, φ(n)) ≠ 1.";

                return result;
            }

            result.IsValid = true;

            result.D = _euclid.ModInverse(e, result.Phi);

            result.Cipher =
                _modular.Encrypt(message, e, result.N);

            result.DecryptedMessage =
                _modular.Decrypt(result.Cipher, result.D, result.N);

            return result;
        }
    }
}
