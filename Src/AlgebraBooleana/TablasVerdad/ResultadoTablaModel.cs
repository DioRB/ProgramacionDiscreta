namespace ProgramacionDiscreta.Src.AlgebraBooleana.TablasVerdad
{
    public class ResultadoTablaModel
    {
        public string ExpressionName { get; set; } = "";

        public List<FilaTablaVerdadModel> Rows { get; set; } = new();
    }
}
