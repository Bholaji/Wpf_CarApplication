using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;

namespace Services.Utilities
{
    public class Utilities : IUtilities
    {

        private const int _saltSize = 16; // 128 bits
        private const int _keySize = 32; // 256 bits
        private const int _iterations = 50000;
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("mysecretkey1234567890123456789012"); // 256-bit key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("myinitializationvector"); // 128-bit IV
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

        private const char segmentDelimiter = ':';

        public bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword); // Hash the entered password

            // Compare the hashed entered password with the stored hashed password
            var x = hashedEnteredPassword.Equals(storedHashedPassword, StringComparison.OrdinalIgnoreCase);
            return x;
        }

        public bool VerifyNormalPassword(string enteredPassword, string receivedPassword)
        {
            string EnteredPassword = enteredPassword;

            var x = EnteredPassword.Equals(receivedPassword, StringComparison.OrdinalIgnoreCase);
            return x;
        }

        public string Hash(string input)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm
            );
        }

        public bool Verify(string input, string hashString)
        {
            string[] segments = hashString.Split(segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        public string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public string Encrypt(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            byte[] encryptedBytes = msEncrypt.ToArray();
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cipherTextBytes);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

        public string UploadImageAndGetUrl(string imagePath)
        {
            Account account = new(
            "dx6vy4elc",
            "876881545965133",
            "KU1YrXersVMgHj8KB2eybnq_B1w"
        );
            Cloudinary cloudinary = new(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath)
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        public void SendConfirmationEmail(string userEmail)
        {
            try
            {
                MailMessage mail = new();
                SmtpClient smtpServer = new("smtp.mail.yahoo.com");
                mail.From = new MailAddress("bolaji4aus2017@yahoo.com");
                mail.To.Add(userEmail);
                mail.Subject = "Confirmation of Your Order";
                mail.Body = "Thank you for your order! This is a confirmation email.";

                smtpServer.Port = 465; // Use 587 for TLS, or 465 for SSL
                smtpServer.EnableSsl = true;
                smtpServer.Credentials = new NetworkCredential("bolaji4aus2017@yahoo", "rah4rah1234");

                smtpServer.Send(mail);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        
        public bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object and match the email against the pattern
            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);

            return match.Success;
        }

        public async Task<bool> SendEmailAsync()
        {
            bool isSuccessful = default;
            try
            {
                string apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("bolajijohnson19@gmail.com", "Bolaji");
                var to = new EmailAddress("folusayo.abe.dev@gmail.com");
                var subject = "Confirmation of Your Order";
                var plainTextContent = "Thank you for your order! This is a confirmation email.";
                var htmlContent = "<strong>Thank you for your order!</strong> This is a confirmation email.";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                isSuccessful =  response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return isSuccessful;
        }


    }

}
