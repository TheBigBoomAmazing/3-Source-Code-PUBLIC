using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using eContract.DDP.Common;
using eContract.Log;

namespace eContract.DDP.Common
{
    public class ScheduleManager : List<ScheduleEntity>
    {
        /// <summary>
        /// 资源目录
        /// </summary>
        public string ResourceDir = "";

        /// <summary>
        /// Schedule文件名
        /// </summary>
        private string _scheduleFileName = "";

        /// <summary>
        /// Schedule dom
        /// </summary>
        private XmlDocument _dom = null;

        /// <summary>
        ///  是否正在加载
        /// </summary>
        public bool IsLoding = false;

        public event EventHandler ScheduleChanged;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleManager(string resourceDir)
        {
            this.ResourceDir = resourceDir;
            this._scheduleFileName = resourceDir + "\\" + "Schedules.xml";

            this.LoadSchedule();
        }

        /// <summary>
        /// 加载Schedule
        /// </summary>
        public void LoadSchedule()
        {
            if (this.IsLoding == true)
            {
                // 写日志
                LogManager.Current.WriteCommonLog("", "目前正在加载Schedule，不能继续加载。", "");
                return;
            }

            this.IsLoding = true;

            // 写日志
            LogManager.Current.WriteCommonLog("", "加载Schedule开始", "");

            // 先清空原来的
            this.Clear();

            // 将配置文件加载到dom，如果不存在，新创建文件
            this._dom = new XmlDocument();
            if (File.Exists(this._scheduleFileName) == false)
            {
                this._dom.LoadXml("<root/>");
                this.SaveDom2File();
            }
            else
            {
                this._dom.Load(this._scheduleFileName);
            }

            // 解析出jobs
            XmlNode nodeRoot = this._dom.DocumentElement;
            XmlNodeList listSchedule = nodeRoot.SelectNodes("Schedule");
            for (int i = 0; i < listSchedule.Count; i++)
            {
                XmlNode nodeSchedule = listSchedule[i];
                ScheduleEntity entity = new ScheduleEntity(nodeSchedule);
                this.Add(entity);
            }

            // 写日志
            LogManager.Current.WriteCommonLog("", "共加载'" + this.Count.ToString() + "'个Schedule", "");

            this.IsLoding = false;
        }

        /// <summary>
        /// 根据ID获取Schedule实体
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public ScheduleEntity GetScheduleEntityByID(string scheduleID)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ScheduleEntity entity = this[i];
                if (entity.ScheduleID == scheduleID)
                    return entity;
            }

            return null;
        }


        /// <summary>
        /// 根据ID获取Schedule实体
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public ScheduleEntity GetScheduleByJobCode(string jobCode)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ScheduleEntity entity = this[i];
                if (entity.JobCode == jobCode)
                    return entity;
            }

            return null;
        }

        /// <summary>
        /// 将内存对象保存到文件
        /// </summary>
        public void SaveDom2File()
        {
            this._dom.Save(this._scheduleFileName);
        }

        /// <summary>
        /// 获取到达时间点的Job
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<ScheduleEntity> GetArriveJob(DateTime time)
        {
            List<ScheduleEntity> scheduleList = new List<ScheduleEntity>();

            if (this.IsLoding == true)
            {
                // 写日志
                LogManager.Current.WriteCommonLog("", "目前正在加载Schedule，不能检查到达时间Job。", "");
                return scheduleList;
            }

            ScheduleEntity entity = null;

            try
            {
                for (int i = 0; i < this.Count; i++)
                {
                    entity = this[i];

                    if (entity.IsRun(time) == true)
                    {
                        scheduleList.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                string jobCode = "";
                if (entity != null)
                    jobCode = entity.JobCode;

                LogManager.Current.WriteExceptionLog("", "检查'" + jobCode + "'是否到达时间异常", ex, "");
            }

            // 排序
            //scheduleList.Sort(new ComparerClass());
            return scheduleList;
        }

        /// <summary>
        /// 新增一个Schedule
        /// </summary>
        public void NewSchedule(ScheduleEntity scheduleEntity)
        {
            // 给dom中新增一个Schedule节点
            XmlNode node = this._dom.CreateElement("Schedule");
            node.InnerXml = scheduleEntity.GetInnerXml();
            this._dom.DocumentElement.AppendChild(node);

            // 保存xml文件
            this.SaveDom2File();

            // 加到内存
            this.Add(scheduleEntity);
        }

        /// <summary>
        /// 编辑Schedule
        /// </summary>
        public void EditSchedule(ScheduleEntity scheduleEntity)
        {
            // 从dom中找到对应的节点，修改内容
            XmlNode node = this._dom.DocumentElement.SelectSingleNode("Schedule[ScheduleID='" + scheduleEntity.ScheduleID + "']");
            node.InnerXml = scheduleEntity.GetInnerXml();

            // 保存xml文件
            this.SaveDom2File();

            this.Remove(scheduleEntity);

            this.Add(scheduleEntity);

        }

        /// <summary>
        /// 删除Schedule
        /// </summary>
        public void DeleteSchedule(ScheduleEntity scheduleEntity)
        {
            // 从内存中删除
            this.Remove(scheduleEntity);

            // 从dom找到对应的节点，删除
            XmlNode node = this._dom.DocumentElement.SelectSingleNode("Schedule[ScheduleID='" + scheduleEntity.ScheduleID + "']");
            node.ParentNode.RemoveChild(node);

            // 保存xml文件
            this.SaveDom2File();
        }

        /// <summary>
        /// 得到上伟
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            return this._dom.OuterXml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleSimpleXml"></param>
        /// <returns></returns>
        public bool ChangeSchedule(string xml)
        {
            // 先清空内存
            this.Clear();

            // 加载到dom
            this._dom.LoadXml(xml);
            this.SaveDom2File();

            // 重新加载到内存
            this.LoadSchedule();

            // 触发事件
            if (this.ScheduleChanged != null)
            {
                this.ScheduleChanged(this, new EventArgs());
            }

            return true;
        }


    }
}
