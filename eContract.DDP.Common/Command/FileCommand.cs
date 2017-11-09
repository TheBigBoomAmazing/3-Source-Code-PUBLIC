using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using eContract.DDP.Common.CommonJob;
using System.IO;
using System.Xml;

namespace eContract.DDP.Common.Command
{
    public class FileCommand:BaseCommand
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public string Type = "";                 // rename
        public string SourceFileName = "";   // 源文件
        public string TargetFileName = "";    // 目标文件

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

            // 改名
            if (this.Type == DDPConst.File_Type_Rename)
            {
                if (File.Exists(this.TargetFileName) == true)
                    File.Delete(this.TargetFileName);

                FileInfo fi = new FileInfo(this.SourceFileName);
                fi.MoveTo(this.TargetFileName);
            }

            return ResultCode.Success; ;
        }
    }
}
