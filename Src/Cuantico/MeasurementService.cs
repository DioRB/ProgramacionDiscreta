namespace ProgramacionDiscreta.Src.Cuantico
{
    public class MeasurementService
    {
        private readonly Random random = new();

        public MeasurementResultModel Measure(
            QubitStateModel state,
            int numberOfMeasurements = 1000)
        {
            MeasurementResultModel result = new();

            result.Probability0 =
                state.Alpha * state.Alpha;

            result.Probability1 =
                state.Beta * state.Beta;

            // Evita pequeños errores de precisión
            NormalizeProbabilities(result);

            for (int i = 0; i < numberOfMeasurements; i++)
            {
                double value = random.NextDouble();

                if (value < result.Probability0)
                {
                    result.Measured0++;
                }
                else
                {
                    result.Measured1++;
                }
            }

            return result;
        }

        private void NormalizeProbabilities(
            MeasurementResultModel result)
        {
            double sum =
                result.Probability0 +
                result.Probability1;

            if (sum == 0)
            {
                return;
            }

            result.Probability0 /= sum;
            result.Probability1 /= sum;
        }

        public double GetObservedProbability0(
            MeasurementResultModel result)
        {
            int total =
                result.Measured0 +
                result.Measured1;

            if (total == 0)
            {
                return 0;
            }

            return (double)result.Measured0 / total;
        }

        public double GetObservedProbability1(
            MeasurementResultModel result)
        {
            int total =
                result.Measured0 +
                result.Measured1;

            if (total == 0)
            {
                return 0;
            }

            return (double)result.Measured1 / total;
        }
    }
}

