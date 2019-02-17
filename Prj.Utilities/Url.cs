using System;
using System.Web;

namespace Prj.Utilities
{
    public static class Url
    {
        public const string DefaultUrl = "~/Content/Images/no-image.png";

        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://", StringComparison.Ordinal) > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }

        /// <summary>
        /// مخصوص فروشگاه
        /// </summary>
        /// <param name="serverurl"></param>
        /// <param name="forceHttps"></param>
        /// <param name="defaulturl"></param>
        /// <returns></returns>
        public static string ResolveServerUrlForShop(string serverurl, string defaulturl, bool forceHttps)
        {
            if (serverurl == null)
                serverurl = string.Empty;

            if (serverurl.ToLower().Contains("~/files"))
            {
                return ResolveServerUrl(VirtualPathUtility
                          .ToAbsolute(!string.IsNullOrEmpty(serverurl) ? serverurl : defaulturl ?? DefaultUrl), forceHttps);
            }

            return ResolveServerUrl(VirtualPathUtility
                        .ToAbsolute(!string.IsNullOrEmpty(serverurl) ? ImageUtility.ShopRoot + serverurl : defaulturl ?? DefaultUrl), forceHttps);
        }

        /// <returns></returns>
        public static string ResolveDynamicUrl(string action, string paramter)
        {
            return $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Host}/api/Common/Image/{action}?{paramter}";

        }
    }
}
