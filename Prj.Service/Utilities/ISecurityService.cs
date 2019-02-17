namespace Prj.Services.Utilities
{
    public interface ISecurityService
    {
        string Decrypt(string input, string key);
        string Encrypt(string input, string key);
        string GetMD5Hash(string input);
        string GetSha256Hash(string input);
    }
}