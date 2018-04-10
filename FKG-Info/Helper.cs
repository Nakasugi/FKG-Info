using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace FKG_Info
{
    public class Helper
    {
        /// <summary>
        /// Converting string to MD5 hash
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string original)
        {
            byte[] bytes;
            MD5 md5 = new MD5CryptoServiceProvider();

            bytes = System.Text.Encoding.ASCII.GetBytes(original);
            bytes = md5.ComputeHash(bytes);

            md5.Dispose();

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }



        /// <summary>
        /// Check is folder exists, create if folder not found.
        /// Return "true" if folder created or exist, "false" if error.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static bool CheckFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Directory Error");
                    return false;
                }
            }
            return true;
        }




        /// <summary>
        /// Extract Zip, srcStream will closed.
        /// </summary>
        /// <param name="srcStream"></param>
        /// <returns></returns>
        public static MemoryStream DecompressStream(MemoryStream srcStream)
        {
            MemoryStream dstStream = new MemoryStream();

            srcStream.Position = 2;

            try
            {
                using (DeflateStream outStream = new DeflateStream(srcStream, CompressionMode.Decompress))
                {
                    outStream.CopyTo(dstStream);
                }
            }
            catch
            {
                try
                {
                    srcStream.Position = 0;
                    srcStream.CopyTo(dstStream);
                }
                catch
                {
                    dstStream.Close();
                    dstStream = null;
                }
            }

            srcStream.Close();

            return dstStream;
        }



        public static DataGridViewRow CreateDGVRow(System.Drawing.Image image, string text, string ttip = null)
        {
            var row = new DataGridViewRow();
            var imageCell = new DataGridViewImageCell();
            var textCell = new DataGridViewTextBoxCell();

            imageCell.Value = image;
            imageCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageCell.Style.Padding = new Padding(2);
            imageCell.ToolTipText = ttip;

            textCell.Value = text;

            row.Cells.Add(imageCell);
            row.Cells.Add(textCell);

            return row;
        }



        public static bool XmlGetText(XmlNode node, string child, ref string result)
        {
            try
            {
                result = node[child].InnerText;
                return true;
            }
            catch { return false; }
        }



        public static bool XmlGetInt32(XmlNode node, string child, ref int result)
        {
            try
            {
                result = int.Parse(node[child].InnerText);
                return true;
            }
            catch { return false; }
        }



        public static bool XmlCheckNode(XmlNode node, string child, string value)
        {
            string temp = null;

            XmlGetText(node, child, ref temp);

            return value == temp;
        }
    }
}
