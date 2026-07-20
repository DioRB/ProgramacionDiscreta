using ProgramacionDiscreta.Src.Grafos.Dijkstra;

namespace ProgramacionDiscreta.Src.Grafos.ImpactoRed
{
    public class ImpactoRedService
    {
        private readonly DijkstraService _dijkstra;

        public ImpactoRedService(DijkstraService dijkstra)
        {
            _dijkstra = dijkstra;
        }

        // Analiza el efecto de cerrar un vértice
        public List<ImpactoResultadoModel> AnalyzeNodeClosure(
            Dictionary<string, List<(string Neighbor, int Weight)>> originalGraph,
            string nodeToRemove,
            List<(string Origin, string Destination)> pairs)
        {
            var modifiedGraph = CloneGraph(originalGraph);

            RemoveNode(modifiedGraph, nodeToRemove);

            return CompareGraphs(
                originalGraph,
                modifiedGraph,
                pairs);
        }

        // Analiza el efecto de cerrar una arista
        public List<ImpactoResultadoModel> AnalyzeEdgeClosure(
            Dictionary<string, List<(string Neighbor, int Weight)>> originalGraph,
            string from,
            string to,
            List<(string Origin, string Destination)> pairs)
        {
            var modifiedGraph = CloneGraph(originalGraph);

            RemoveEdge(modifiedGraph, from, to);

            return CompareGraphs(
                originalGraph,
                modifiedGraph,
                pairs);
        }

        // Compara las rutas antes y después del cierre
        private List<ImpactoResultadoModel> CompareGraphs(
            Dictionary<string, List<(string Neighbor, int Weight)>> original,
            Dictionary<string, List<(string Neighbor, int Weight)>> modified,
            List<(string Origin, string Destination)> pairs)
        {
            List<ImpactoResultadoModel> results = new();

            foreach (var pair in pairs)
            {
                var before =
                    _dijkstra.FindShortestPath(
                        original,
                        pair.Origin,
                        pair.Destination);

                var after =
                    _dijkstra.FindShortestPath(
                        modified,
                        pair.Origin,
                        pair.Destination);

                ImpactoResultadoModel result = new()
                {
                    Origin = pair.Origin,
                    Destination = pair.Destination
                };

                if (!before.Exists)
                {
                    result.Status = "No existía camino";

                    results.Add(result);

                    continue;
                }

                result.DistanceBefore = before.Distance;

                if (!after.Exists)
                {
                    result.DistanceAfter = null;
                    result.Difference = null;
                    result.Status = "Desconectado";

                    results.Add(result);

                    continue;
                }

                result.DistanceAfter = after.Distance;

                result.Difference =
                    after.Distance - before.Distance;

                if (result.Difference == 0)
                {
                    result.Status = "Sin cambios";
                }
                else if (result.Difference > 0)
                {
                    result.Status = "Aumentó";
                }
                else
                {
                    result.Status = "Disminuyó";
                }

                results.Add(result);
            }

            return results;
        }

        // Copia el grafo para no modificar el original
        private Dictionary<string, List<(string Neighbor, int Weight)>> CloneGraph(
            Dictionary<string, List<(string Neighbor, int Weight)>> graph)
        {
            Dictionary<string, List<(string, int)>> copy = new();

            foreach (var node in graph)
            {
                copy[node.Key] = new();

                foreach (var edge in node.Value)
                {
                    copy[node.Key].Add(edge);
                }
            }

            return copy;
        }

        // Elimina un vértice y todas sus conexiones
        private void RemoveNode(
            Dictionary<string, List<(string Neighbor, int Weight)>> graph,
            string node)
        {
            if (!graph.ContainsKey(node))
                return;

            graph.Remove(node);

            foreach (var vertex in graph.Keys)
            {
                graph[vertex].RemoveAll(
                    e => e.Neighbor == node);
            }
        }

        // Elimina una conexión entre dos vértices
        private void RemoveEdge(
            Dictionary<string, List<(string Neighbor, int Weight)>> graph,
            string from,
            string to)
        {
            if (graph.ContainsKey(from))
            {
                graph[from].RemoveAll(
                    e => e.Neighbor == to);
            }

            if (graph.ContainsKey(to))
            {
                graph[to].RemoveAll(
                    e => e.Neighbor == from);
            }
        }
    }
}
