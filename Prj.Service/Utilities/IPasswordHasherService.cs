namespace Prj.Services.Utilities
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}