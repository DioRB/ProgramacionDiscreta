namespace ProgramacionDiscreta.Src.AlgebraBooleana.TablasVerdad
{
    public class ExpresionBoolService
    {
        // Expresiones por medio de funciones
        public bool Expression1(
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return (A && B) || !C;
        }

        public bool Expression2(
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return (A ^ B) && C;
        }

        public bool Expression3(
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return (A || B) && (!A || C);
        }

        public bool Expression4(
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return (!A && B) || (C ^ D);
        }

        public bool Expression5(
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return (A && !B) || (!C && D);
        }

        // Se evalua cada función. Agregarla acá por cada función nueva
        public bool Evaluate(
            int option,
            bool A,
            bool B,
            bool C,
            bool D)
        {
            return option switch
            {
                1 => Expression1(A, B, C, D),
                2 => Expression2(A, B, C, D),
                3 => Expression3(A, B, C, D),
                4 => Expression4(A, B, C, D),
                5 => Expression5(A, B, C, D),
                _ => false
            };
        }

        public string GetExpressionName(int option)
        {
            return option switch
            {
                1 => "(A ∧ B) ∨ (¬C)",
                2 => "(A ⊕ B) ∧ C",
                3 => "(A ∨ B) ∧ (¬A ∨ C)",
                4 => "(¬A ∧ B) ∨ (C ⊕ D)",
                5 => "(A ∧ ¬B) ∨ (¬C ∧ D)",
                _ => ""
            };
        }
    }
}
