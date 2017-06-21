using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FKG_Info
{
    public class StringHelper
    {
        /// <summary>
        /// Replace digit+digit to result
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ReplaceSimpleArithmetic(string src, char ch)
        {
            string rep = null, res = null, pat = "\\d+";
            string[] st = new string[2];

            switch (ch)
            {
                case '+':
                case '*':
                    pat += '\\';
                    break;
                case '-':
                case '/':
                    break;
                default: return src;
            }

            pat += ch + "\\d+";

            Regex rg = new Regex(pat);
            Match dg = rg.Match(src);

            if (dg.Success)
            {
                try
                {
                    rep = dg.Value;

                    int v1 = int.Parse(rep.Split(ch)[0]);
                    int v2 = int.Parse(rep.Split(ch)[1]);

                    float fres = 0;

                    switch (ch)
                    {
                        case '+': fres = v1 + v2; break;
                        case '-': fres = v1 - v2; break;
                        case '*': fres = v1 * v2; break;
                        case '/': fres = v1 / v2; break;
                        default: break;
                    }

                    res = fres.ToString();
                }
                catch { res = null; }
            }

            if (res != null) src = src.Replace(rep, res);

            return src;
        }



        /// <summary>
        /// Converting string to MD5 hash
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string GetHash(string original)
        {
            byte[] bytes;
            MD5 md5 = new MD5CryptoServiceProvider();

            bytes = System.Text.Encoding.ASCII.GetBytes(original);
            bytes = md5.ComputeHash(bytes);

            md5.Dispose();

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
