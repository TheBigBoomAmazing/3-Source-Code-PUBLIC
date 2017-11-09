using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections.Generic;


namespace eContract.DDP.Common
{
    /// <summary>
    /// 压缩类
    /// </summary>
    public class ZipClass
    {
        #region zipfile
        public static void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
        {
            //如果文件没有找到，则报错
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }

            System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry("ZippedFile");
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[BlockSize];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish();
            ZipStream.Close();
            StreamToZip.Close();
        }
        #endregion

        #region zip file dictory
        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="args">数组(数组[0]: 要压缩的目录; 数组[1]: 压缩的文件名)</param>
        public static void ZipFileDictory(string[] args)
        {
            try
            {
                FastZip fastZip = new FastZip();

                bool recurse = true;

                //压缩后的文件名，压缩目录 ，是否递归          

                //BZip2.Compress(File.OpenRead(args[0]),File.Create(args[1]),4096);     


                fastZip.CreateZip(args[1], args[0], recurse, "");

            }
            catch (Exception ex)
            {
                throw ex;
            }



            //string[] filenames = Directory.GetFiles(args[0]);

            //Crc32 crc = new Crc32();
            //ZipOutputStream s = new ZipOutputStream(File.Create(args[1]));
            //s.SetLevel(6);

            //foreach (string file in filenames)
            //{
            //    //打开压缩文件
            //    FileStream fs = File.OpenRead(file);

            //    byte[] buffer = new byte[fs.Length];
            //    fs.Read(buffer, 0, buffer.Length);
            //    ZipEntry entry = new ZipEntry(file);

            //    entry.DateTime = DateTime.Now;

            //    entry.Size = fs.Length;
            //    fs.Close();

            //    crc.Reset();
            //    crc.Update(buffer);

            //    entry.Crc = crc.Value;

            //    s.PutNextEntry(entry);

            //    s.Write(buffer, 0, buffer.Length);

            //}

            //s.Finish();
            //s.Close();
        }
        #endregion

        #region zipfile
        //Added by Andy 20150814
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        public static void ZipMultiFile(List<string> FileToZip, string ZipedFile)
        {
            try
            {                
                long pai = 1024 * 1024;//每一次写1兆
                long forint = 0;
                byte[] buffer = null;
                FileStream fs = null;
                ZipEntry entry = null;
                Crc32 crc = new Crc32();
                ZipOutputStream zipout = new ZipOutputStream(File.Create(ZipedFile));
                zipout.SetLevel(6);
                foreach (var file in FileToZip)
                {
                    fs = File.OpenRead(file);
                    forint = fs.Length % pai == 0 ? (fs.Length / pai) : (fs.Length / pai + 1);
                    entry = new ZipEntry(file.Substring(file.LastIndexOf("\\") + 1));
                    entry.Size = fs.Length;
                    entry.DateTime = DateTime.Now;
                    zipout.PutNextEntry(entry);
                    for (long i = 1; i <= forint; i++)
                    {
                        if (pai * i < fs.Length)
                        {
                            buffer = new byte[pai];
                            fs.Seek(pai * (i - 1), System.IO.SeekOrigin.Begin);
                        }
                        else
                        {
                            if (fs.Length < pai)
                            {
                                buffer = new byte[fs.Length];
                            }
                            else
                            {
                                buffer = new byte[fs.Length - pai * (i - 1)];
                                fs.Seek(pai * (i - 1), System.IO.SeekOrigin.Begin);
                            }
                        }
                        fs.Read(buffer, 0, buffer.Length);
                        crc.Reset();
                        crc.Update(buffer);
                        zipout.Write(buffer, 0, buffer.Length);
                        zipout.Flush();
                    }
                }                
                fs.Close();
                zipout.Finish();
                zipout.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        //Added by Andy 20150810
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        public static void ZipFile(string FileToZip, string ZipedFile)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            try
            {
                FileStream ZipFile = File.Create(ZipedFile);
                ZipOutputStream zipout = new ZipOutputStream(ZipFile);
                FileStream fs = File.OpenRead(FileToZip);
                long pai = 1024 * 1024;//每一次写1兆
                long forint = fs.Length % pai == 0 ? (fs.Length / pai) : (fs.Length / pai + 1);
                byte[] buffer = null;
                ZipEntry entry = new ZipEntry(FileToZip.Substring(FileToZip.LastIndexOf("\\") + 1));
                entry.Size = fs.Length;
                entry.DateTime = DateTime.Now;
                zipout.PutNextEntry(entry);
                zipout.SetLevel(6);
                Crc32 crc = new Crc32();
                for (long i = 1; i <= forint; i++)
                {
                    if (pai * i < fs.Length)
                    {
                        buffer = new byte[pai];
                        fs.Seek(pai * (i - 1), System.IO.SeekOrigin.Begin);
                    }
                    else
                    {
                        if (fs.Length < pai)
                        {
                            buffer = new byte[fs.Length];
                        }
                        else
                        {
                            buffer = new byte[fs.Length - pai * (i - 1)];
                            fs.Seek(pai * (i - 1), System.IO.SeekOrigin.Begin);
                        }
                    }
                    fs.Read(buffer, 0, buffer.Length);
                    crc.Reset();
                    crc.Update(buffer);
                    zipout.Write(buffer, 0, buffer.Length);
                    zipout.Flush();
                }
                fs.Close();
                zipout.Finish();
                zipout.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        public static void ZipFileOld(string FileToZip, string ZipedFile)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream fs = File.OpenRead(FileToZip);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            FileStream ZipFile = File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(FileToZip.Substring(FileToZip.LastIndexOf("\\") + 1));
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(6);

            ZipStream.Write(buffer, 0, buffer.Length);
            ZipStream.Finish();
            ZipStream.Close();
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        /// <param name="displayName">压缩包中文件的显示名</param>
        public static void ZipFile(string FileToZip, string ZipedFile, string displayName)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream fs = File.OpenRead(FileToZip);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            FileStream ZipFile = File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(displayName);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(6);

            ZipStream.Write(buffer, 0, buffer.Length);
            ZipStream.Finish();
            ZipStream.Close();
        }
        #endregion
    }

    /// <summary>
    ///  解压类
    /// </summary>
    public class UnZipClass
    {
        #region unzip
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="args">待解压的文件</param>
        /// <param name="args">指定解压目标目录</param>
        public static void UnZip(string ZipedFile, string FileToZip)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(ZipedFile));
            ZipEntry theEntry;
            string directoryName = Path.GetDirectoryName(FileToZip);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            //else
            //{
            //    DirectoryInfo dir = new DirectoryInfo(directoryName);
            //    FileInfo[] files = dir.GetFiles();
            //    foreach (FileInfo f in files)
            //        f.Delete();
            //}
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(theEntry.Name);
                string fileExt = Path.GetExtension(theEntry.Name).ToUpper().Substring(1);

                if (fileName != String.Empty && (fileExt == "XLS"))
                {
                    FileStream streamWriter = File.Create(FileToZip);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }

                    streamWriter.Close();
                }
            }
            s.Close();
        }


        //解压缩以后的文件名和路径，压缩前的路径
        public static Boolean UNZipFile(string zipFileName, string targetDirectory)
        {
            try
            {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(zipFileName, targetDirectory, "");
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion
    }

}
