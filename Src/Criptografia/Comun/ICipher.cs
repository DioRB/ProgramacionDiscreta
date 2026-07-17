namespace ProgramacionDiscreta.Src.Criptografia.Comun
{
    public interface ICipher
    {
        string Encrypt(string text, int key);
        string Decrypt(string text, int key);
    }
}
