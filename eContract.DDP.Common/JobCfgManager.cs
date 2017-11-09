using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using eContract.DDP.Common;
using System.Collections;

namespace eContract.DDP.Common
{
    /// <summary>
    /// Job配置类
    /// </summary>
    public class JobCfgManager : List<JobEntity>
    {
        /// <summary>
        /// 应用程序目标
        /// </summary>
        public string ResourceDir = "";

        /// <summary>
        /// 任务文件名
        /// </summary>
        private string _jobFileName = "";

        /// <summary>
        /// 任务Xmldocument
        /// </summary>
        public XmlDocument Dom = null;

        /// <summary>
        /// 小节
        /// </summary>
        private List<Section> _sectionList = new List<Section>();

        /// <summary>
        /// 当前任务
        /// </summary>
        public JobEntity CurrentEntity = null;

        /// <summary>
        /// 任务中用户定义信息文件
        /// </summary>
        public string JobUserInfoFile = "";
        public XmlDocument JobUserInfoDom = null;

        /// <summary>
        /// 时间表管理对象
        /// </summary>
        public ScheduleManager ScheduleManager = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public JobCfgManager(string resourceDir, ScheduleManager scheduleManager)
        {
            this.ScheduleManager = scheduleManager;

            this.ResourceDir = resourceDir;
            this._jobFileName = resourceDir + "\\" + "Jobs.xml";

            this.LoadJob();
        }

        /// <summary>
        /// 加载任务
        /// </summary>
        public void LoadJob()
        {
            this.Clear();
            this.CurrentEntity = null;

            // 将配置文件加载到dom，如果不存在，新创建文件
            this.Dom = new XmlDocument();
            if (File.Exists(this._jobFileName) == false)
            {
                this.Dom.LoadXml("<root/>");
                this.SaveDom2File();
            }
            else
            {
                this.Dom.Load(this._jobFileName);
            }

            XmlNode nodeRoot = this.Dom.DocumentElement;

            // section
            XmlNodeList sectionList = nodeRoot.SelectNodes("Sections/Section");
            for (int i = 0; i < sectionList.Count; i++)
            {
                XmlNode sectionNode = sectionList[i];
                Section section = new Section();
                section.Name = XmlUtil.GetAttrValue(sectionNode, "Name").Trim();
                section.Text = XmlUtil.GetNodeText(sectionNode).Trim();
                this._sectionList.Add(section);
            }

            // 替换引用section的地方
            XmlNodeList refSectionNodeList = nodeRoot.SelectNodes("//*[@RefSection]");
            for (int j = 0; j < refSectionNodeList.Count; j++)
            {
                XmlNode refSectionNode = refSectionNodeList[j];
                string sectionName = XmlUtil.GetAttrValue(refSectionNode, "RefSection").Trim();
                refSectionNode.InnerText = this.GetSectionText(sectionName);
            }


            // 解析出jobs
            XmlNodeList listJob = nodeRoot.SelectNodes("Job");
            for (int i = 0; i < listJob.Count; i++)
            {
                XmlNode nodeJob = listJob[i];

                JobEntity entity = new JobEntity(this, nodeJob.OuterXml);
                this.Add(entity);
                if (entity.Active)
                {
                    CurrentEntity = entity;
                }

            }
        }


        /// <summary>
        /// 得到Section内容
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public string GetSectionText(string sectionName)
        {
            for (int i = 0; i < this._sectionList.Count; i++)
            {
                Section section = this._sectionList[i];
                if (section.Name == sectionName)
                    return section.Text;
            }
            return "";
        }

        /// <summary>
        /// 根据Code找到对应的Job
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public JobEntity GetJobByCode(string jobCode)
        {
            for (int i = 0; i < this.Count; i++)
            {
                JobEntity entity = this[i];
                if (entity.Code == jobCode)
                    return entity;
            }

            return null;
        }


        /// <summary>
        /// 修改Job
        /// </summary>
        /// <param name="jobEntity"></param>
        public JobEntity EditJob(JobEntity jobEntity, string jobXml, Hashtable userParams, bool needEditJob)
        {
            JobEntity returnEntity = jobEntity;

            if (needEditJob == true)
            {
                // // 保存xml文件
                XmlNode node = this.Dom.DocumentElement.SelectSingleNode("Job[Property/@Code='" + jobEntity.Code + "']");
                this.Dom.DocumentElement.RemoveChild(node);
                this.Dom.DocumentElement.InnerXml += jobXml;
                this.SaveDom2File();

                // 更新内存
                JobEntity newEntity = new JobEntity(this, jobXml);
                this.Insert(this.IndexOf(jobEntity), newEntity);
                this.Remove(jobEntity);

                returnEntity = newEntity;
            }

            return returnEntity;
        }


        /// <summary>
        /// 将内存对象保存到文件
        /// </summary>
        public void SaveDom2File()
        {
            this.Dom.Save(this._jobFileName);
        }

        /// <summary>
        /// 设为当前任务
        /// </summary>
        /// <param name="jobCode"></param>
        public bool ChangeUserParameter(JobEntity entity, string userParameterXml)
        {
            // 更新内存
            XmlNode node = this.Dom.DocumentElement.SelectSingleNode("Job[Property/@Code='" + entity.Code + "']");
            this.Dom.DocumentElement.RemoveChild(node);
            this.Dom.DocumentElement.InnerXml += userParameterXml;
            this.SaveDom2File();

            JobEntity newEntity = new JobEntity(this, userParameterXml);
            this.Insert(this.IndexOf(entity), newEntity);
            this.Remove(entity);

            return true;
        }

        /// <summary>
        /// 得到用户参数，用于上传到服务器
        /// </summary>
        /// <returns></returns>
        public string GetJobParamsXml()
        {
            return this.Dom.OuterXml;
        }

    }


    public class Section
    {
        public string Name = "";
        public string Text = "";
    }
}
