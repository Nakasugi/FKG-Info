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
        /// Create folder from file path if not exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateFolderForFile(string path)
        {
            string dir;

            try { dir = Path.GetDirectoryName(path); } catch { return false; }
            if (Directory.Exists(dir)) return true;
            try { Directory.CreateDirectory(dir); } catch { return false; }

            return true;
        }

        

        /// <summary>
        /// Compress to zip stream
        /// </summary>
        public static MemoryStream CompressStream(Stream srcStream, int offset, bool leaveOpen = false)
        {
            if (srcStream == null) return null;

            MemoryStream dstStream = new MemoryStream();

            srcStream.Position = offset;

            try
            {
                using (DeflateStream workStream = new DeflateStream(dstStream, CompressionMode.Compress, true))
                {
                    srcStream.CopyTo(workStream);
                }
            }
            catch
            {
                dstStream.Close();
                dstStream = null;
            }

            if (!leaveOpen) srcStream.Close();

            return dstStream;
        }



        /// <summary>
        /// Extract zip stream
        /// </summary>
        public static MemoryStream DecompressStream(Stream srcStream, int offset, bool leaveOpen = false)
        {
            if (srcStream == null) return null;

            MemoryStream dstStream = new MemoryStream();

            srcStream.Position = offset;

            try
            {
                using (DeflateStream workStream = new DeflateStream(srcStream, CompressionMode.Decompress, true))
                {
                    workStream.CopyTo(dstStream);
                }
            }
            catch
            {
                dstStream.Close();
                dstStream = null;
            }

            if (!leaveOpen) srcStream.Close();

            return dstStream;
        }



        public static byte[] CompressData(byte[] data)
        {
            if (data == null) return null;

            byte[] res;

            try
            {
                using (MemoryStream dstStream = new MemoryStream())
                {
                    using (DeflateStream workStream = new DeflateStream(dstStream, CompressionMode.Compress, true))
                    {
                        workStream.Write(data, 0, data.Length);
                    }

                    res = dstStream.ToArray();
                }
            }
            catch { res = null; }

            return res;
        }


        
        public static byte[] DecompressData(byte[] data)
        {
            if (data == null) return null;

            byte[] res;

            try
            {
                using (MemoryStream srcStream = new MemoryStream(data))
                {
                    using (MemoryStream dstStream = new MemoryStream())
                    {
                        using (DeflateStream workStream = new DeflateStream(srcStream, CompressionMode.Decompress))
                        {
                            workStream.CopyTo(dstStream);
                        }

                        res = dstStream.ToArray();
                    }
                }
            }
            catch { res = null; }

            return res;
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



        public static bool XmlCheckNode(XmlNode node, string child, ref bool value)
        {
            string temp = null;

            XmlGetText(node, child, ref temp);

            if (temp == "True") { value = true; return true; }
            if (temp == "False") { value = false; return true; }

            return false;
        }



        public static string DataToBase64(byte[] data)
        {
            if (data == null) return "Input data is null";

            try
            {
                return Convert.ToBase64String(data);
            }
            catch { return "Cannot convert to Base64"; }
        }



        public static byte[] Base64ToData(string s64)
        {
            if (s64 == null) return null;

            try
            {
                return Convert.FromBase64String(s64);
            }
            catch { return null; }
        }
    }
}
