using ProgramacionDiscreta.Src.Criptografia.Comun;
using System.Text;

namespace ProgramacionDiscreta.Src.Criptografia.Cesar
{
    public class CesarCipher : ICipher
    {
        public string Encrypt(string text, int key)
        {
            return Transform(text, key);
        }

        public string Decrypt(string text, int key)
        {
            return Transform(text, -key);
        }

        // Muestra la transformacion para todas las k (ataque de fuerza bruta)
        public List<CesarBruteForceResult> BruteForce(string text)
        {
            List<CesarBruteForceResult> results = new();

            for (int i = 0; i < Alfabeto.Length; i++)
            {
                results.Add(new CesarBruteForceResult
                {
                    Shift = i,
                    Text = Decrypt(text, i)
                });
            }

            return results;
        }

        private string Transform(string text, int shift)
        {
            text = TextNormalizer.Normalize(text);

            StringBuilder result = new();

            foreach (char c in text)
            {
                result.Append(ShiftCharacter(c, shift));
            }

            return result.ToString();
        }

        // Devuelve el indice igual si no pertenece al alfabeto
        private char ShiftCharacter(char c, int shift)
        {
                int index = Alfabeto.Letters.IndexOf(c);

                if (index == -1)
                    return c;

                int newIndex = (index + NormalizeShift(shift)) % Alfabeto.Length;

                return Alfabeto.Letters[newIndex];
        }

        // Mueve los indices (como un mod) para que no se corra el k
        private int NormalizeShift(int shift)
        {
            shift %= Alfabeto.Length;

            if (shift < 0)
                shift += Alfabeto.Length;

            return shift;

        }
    }

}
