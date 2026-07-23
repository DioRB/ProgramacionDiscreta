namespace ProgramacionDiscreta.Src.Shannon
{
    public class ResultadoComparacionModel
    {
        public ResultadoShannonModel First { get; set; } = new();

        public ResultadoShannonModel Second { get; set; } = new();

        public string Conclusion { get; set; } = "";
    }
}
