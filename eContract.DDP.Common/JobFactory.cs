using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Reflection;
using System.Diagnostics;
using eContract.DDP.Common;
using eContract.DDP.Common.CommonJob;

namespace eContract.DDP.Common
{
    public class JobFactory
    {
        /// <summary>
        /// 创建Job实例
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IJob CreateJobInstance(string assemblyName,
            string className)
        {
            Debug.Assert(String.IsNullOrEmpty(assemblyName) == false, "assemblyName参数不能为空");
            Debug.Assert(String.IsNullOrEmpty(className) == false, "className参数不能为空");

            Assembly assembly = Assembly.LoadFrom(assemblyName);
            if (assembly == null)
                return null;

            Object obj = assembly.CreateInstance(className);

            return (IJob)obj;
        }
    }
}
