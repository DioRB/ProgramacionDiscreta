namespace ProgramacionDiscreta.Src.Grafos.ImpactoRed
{
    public class ImpactoResultadoModel
    {
        public string Origin { get; set; } = "";

        public string Destination { get; set; } = "";

        public int? DistanceBefore { get; set; }

        public int? DistanceAfter { get; set; }

        public int? Difference { get; set; }

        public string Status { get; set; } = "";
    }
}
