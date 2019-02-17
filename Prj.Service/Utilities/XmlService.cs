using System.Web;
using System.Xml;

namespace Prj.Services.Utilities
{
    public class XmlService : IXmlService
    {
        public string GetNodeValue(string filePath, string node)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);
            return doc.DocumentElement.SelectSingleNode(node).InnerText;
        }
        public string GetApplicationNodeValue(string node)
        {
            var filePath = HttpContext.Current.Server
                    .MapPath("~" + "\\App_Data\\Application.xml");

            return GetNodeValue(filePath, node);
        }
    }
}
