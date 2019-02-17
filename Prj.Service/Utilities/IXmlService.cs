namespace Prj.Services.Utilities
{
    public interface IXmlService
    {
        string GetNodeValue(string filePath, string node);
        string GetApplicationNodeValue(string node);
    }
}