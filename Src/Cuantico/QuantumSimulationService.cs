namespace ProgramacionDiscreta.Src.Cuantico
{
    public class QuantumSimulationService
    {
        private readonly QubitGateService gateService;
        private readonly MeasurementService measurementService;

        public QuantumSimulationService(
            QubitGateService gateService,
            MeasurementService measurementService)
        {
            this.gateService = gateService;
            this.measurementService = measurementService;
        }

        public SimulationResultModel Simulate(
            bool startInZero,
            string gateSequence,
            int measurements = 1000)
        {
            // Estado inicial
            QubitStateModel state =
                startInZero
                ? gateService.CreateZeroState()
                : gateService.CreateOneState();

            // Aplicar las compuertas indicadas
            state =
                gateService.ApplyGateSequence(
                    state,
                    gateSequence);

            // Simular las mediciones
            MeasurementResultModel measurement =
                measurementService.Measure(
                    state,
                    measurements);

            return new SimulationResultModel
            {
                State = state,
                Measurement = measurement
            };
        }

        // Método de apoyo para mostrar el estado del qubit
        public string GetStateAsString(
            QubitStateModel state)
        {
            return $"{state.Alpha:F6}|0⟩ + {state.Beta:F6}|1⟩";
        }
    }
}
