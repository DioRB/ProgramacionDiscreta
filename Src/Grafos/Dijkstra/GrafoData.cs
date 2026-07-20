using System.Text.Json.Serialization;
namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class GrafoData
    {
        [JsonPropertyName("nodes")]
        public List<GrafoNodoModel> Nodes { get; set; } = new();

        [JsonPropertyName("edges")]
        public List<GrafoAristaModel> Edges { get; set; } = new();
    }
}
