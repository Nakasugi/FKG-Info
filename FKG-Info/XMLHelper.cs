using System.Xml;

namespace FKG_Info
{
    public class XmlHelper
    {
        public static string GetText(XmlNode node, string child)
        {
            string res;

            try
            {
                res = node[child].InnerText;
            }
            catch
            {
                res = "";
            }

            return res;
        }
        


        public static int GetInt32(XmlNode node, string child)
        {
            int res;

            try
            {
                res = int.Parse(node[child].InnerText);
            }
            catch
            {
                res = 0;
            }

            return res;
        }
    }
}