using System.Collections.Generic;
using System.Net;

namespace Dawnx.Net.Web
{
    public interface IProcessor
    {
        /// <summary>
        /// If this method cannot determine response, it should be return null.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="response"></param>
        /// <param name="method"></param>
        /// <param name="enctype"></param>
        /// <param name="url"></param>
        /// <param name="updata"></param>
        /// <param name="upfiles"></param>
        /// <returns></returns>
        HttpWebResponse Process(
            HttpAccess web, HttpWebResponse response,
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles);
    }
}
