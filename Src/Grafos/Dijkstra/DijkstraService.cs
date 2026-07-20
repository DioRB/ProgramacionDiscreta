using ProgramacionDiscreta.Src.Grafos.Dijkstra;
namespace ProgramacionDiscreta.Src.Grafos.Dijkstra
{
    public class DijkstraService
    {
        public ResultadoTramoModel FindShortestPath(
            Dictionary<string, List<(string Neighbor, int Weight)>> graph,
            string start,
            string end)
        {
            // Distancia mínima conocida hacia cada vértice
            Dictionary<string, int> distances = new();

            // Permite reconstruir la ruta óptima al finalizar
            Dictionary<string, string?> previous = new();

            // Conjunto de vértices pendientes por visitar
            HashSet<string> unvisited = new();

            foreach (var vertex in graph.Keys)
            {
                distances[vertex] = int.MaxValue;
                previous[vertex] = null;
                unvisited.Add(vertex);
            }

            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                string current = GetClosestVertex(
                    unvisited,
                    distances);

                // Si el vértice más cercano sigue siendo infinito,
                // significa que ya no existen caminos posibles.
                if (distances[current] == int.MaxValue)
                    break;

                if (current == end)
                    break;

                unvisited.Remove(current);

                foreach (var edge in graph[current])
                {
                    if (!unvisited.Contains(edge.Neighbor))
                        continue;

                    int newDistance =
                        distances[current] + edge.Weight;

                    // Si encontramos un camino más corto,
                    // actualizamos la distancia y el predecesor.
                    if (newDistance < distances[edge.Neighbor])
                    {
                        distances[edge.Neighbor] = newDistance;
                        previous[edge.Neighbor] = current;
                    }
                }
            }

            if (distances[end] == int.MaxValue)
            {
                return new ResultadoTramoModel
                {
                    Exists = false
                };
            }

            List<string> path = BuildPath(previous, end);

            return new ResultadoTramoModel
            {
                Exists = true,
                Distance = distances[end],
                Path = path
            };
        }

        private string GetClosestVertex(
            HashSet<string> unvisited,
            Dictionary<string, int> distances)
        {
            string closest = "";

            int minDistance = int.MaxValue;

            foreach (string vertex in unvisited)
            {
                if (distances[vertex] < minDistance)
                {
                    minDistance = distances[vertex];
                    closest = vertex;
                }
            }

            return closest;
        }

        private List<string> BuildPath(
            Dictionary<string, string?> previous,
            string destination)
        {
            List<string> path = new();

            string? current = destination;

            // Se reconstruye la ruta recorriendo los predecesores
            // desde el destino hasta el origen.
            while (current != null)
            {
                path.Add(current);

                current = previous[current];
            }

            path.Reverse();

            return path;
        }
    }
}
