using System;
using System.Collections.Generic;
using System.Linq;
using ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana;

namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    // Simplificador de funciones booleanas mediante el algoritmo Quine-McCluskey. Soporta 3 o 4 variables, minterminos y don't-cares, y verifica automáticamente que
    // la expresión simplificada sea equivalente (misma tabla de verdad) a la original.
    public class BooleanSimplifierService : IBooleanSimplifierService
    {
        public SimplificationResultModel Simplify(SimplificationRequestModel request)
        {
            ValidateRequest(request);

            int n = request.NumberOfVariables;
            var varNames = request.VariableNames ?? DefaultVariableNames(n);

            var minterms = request.Minterms.Distinct().OrderBy(x => x).ToList();
            var dontCares = request.DontCares.Distinct().Except(minterms).OrderBy(x => x).ToList();

            var result = new SimplificationResultModel
            {
                OriginalMinterms = minterms,
                DontCares = dontCares,
                VariableNames = varNames,
                OriginalExpressionSigma = $"\u03A3m({string.Join(",", minterms)})",
                OriginalExpressionCanonica = BuildCanonicalExpression(minterms, n, varNames),
                GateCountOriginal = EstimateGateCountCanonical(minterms, n)
            };

            int totalCombinations = 1 << n;

            // Caso trivial: función siempre 0
            if (minterms.Count == 0)
            {
                result.SimplifiedExpression = "0";
                result.Steps.Add("No hay minterminos: la función es constante 0.");
                result.TruthTable = BuildTruthTable(n, minterms, dontCares, new List<PrimeImplicantModel>(), varNames);
                result.IsEquivalent = result.TruthTable.All(r => r.Match);
                result.GateCountSimplified = 0;
                return result;
            }

            // Caso trivial: función siempre 1 (todos los minterminos + don't cares cubren todo)
            if (minterms.Count + dontCares.Count >= totalCombinations && minterms.Count > 0)
            {
                // Solo es realmente "1" si TODOS los renglones no cubiertos por minterms son don't care
                bool coversAll = Enumerable.Range(0, totalCombinations).All(i => minterms.Contains(i) || dontCares.Contains(i));
                if (coversAll)
                {
                    result.SimplifiedExpression = "1";
                    result.Steps.Add("Todos los renglones de la tabla de verdad son 1 (o no importa): la función es constante 1.");
                    result.TruthTable = BuildTruthTable(n, minterms, dontCares, new List<PrimeImplicantModel>(), varNames);
                    // Para la constante 1 evaluamos manualmente
                    foreach (var row in result.TruthTable) row.SimplifiedOutput = 1;
                    result.IsEquivalent = result.TruthTable.All(r => r.Match);
                    result.GateCountSimplified = 0;
                    return result;
                }
            }

            // Agrupa términos iniciales 
            var allTerms = minterms.Concat(dontCares).Distinct().ToList();
            var currentGroup = allTerms
                .Select(m => new PrimeImplicantModel { Pattern = ToBinary(m, n), Minterms = new HashSet<int> { m } })
                .ToList();

            result.Steps.Add($"Paso 1: se listan {allTerms.Count} términos iniciales (minterminos + don't-cares) en binario de {n} bits.");

            var primeImplicants = new List<PrimeImplicantModel>();
            int iteration = 1;

            // Combina iterativamente términos que difieren en 1 bit 
            while (true)
            {
                var groupsByOnes = currentGroup
                    .GroupBy(t => t.CountOnes)
                    .OrderBy(g => g.Key)
                    .ToList();

                var newTerms = new Dictionary<string, PrimeImplicantModel>();
                var anyCombined = false;

                for (int i = 0; i < groupsByOnes.Count - 1; i++)
                {
                    // Solo se pueden combinar términos de grupos con diferencia de 1 en cantidad de unos
                    var groupA = groupsByOnes[i];
                    var groupB = groupsByOnes.FirstOrDefault(g => g.Key == groupsByOnes[i].Key + 1);
                    if (groupB == null) continue;

                    foreach (var termA in groupA)
                    {
                        foreach (var termB in groupB)
                        {
                            if (TryCombine(termA.Pattern, termB.Pattern, out string combinedPattern))
                            {
                                termA.Combined = true;
                                termB.Combined = true;
                                anyCombined = true;

                                if (!newTerms.TryGetValue(combinedPattern, out var existing))
                                {
                                    existing = new PrimeImplicantModel { Pattern = combinedPattern, Minterms = new HashSet<int>() };
                                    newTerms[combinedPattern] = existing;
                                }
                                existing.Minterms.UnionWith(termA.Minterms);
                                existing.Minterms.UnionWith(termB.Minterms);
                            }
                        }
                    }
                }

                // Los términos que no se combinaron con nadie son implicantes primos
                foreach (var t in currentGroup.Where(t => !t.Combined))
                {
                    if (!primeImplicants.Any(pi => pi.Pattern == t.Pattern))
                        primeImplicants.Add(t);
                }

                if (!anyCombined || newTerms.Count == 0)
                {
                    result.Steps.Add($"Iteración {iteration}: no quedan términos por combinar. Fin del proceso de combinación.");
                    break;
                }

                result.Steps.Add($"Iteración {iteration}: se combinaron términos adyacentes (distancia de Hamming 1), generando {newTerms.Count} términos nuevos.");
                currentGroup = newTerms.Values.ToList();
                iteration++;
            }

            // Deduplicar implicantes primos por patrón (por seguridad)
            primeImplicants = primeImplicants
                .GroupBy(p => p.Pattern)
                .Select(g =>
                {
                    var merged = g.First().Clone();
                    foreach (var extra in g.Skip(1)) merged.Minterms.UnionWith(extra.Minterms);
                    return merged;
                })
                .ToList();

            result.AllPrimeImplicants = primeImplicants;
            result.Steps.Add($"Se identificaron {primeImplicants.Count} implicantes primos en total.");

            // tabla de cobertura (chart) y esenciales
            var chart = minterms.ToDictionary(
                m => m,
                m => primeImplicants.Where(pi => pi.Minterms.Contains(m)).ToList());

            var essential = new List<PrimeImplicantModel>();
            foreach (var kv in chart)
            {
                if (kv.Value.Count == 1)
                {
                    var pi = kv.Value[0];
                    if (!essential.Contains(pi))
                    {
                        pi.IsEssential = true;
                        essential.Add(pi);
                    }
                }
            }

            result.EssentialPrimeImplicants = essential;
            result.Steps.Add(essential.Count > 0
                ? $"Se encontraron {essential.Count} implicante(s) primo(s) esencial(es): son los únicos que cubren a algún mintermino."
                : "No hay implicantes primos esenciales evidentes (cada mintermino puede cubrirse por más de un implicante).");

            var covered = new HashSet<int>();
            foreach (var pi in essential) covered.UnionWith(pi.Minterms.Intersect(minterms));

            // Cubre los minterminos restantes
            var selected = new List<PrimeImplicantModel>(essential);
            var remaining = minterms.Except(covered).ToList();

            while (remaining.Count > 0)
            {
                var candidate = primeImplicants
                    .Where(pi => !selected.Contains(pi))
                    .OrderByDescending(pi => pi.Minterms.Intersect(remaining).Count())
                    .ThenByDescending(pi => pi.CountDashes)
                    .FirstOrDefault();

                if (candidate == null || candidate.Minterms.Intersect(remaining).Count() == 0)
                    break; // seguridad: no debería ocurrir si los datos son consistentes

                selected.Add(candidate);
                remaining = remaining.Except(candidate.Minterms).ToList();
            }

            if (remaining.Count > 0)
            {
                result.Steps.Add("Advertencia: no se logró cubrir todos los minterminos (verifique los datos de entrada).");
            }

            result.SelectedPrimeImplicants = selected;

            // Construye la expresión final 
            result.SimplifiedExpression = selected.Count == 0
                ? "0"
                : string.Join(" + ", selected
                    .OrderBy(s => s.Pattern)
                    .Select(s => s.ToLiteralExpression(varNames)));

            result.GateCountSimplified = EstimateGateCountSop(selected);

            // Verificación de equivalencia (misma tabla de verdad) 
            result.TruthTable = BuildTruthTable(n, minterms, dontCares, selected, varNames);
            result.IsEquivalent = result.TruthTable.All(r => r.Match);

            result.Steps.Add(result.IsEquivalent
                ? "Verificación: la tabla de verdad de la expresión simplificada coincide con la original. Son equivalentes."
                : "Verificación: se encontraron diferencias entre la tabla de verdad original y la simplificada.");

            return result;
        }

        private static void ValidateRequest(SimplificationRequestModel request)
        {
            if (request.NumberOfVariables != 3 && request.NumberOfVariables != 4)
                throw new ArgumentException("El número de variables debe ser 3 o 4.");

            int max = (1 << request.NumberOfVariables) - 1;

            foreach (var m in request.Minterms)
                if (m < 0 || m > max)
                    throw new ArgumentException($"El mintermino {m} está fuera de rango [0,{max}] para {request.NumberOfVariables} variables.");

            foreach (var d in request.DontCares)
                if (d < 0 || d > max)
                    throw new ArgumentException($"El don't-care {d} está fuera de rango [0,{max}] para {request.NumberOfVariables} variables.");

            if (request.VariableNames != null && request.VariableNames.Count != request.NumberOfVariables)
                throw new ArgumentException("La cantidad de nombres de variables no coincide con NumberOfVariables.");
        }

        private static List<string> DefaultVariableNames(int n) =>
            n == 3 ? new List<string> { "A", "B", "C" } : new List<string> { "A", "B", "C", "D" };

        private static string ToBinary(int value, int bits) => Convert.ToString(value, 2).PadLeft(bits, '0');

        // Intenta combinar dos patrones binarios que difieren en exactamente un bit (y tienen los mismos guiones '-' en las mismas posiciones). Si se puede, devuelve el patrón combinado con un '-' en la posición que difiere.
        private static bool TryCombine(string p1, string p2, out string combined)
        {
            combined = string.Empty;
            if (p1.Length != p2.Length) return false;

            int diffCount = 0;
            int diffIndex = -1;
            for (int i = 0; i < p1.Length; i++)
            {
                if (p1[i] != p2[i])
                {
                    // Si uno tiene '-' y el otro no, en esa posición, no se pueden combinar
                    if (p1[i] == '-' || p2[i] == '-') return false;
                    diffCount++;
                    diffIndex = i;
                    if (diffCount > 1) return false;
                }
            }

            if (diffCount != 1) return false;

            var chars = p1.ToCharArray();
            chars[diffIndex] = '-';
            combined = new string(chars);
            return true;
        }

        private static string BuildCanonicalExpression(List<int> minterms, int n, List<string> varNames)
        {
            if (minterms.Count == 0) return "0";
            var terms = minterms.Select(m =>
            {
                var bin = ToBinary(m, n);
                var literals = bin.Select((c, i) => c == '1' ? varNames[i] : varNames[i] + "'");
                return string.Join("", literals);
            });
            return string.Join(" + ", terms);
        }

        private static int EstimateGateCountCanonical(List<int> minterms, int n)
        {
            // Cada mintermino usa (n-1) entradas AND (si n>1); la OR final usa (minterms-1) entradas.
            if (minterms.Count == 0) return 0;
            int andGates = minterms.Count * Math.Max(0, n - 1);
            int orGates = Math.Max(0, minterms.Count - 1);
            return andGates + orGates;
        }

        private static int EstimateGateCountSop(List<PrimeImplicantModel> terms)
        {
            if (terms.Count == 0) return 0;
            int andGates = terms.Sum(t => Math.Max(0, (t.Pattern.Length - t.CountDashes) - 1));
            int orGates = Math.Max(0, terms.Count - 1);
            return andGates + orGates;
        }

        /// Evalúa si una combinación de variables satisface al menos uno de los términos seleccionados (expresión simplificada evaluada).
        private static int EvaluateSelectedTerms(List<PrimeImplicantModel> terms, string binaryInput)
        {
            foreach (var term in terms)
            {
                bool matches = true;
                for (int i = 0; i < term.Pattern.Length; i++)
                {
                    if (term.Pattern[i] == '-') continue;
                    if (term.Pattern[i] != binaryInput[i]) { matches = false; break; }
                }
                if (matches) return 1;
            }
            return 0;
        }

        private static List<TruthTableRowModel> BuildTruthTable(
            int n, List<int> minterms, List<int> dontCares, List<PrimeImplicantModel> selectedTerms, List<string> varNames)
        {
            var rows = new List<TruthTableRowModel>();
            int total = 1 << n;

            for (int i = 0; i < total; i++)
            {
                var binary = ToBinary(i, n);
                bool isDontCare = dontCares.Contains(i);
                int originalOutput = minterms.Contains(i) ? 1 : 0;
                int simplifiedOutput = EvaluateSelectedTerms(selectedTerms, binary);

                rows.Add(new TruthTableRowModel
                {
                    Index = i,
                    Binary = binary,
                    OriginalOutput = originalOutput,
                    SimplifiedOutput = simplifiedOutput,
                    IsDontCare = isDontCare
                });
            }
            return rows;
        }
    }
}
