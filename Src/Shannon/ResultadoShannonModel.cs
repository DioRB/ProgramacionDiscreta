namespace ProgramacionDiscreta.Src.Shannon
{
    public class ResultadoShannonModel
    {
        public string Text { get; set; } = "";

        public int Length { get; set; }

        public double Entropy { get; set; }

        public List<InfoSimboloModel> Symbols { get; set; } = new();

    }
}
