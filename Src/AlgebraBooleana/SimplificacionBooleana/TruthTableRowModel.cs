namespace ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana
{
    public class TruthTableRowModel
    {
        public int Index { get; set; }
        public string Binary { get; set; } = string.Empty;
        public int OriginalOutput { get; set; }
        public int SimplifiedOutput { get; set; }
        public bool IsDontCare { get; set; }
        /// Coinciden si ambas salidas son iguales, o si la fila es "no importa"
        public bool Match => IsDontCare || OriginalOutput == SimplifiedOutput;
    }
}
