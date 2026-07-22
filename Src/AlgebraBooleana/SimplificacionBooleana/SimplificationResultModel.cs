using System.Collections.Generic;

namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    public class SimplificationResultModel
    {
        public List<int> OriginalMinterms { get; set; } = new();
        public List<int> DontCares { get; set; } = new();
        public List<string> VariableNames { get; set; } = new();

        public List<PrimeImplicantModel> AllPrimeImplicants { get; set; } = new();

        public List<PrimeImplicantModel> EssentialPrimeImplicants { get; set; } = new();

        public List<PrimeImplicantModel> SelectedPrimeImplicants { get; set; } = new();
     
        public string OriginalExpressionCanonica { get; set; } = string.Empty;

        public string OriginalExpressionSigma { get; set; } = string.Empty;

        public string SimplifiedExpression { get; set; } = string.Empty;

        public bool IsEquivalent { get; set; }

        public List<TruthTableRowModel> TruthTable { get; set; } = new();

        public List<string> Steps { get; set; } = new();

        public int GateCountOriginal { get; set; }
        public int GateCountSimplified { get; set; }
    }
} 
