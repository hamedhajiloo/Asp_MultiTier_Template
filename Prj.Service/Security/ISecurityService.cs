namespace Prj.Services.Security
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}