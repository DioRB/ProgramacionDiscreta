namespace ProgramacionDiscreta.Src.AlgebraBooleana.TablasVerdad
{
    public class GeneradorTablaVerdadService
    {
        private readonly ExpresionBoolService _expressionService;

        public GeneradorTablaVerdadService(
            ExpresionBoolService expressionService)
        {
            _expressionService = expressionService;
        }

        public ResultadoTablaModel Generate(int expression)
        {
            ResultadoTablaModel table = new();

            table.ExpressionName =
                _expressionService.GetExpressionName(expression);

            for (int i = 0; i < 16; i++)
            {
                bool A = (i & 8) != 0;
                bool B = (i & 4) != 0;
                bool C = (i & 2) != 0;
                bool D = (i & 1) != 0;

                table.Rows.Add(new FilaTablaVerdadModel
                {
                    A = A,
                    B = B,
                    C = C,
                    D = D,
                    Result = _expressionService.Evaluate(
                        expression,
                        A,
                        B,
                        C,
                        D)
                });
            }

            return table;
        }
    }
}
