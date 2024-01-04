namespace Services.Utilities
{
    public interface IUtilities
    {
        bool VerifyPassword(string enteredPassword, string storedHashedPassword);
        public bool VerifyNormalPassword(string enteredPassword, string receivedPassword);
        string Decrypt(string cipherText);
        string Encrypt(string plainText);
        string Hash(string input);
        string HashPassword(string password);
        bool Verify(string input, string hashString);
        string UploadImageAndGetUrl(string imagePath);
        void SendConfirmationEmail(string userEmail);
        bool IsValidEmail(string email);
        Task<bool> SendEmailAsync();
    }
}