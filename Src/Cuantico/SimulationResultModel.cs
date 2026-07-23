namespace ProgramacionDiscreta.Src.Cuantico
{
    public class SimulationResultModel
    {
        public QubitStateModel State { get; set; } = new();

        public MeasurementResultModel Measurement { get; set; } = new();
    }
}
