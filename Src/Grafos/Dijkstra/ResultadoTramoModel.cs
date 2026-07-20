namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class ResultadoTramoModel
    {
        public List<string> Path { get; set; } = new();

        public int Distance { get; set; }

        public bool Exists { get; set; }
    }
}
