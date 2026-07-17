using System.Globalization;
using System.Text;

namespace ProgramacionDiscreta.Src.Criptografia.Comun
{
    // Transforma el texto ingresado para tenerlo siempre en mayusculas sin tildes
    public static class TextNormalizer
    {
        public static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string normalized = text.Normalize(NormalizationForm.FormD);

            StringBuilder builder = new();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().ToUpper();
        }
    }
}
