
namespace Prj.Services.Utilities
{
    public interface IGeneratorService
    {
        string GenerateClientId(int length);
        string GenerateRandomString(int length);
    }
}