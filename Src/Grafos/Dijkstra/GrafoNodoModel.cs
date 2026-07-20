using System.Text.Json.Serialization;
namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class GrafoNodoModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
