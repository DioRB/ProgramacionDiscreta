using ProgramacionDiscreta.Src.Grafos.Dijkstra;

namespace ProgramacionDiscreta.Src.Grafos.ColoreoGrafo
{
    public class GrafoConflicoCurso
    {
        public List<GrafoNodoModel> Nodes { get; set; } = new();
        public List<GrafoAristaModel> Edges { get; set; } = new();
    }
}
