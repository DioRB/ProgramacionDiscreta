using System.Text.Json;
using ProgramacionDiscreta.Src.Grafos.Dijkstra;

namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class GrafoService
    {
        private readonly HttpClient _httpClient;

        private GrafoData? _graph;

        public GrafoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Carga el grafo desde graph.json
        public async Task LoadGraphAsync()
        {
            if (_graph != null)
                return;

            var json =
                await _httpClient.GetStringAsync("data/grafo.json");

            _graph =
                JsonSerializer.Deserialize<GrafoData>(json)
                ?? new GrafoData();
        }

        public List<GrafoNodoModel> GetNodes()
        {
            return _graph?.Nodes ?? new();
        }

        public List<GrafoAristaModel> GetEdges()
        {
            return _graph?.Edges ?? new();
        }

        // Convierte las aristas del JSON en una lista de adyacencia
        // para que el algoritmo de Dijkstra pueda recorrer el grafo.
        public Dictionary<string, List<(string Neighbor, int Weight)>> BuildGraph()
        {
            Dictionary<string, List<(string, int)>> graph = new();

            foreach (var node in _graph!.Nodes)
            {
                graph[node.Name] = new();
            }

            foreach (var edge in _graph.Edges)
            {
                graph[edge.From].Add((edge.To, edge.Weight));
                graph[edge.To].Add((edge.From, edge.Weight));
            }

            return graph;
        }
    }
}
