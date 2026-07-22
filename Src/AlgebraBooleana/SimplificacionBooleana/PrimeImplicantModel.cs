using System.Collections.Generic;
using System.Linq;

namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    public class PrimeImplicantModel
    {
        // Patrón binario, uno o cuatro caracteres por variable
        public string Pattern { get; set; } = string.Empty;

        // Minterminos (y don't-cares) que este implicante cubre
        public HashSet<int> Minterms { get; set; } = new();

        // True si ya fue combinado con otro término en una iteración posterior.
        public bool Combined { get; set; } = false;

        //True si este implicante resultó ser esencial en el análisis de cobertura
        public bool IsEssential { get; set; } = false;

        public int CountOnes => Pattern.Count(c => c == '1');
        public int CountDashes => Pattern.Count(c => c == '-');

        //Genera la expresión en literales (p.ej. "A B' D") para este implicante.
        public string ToLiteralExpression(IReadOnlyList<string> variableNames)
        {
            if (CountDashes == Pattern.Length)
                return "1"; 

            var literals = new List<string>();
            for (int i = 0; i < Pattern.Length; i++)
            {
                if (Pattern[i] == '1') literals.Add(variableNames[i]);
                else if (Pattern[i] == '0') literals.Add(variableNames[i] + "'");
                // '-' se omite: la variable no aparece en el término
            }
            return literals.Count == 0 ? "1" : string.Join("", literals);
        }

        public PrimeImplicantModel Clone() => new()
        {
            Pattern = Pattern,
            Minterms = new HashSet<int>(Minterms),
            Combined = Combined,
            IsEssential = IsEssential
        };

        public override string ToString() => $"{Pattern} -> m({string.Join(",", Minterms.OrderBy(x => x))})";
    }
}
