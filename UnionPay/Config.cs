using System.IO;
using System.Xml;

namespace UnionPay
{
    public class Config
    {
        private static XmlDocument _xmlDoc;

        /// <summary>
        /// xml模板
        /// </summary>
        private static XmlDocument XmlDoc
        {
            get
            {
                if (_xmlDoc == null)
                {
                    _xmlDoc = new XmlDocument();
                    _xmlDoc.Load("Template.config");
                }
                return _xmlDoc;
            }
        }

        public Stream SetConfig(string key, string partner, string chairtyName)
        {
            var ConfigInf = XmlDoc.SelectSingleNode("ConfigInf");
            var securityKey = ConfigInf.SelectSingleNode("securityKey");
            securityKey.InnerText = key;

            var payParamsPredef = ConfigInf.SelectSingleNode("payParamsPredef");
            var merId = payParamsPredef.SelectSingleNode("merId");
            merId.InnerText = partner;

            var merAbbr = payParamsPredef.SelectSingleNode("merAbbr");
            merAbbr.InnerText = chairtyName;

            var bytes = System.Text.Encoding.UTF8.GetBytes(XmlDoc.InnerXml);
            var stream = new MemoryStream(bytes);

            return stream;
        }
    }
}
