namespace ProgramacionDiscreta.Src.Grafos.ColoreoGrafo
{
    public class ValidacionColoreoService
    {
        // Verifica que dos vértices adyacentes nunca tengan el mismo color
        public bool Validate(
            Dictionary<string, List<string>> graph,
            List<AsignaColorModel> assignments)
        {
            Dictionary<string, int> colors =
                assignments.ToDictionary(
                    a => a.Vertex,
                    a => a.Color);

            foreach (var vertex in graph.Keys)
            {
                foreach (var neighbor in graph[vertex])
                {
                    if (colors[vertex] == colors[neighbor])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
