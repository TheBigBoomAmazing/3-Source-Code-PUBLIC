using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using eContract.DDP.Common.Command;
//using LIPS.WinService.Command;

namespace eContract.DDP.Common.Command
{
    /// <summary>
    /// 命令工厂
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// 根据xml配置创建命令
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(string appDir,XmlNode node)
        {
            ICommand command = null;
            if (node.Name == "Database")
                command = new DatabaseCommand();
            else if (node.Name == "DatabaseHeaderDetail")
                command = new DBHeaderDetailCommand();
            else if (node.Name == "DBtoFile")
                command = new DBtoFileCommand();
            else if (node.Name == "Zip")
                command = new ZipCommand();
            else if (node.Name == "File")
                command = new FileCommand();
            //else if (node.Name == "LIPS_UpdateItemFlagCmd")
            //    command = new LIPS_UpdateItemFlagCmd();
            //else if (node.Name == "LIPS_UpdateOrderFlagCmd")
            //    command = new LIPS_UpdateOrderFlagCmd();
            //else if (node.Name == "LIPS_UpdateASNFlagCmd")
            //    command = new LIPS_UpdateASNFlagCmd();
            else
            {
                string dllName = XmlUtil.GetAttrValue(node, "DLL");
                if (dllName != "")
                    dllName = appDir + "\\" + dllName;

                string className = XmlUtil.GetAttrValue(node, "Class");

                if (dllName != "" && className != "")
                    command = CreateCommandInstance(dllName, className);
            }
            return command;
        }

        /// <summary>
        /// 创建Job实例
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static ICommand CreateCommandInstance(string assemblyName,
            string className)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyName);
            if (assembly == null)
                return null;

            Object obj = assembly.CreateInstance(className);

            return (ICommand)obj;
        }
    }
}
