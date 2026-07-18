using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.MPC
{
    public class MPCResultModel
    {
        public List<NotaSModel> Shares { get; set; } = new();

        public BigInteger Server1Total { get; set; }

        public BigInteger Server2Total { get; set; }

        public BigInteger Server3Total { get; set; }

        public BigInteger Total { get; set; }

        public double Average { get; set; }
    }
}
