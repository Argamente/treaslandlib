using System;
using System.Collections.Generic;
using System.IO;

namespace TreaslandLib.Utils
{
    public class IOUtils
    {
        // -------------------------- 文件读写操作 --------------------------------
        /// <summary>
        /// 读文本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string OpenTextFile(string filePath, System.Text.Encoding encoding = null)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs, encoding == null ? System.Text.Encoding.UTF8 : encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        public void WriteTextFile(string filePath, string text, System.Text.Encoding encoding = null)
        {
            try
            {
                CreateParentDirectory(filePath);
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    if(encoding == null)
                    {
                        encoding = new System.Text.UTF8Encoding();
                    }

                    byte[] data = encoding.GetBytes(text);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch(Exception e)
            {
                // how should i do?
                // 狗屎啊，居然不让我请假，明明没什么事情做，都不知道风秀还能玩多久，
                // 仅仅是因为Boss在这边，就不能请假了？Fuck
            }
        }


        /// <summary>
        /// 追加文本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void AppendTextFile(string filePath,string text)
        {
            try
            {
                CreateParentDirectory(filePath);
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    byte[] data = new System.Text.UTF8Encoding().GetBytes(text);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch(Exception e)
            {
                // how should i do? Fuck
            }
        }

        
        /// <summary>
        /// 打开二进制文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] OpenBinaryFile(string filePath)
        {
            byte[] bytes = null;
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        bytes = new byte[fs.Length];
                        reader.Read(bytes, 0, bytes.Length);
                    }
                }
            }
            return bytes;
        }


        /// <summary>
        /// 写二进制文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bytes"></param>
        public static void WriteBinaryFile(string filePath, byte[] bytes)
        {
            CreateParentDirectory(filePath);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(bytes);
                }
            }
        }



        // -------------------------- 目录操作 --------------------------------

            /// <summary>
            /// 创建文件的父目录
            /// </summary>
            /// <param name="filePath"></param>
        public static void CreateParentDirectory(string filePath)
        {
            string parentFolderName = new FileInfo(filePath).DirectoryName;
            if (!Directory.Exists(parentFolderName))
            {
                Directory.CreateDirectory(parentFolderName);
            }
        }
    }
}
