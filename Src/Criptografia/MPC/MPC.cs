using System.Numerics;

namespace ProgramacionDiscreta.Src.Criptografia.MPC
{
    public class MPC
    {
        private readonly SecretSharing _sharingService;

        //Modulo grande
        private readonly BigInteger _modulus = 1510841;

        public MPC(SecretSharing sharingService)
        {
            _sharingService = sharingService;
        }
        // Función del proceso de suma por servidores y reconstrucción
        public MPCResultModel Process(List<int> grades)
        {
            MPCResultModel result = new();

            foreach (int grade in grades)
            {
                NotaSModel share =
                    _sharingService.SplitSecret(
                        grade,
                        _modulus);
                //Recolecta todas las notas que le corresponde a cada servidor
                result.Shares.Add(share);

                result.Server1Total += share.Share1;
                result.Server2Total += share.Share2;
                result.Server3Total += share.Share3;
            }

            result.Total =
                _sharingService.Reconstruct(
                    result.Server1Total,
                    result.Server2Total,
                    result.Server3Total,
                    _modulus);

            if (grades.Count > 0)
            {
                result.Average =
                    (double)result.Total / grades.Count;
            }

            return result;
        }
    }
}
