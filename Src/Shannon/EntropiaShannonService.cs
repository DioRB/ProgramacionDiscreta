namespace ProgramacionDiscreta.Src.Shannon
{
        public class EntropiaShannonService
        {
            public ResultadoShannonModel Analyze(string text)
            {
                ResultadoShannonModel result = new();

                if (string.IsNullOrWhiteSpace(text))
                {
                    return result;
                }

                result.Text = text;
                result.Length = text.Length;

                Dictionary<char, int> frequencies =
                    CountSymbols(text);

                foreach (var pair in frequencies.OrderBy(p => p.Key))
                {
                    InfoSimboloModel symbol = new();

                    symbol.Symbol = pair.Key;
                    symbol.Frequency = pair.Value;
                    symbol.Probability =
                        (double)pair.Value / text.Length;

                    result.Symbols.Add(symbol);
                }

                result.Entropy =
                    CalculateEntropy(result.Symbols);

                return result;
            }

            // Cuenta cuántas veces aparece cada símbolo
            private Dictionary<char, int> CountSymbols(
                string text)
            {
                Dictionary<char, int> frequencies = new();

                foreach (char c in text)
                {
                    if (frequencies.ContainsKey(c))
                    {
                        frequencies[c]++;
                    }
                    else
                    {
                        frequencies[c] = 1;
                    }
                }

                return frequencies;
            }

            // Calcula H = -Σ pi log2(pi)
            private double CalculateEntropy(
                List<InfoSimboloModel> symbols)
            {
                double entropy = 0;

                foreach (var symbol in symbols)
                {
                    double p = symbol.Probability;

                    if (p > 0)
                    {
                        entropy -=
                            p * Math.Log2(p);
                    }
                }

                return entropy;
            }
        }
}