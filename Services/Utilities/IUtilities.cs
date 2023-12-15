namespace Services.Utilities
{
    public interface IUtilities
    {
        bool VerifyPassword(string enteredPassword, string storedHashedPassword);
        string Decrypt(string cipherText);
        string Encrypt(string plainText);
        string Hash(string input);
        string HashPassword(string password);
        bool Verify(string input, string hashString);
    }
}