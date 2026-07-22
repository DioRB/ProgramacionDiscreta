namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    public interface IBooleanSimplifierService
    {
            /// <summary>
            /// Simplifica una función booleana de 3 o 4 variables dada por sus minterminos,
            /// usando el algoritmo Quine-McCluskey, y verifica que la expresión resultante
            /// tenga la misma tabla de verdad que la original.
            /// </summary>
            SimplificationResultModel Simplify(SimplificationRequestModel request);
        }
}
