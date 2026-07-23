namespace ProgramacionDiscreta.Src.Shannon
{
    public class ComparacionShannonService
    {
        public ResultadoComparacionModel Compare(
    ResultadoShannonModel first,
    ResultadoShannonModel second)
        {
            ResultadoComparacionModel result = new();

            result.First = first;
            result.Second = second;

            const double tolerance = 0.000001;

            if (Math.Abs(first.Entropy - second.Entropy) < tolerance)
            {
                result.Conclusion =
                    "Ambos textos tienen prácticamente la misma entropía. " +
                    "La incertidumbre al predecir el siguiente símbolo es equivalente.";
            }
            else if (first.Entropy > second.Entropy)
            {
                result.Conclusion =
                    "El primer texto tiene mayor entropía porque sus símbolos " +
                    "están distribuidos de manera más uniforme. " +
                    "Esto hace más difícil predecir cuál será el siguiente símbolo.";
            }
            else
            {
                result.Conclusion =
                    "El segundo texto tiene mayor entropía porque sus símbolos " +
                    "están distribuidos de manera más uniforme. " +
                    "Esto hace más difícil predecir cuál será el siguiente símbolo.";
            }

            return result;
        }
    }
}

