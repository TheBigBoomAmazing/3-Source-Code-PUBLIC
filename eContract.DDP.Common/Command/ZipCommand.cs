using System;
using System.Collections.Generic;
using System.Text;
using eContract.DDP.Common.Command;
using System.Collections;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using eContract.DDP.Common.CommonJob;

namespace eContract.DDP.Common.Command
{
    public class ZipCommand:BaseCommand
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public string Type = "";                 // Compress压缩；Decompress解压
        public string SourceFileName = "";   // 源文件
        public string SourceDir = "";           // 源目录 与SourceFileName二者选一
        public string TargetFileName = "";    // 目标文件
        public string TargetDir = "";            // 目标目录

        #region ICommand Members


        /*
		<Zip Type="Compress"
				SourceFileName="%FileName%.txt" 
				TargetFileName="%FileName%.zip"/>
         */
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appDir"></param>
        /// <param name="node"></param>
        public override void Initialize(Hashtable parameters, XmlNode node)
        {
            // 先调基类函数
            base.Initialize(parameters, node);

            // 解析参数
            this.Type = XmlUtil.GetAttrValue(node, "Type");
            this.SourceFileName = XmlUtil.GetAttrValue(node, "SourceFileName");
            this.SourceFileName = BaseCommand.ReplaceParameters(parameters, this.SourceFileName);

            this.TargetFileName = XmlUtil.GetAttrValue(node, "TargetFileName");
            this.TargetFileName = BaseCommand.ReplaceParameters(parameters, this.TargetFileName);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override ResultCode Execute(ref Hashtable paramters, out string error)
        {
            error = "";

            // 压缩
            if (this.Type == DDPConst.Zip_Type_Compress)
            {
                FastZip fastZip = new FastZip();

                string dir = "";
                string fileFilter = "";

                // 支持多个来源文件
                string[] sourceFileList = this.SourceFileName.Split(new char[] { ';' });
                for (int i = 0; i < sourceFileList.Length; i++)
                {
                    string oneFile = sourceFileList[i].Trim();
                    if (oneFile != "")
                    {
                        if (dir == "")
                            dir = Path.GetDirectoryName(oneFile);

                        if (fileFilter != "")
                            fileFilter += ";";

                        fileFilter += Path.GetFileName(oneFile);

                    }
                }

                fastZip.CreateZip(this.TargetFileName, dir, false, fileFilter);

            }
            else if (this.Type == DDPConst.Zip_Type_Decompress) // 解压
            {

            }

            return ResultCode.Success; ;
        }


        #endregion

    }
}
