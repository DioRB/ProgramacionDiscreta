using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.RSA
{
    public class RSAResultModel
    {
        public bool IsValid { get; set; }

        public string ValidationMessage { get; set; } = "";

        public BigInteger P { get; set; }

        public BigInteger Q { get; set; }

        public BigInteger E { get; set; }

        public BigInteger D { get; set; }

        public BigInteger N { get; set; }

        public BigInteger Phi { get; set; }

        public BigInteger Message { get; set; }

        public BigInteger Cipher { get; set; }

        public BigInteger DecryptedMessage { get; set; }
    }
}
