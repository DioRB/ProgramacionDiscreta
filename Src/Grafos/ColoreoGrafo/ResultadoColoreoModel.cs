namespace ProgramacionDiscreta.Src.Grafos.ColoreoGrafo
{
    public class ResultadoColoreoModel
    {
        public List<AsignaColorModel> Assignments { get; set; } = new();
        public int NumberOfColors { get; set; }
        public bool IsValid { get; set; }
    }
}
