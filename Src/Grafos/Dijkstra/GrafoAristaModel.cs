using System.Text.Json.Serialization;

namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class GrafoAristaModel
    {
        [JsonPropertyName("from")]
        public string From { get; set; } = "";

        [JsonPropertyName("to")]
        public string To { get; set; } = "";

        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }
}
