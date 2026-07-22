using System.Collections.Generic;

namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    public class SimplificationRequestModel
    {
        public int NumberOfVariables { get; set; } = 3;

        public List<int> Minterms { get; set; } = new();

        public List<int> DontCares { get; set; } = new();

        public List<string>? VariableNames { get; set; }
    }
}
