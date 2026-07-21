namespace ProgramacionDiscreta.Src.Grafos.ColoreoGrafo
{
    public class ColoreoGrafoService
    {
        public ResultadoColoreoModel ColorGraph(
    Dictionary<string, List<string>> graph)
        {
            Dictionary<string, int> assignedColors = new();

            foreach (var vertex in graph.Keys.OrderBy(v => v))
            {
                int color = GetFirstAvailableColor(
                    vertex,
                    assignedColors,
                    graph);

                assignedColors[vertex] = color;
            }

            ResultadoColoreoModel result = new();

            result.Assignments = assignedColors
                .Select(pair => new AsignaColorModel
                {
                    Vertex = pair.Key,
                    Color = pair.Value
                })
                .ToList();

            result.NumberOfColors =
                assignedColors.Values.Max() + 1;

            return result;
        }

        // Busca el primer color que no esté siendo usado por los vecinos
        private int GetFirstAvailableColor(
            string vertex,
            Dictionary<string, int> assignedColors,
            Dictionary<string, List<string>> graph)
        {
            HashSet<int> usedColors = new();

            foreach (var neighbor in graph[vertex])
            {
                if (assignedColors.ContainsKey(neighbor))
                {
                    usedColors.Add(
                        assignedColors[neighbor]);
                }
            }

            int color = 0;

            while (usedColors.Contains(color))
            {
                color++;
            }

            return color;
        }

        // Agrupa los vértices por color para mostrarlos fácilmente
        public Dictionary<int, List<string>> GroupByColor(
            ResultadoColoreoModel result)
        {
            return result.Assignments
                .GroupBy(a => a.Color)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(a => a.Vertex).ToList());
        }
    }
}
