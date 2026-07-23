namespace ProgramacionDiscreta.Src.Cuantico
{
    public class QubitGateService
    {
        private readonly double InverseSqrt2 =
            1.0 / Math.Sqrt(2);

        // |0> = [1, 0]
        public QubitStateModel CreateZeroState()
        {
            return new QubitStateModel
            {
                Alpha = 1,
                Beta = 0
            };
        }

        // |1> = [0, 1]
        public QubitStateModel CreateOneState()
        {
            return new QubitStateModel
            {
                Alpha = 0,
                Beta = 1
            };
        }

        // Compuerta X
        //
        // [0 1]
        // [1 0]
        //
        public QubitStateModel ApplyX(
            QubitStateModel state)
        {
            double[,] matrix =
            {
            { 0, 1 },
            { 1, 0 }
        };

            return MultiplyMatrixByState(
                matrix,
                state);
        }

        // Compuerta Z
        //
        // [ 1  0 ]
        // [ 0 -1 ]
        //
        public QubitStateModel ApplyZ(
            QubitStateModel state)
        {
            double[,] matrix =
            {
            { 1, 0 },
            { 0, -1 }
        };

            return MultiplyMatrixByState(
                matrix,
                state);
        }

        // Compuerta Hadamard
        //
        // 1/sqrt(2)
        // [ 1  1 ]
        // [ 1 -1 ]
        //
        public QubitStateModel ApplyH(
            QubitStateModel state)
        {
            double[,] matrix =
            {
            { InverseSqrt2, InverseSqrt2 },
            { InverseSqrt2, -InverseSqrt2 }
        };

            return MultiplyMatrixByState(
                matrix,
                state);
        }

        // Permite aplicar una secuencia de compuertas
        // Ejemplo:
        // "H"
        // "HH"
        // "HX"
        // "HXZ"
        //
        public QubitStateModel ApplyGateSequence(
            QubitStateModel initialState,
            string gates)
        {
            QubitStateModel current =
                CloneState(initialState);

            foreach (char gate in gates.ToUpper())
            {
                switch (gate)
                {
                    case 'X':
                        current = ApplyX(current);
                        break;

                    case 'Z':
                        current = ApplyZ(current);
                        break;

                    case 'H':
                        current = ApplyH(current);
                        break;
                }
            }

            return current;
        }

        // Multiplicación matriz 2x2 por vector 2x1
        private QubitStateModel MultiplyMatrixByState(
            double[,] matrix,
            QubitStateModel state)
        {
            double alpha =
                matrix[0, 0] * state.Alpha +
                matrix[0, 1] * state.Beta;

            double beta =
                matrix[1, 0] * state.Alpha +
                matrix[1, 1] * state.Beta;

            QubitStateModel result =
                new QubitStateModel
                {
                    Alpha = alpha,
                    Beta = beta
                };

            Normalize(result);

            return result;
        }

        // Mantiene:
        //
        // |α|² + |β|² = 1
        //
        private void Normalize(
            QubitStateModel state)
        {
            double norm =
                Math.Sqrt(
                    state.Alpha * state.Alpha +
                    state.Beta * state.Beta);

            if (norm == 0)
            {
                return;
            }

            state.Alpha /= norm;
            state.Beta /= norm;
        }

        private QubitStateModel CloneState(
            QubitStateModel state)
        {
            return new QubitStateModel
            {
                Alpha = state.Alpha,
                Beta = state.Beta
            };
        }
    }
}
