using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using eContract.DDP.Common.CommonJob;

namespace eContract.DDP.Common
{
    public class ScheduleEntity
    {
        /// <summary>
        /// Schedule Type常量
        /// </summary>
        public const string SCHEDULE_TYPE_EVERY_DAY = "EveryDay";
        public const string SCHEDULE_TYPE_EVERY_WEEK = "EveryWeek";
        public const string SCHEDULE_TYPE_EVERY_MONTH = "EveryMonth";
        public const string SCHEDULE_TYPE_Interval = "Interval";

        public string ScheduleID;
        public string JobCode;
        public string ScheduleType;
        public string StartTime;            // 开始时间，格式为 "小时:分钟"
        public int EveryWeekDay = 0;  // 每周星期几 1-7
        public int EveryMonthDay = 0; // 每月几号   1-31
        public int IntervalMinutes = 0; //间隔时间

        /// 开始时间中的几点
        /// </summary>
        public int _startHour = 0;

        /// <summary>
        /// 开始时间中的几分
        /// </summary>
        public int _startMinute = 0;

        /// <summary>
        /// 上次运行时间
        /// </summary>
        public DateTime _lastRunTime = new DateTime();

        /// <summary>
        /// 结束时间，格式为 "小时:分钟"
        /// </summary>
        public string EndTime = "";

        /// <summary>
        /// 结束时间中的几点
        /// </summary>
        public int _endHour = 0;

        /// <summary>
        /// 结束时间中的几分
        /// </summary>
        public int _endMinute = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleEntity()
        { }

        /*
	<Schedule ID="s1">
		<JobCode>j1</JobCode>
		<ScheduleType>EveryDay</ScheduleType>
		<StartTime>23:00</StartTime>
		<EveryWeekDay></EveryWeekDay>
		<EveryMonthDay></EveryMonthDay>	
        <SortNo>0</SortNo>
        <IntervalMinutes>0</IntervalMinutes>
	</Schedule>
         */
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jobNode"></param>
        public ScheduleEntity(XmlNode node)
        {
            //this.ScheduleID = XmlUtil.GetAttrValue(node, "ID").Trim();

            XmlNode nodeScheduleID = node.SelectSingleNode("ScheduleID");
            if (nodeScheduleID == null)
                throw new Exception("未定义<ScheduleID>节点");
            this.ScheduleID = nodeScheduleID.InnerText.Trim();

            XmlNode nodeJobCode = node.SelectSingleNode("JobCode");
            if (nodeJobCode == null)
                throw new Exception("ID为'" + this.ScheduleID + "'的Schedule未定义<JobCode>节点");
            this.JobCode = nodeJobCode.InnerText.Trim();


            XmlNode nodeScheduleType = node.SelectSingleNode("ScheduleType");
            if (nodeScheduleType == null)
                throw new Exception("ID为'" + this.ScheduleID + "'的Schedule未定义<ScheduleType>节点");
            this.ScheduleType = nodeScheduleType.InnerText.Trim();

            XmlNode nodeStartTime = node.SelectSingleNode("StartTime");

            if (nodeStartTime == null)
                throw new Exception("ID为'" + this.ScheduleID + "'的Schedule未定义<StartTime>节点");
            this.StartTime = nodeStartTime.InnerText.Trim();
            if (this.StartTime == "" && this.ScheduleType != SCHEDULE_TYPE_Interval)
            {
                throw new Exception("尚未定义开始时间");
            }
            if (this.StartTime != "")
            {
                try
                {
                    this._startHour = Convert.ToInt32(this.StartTime.Substring(0, 2));
                    this._startMinute = Convert.ToInt32(this.StartTime.Substring(3, 2));
                }
                catch (Exception ex)
                {
                    throw new Exception("Convert start time to Int32 error:" + ex.Message);
                }
            }

            XmlNode nodeEndTime = node.SelectSingleNode("EndTime");
            if (nodeEndTime != null)
            {
                this.EndTime = nodeEndTime.InnerText.Trim();
                if (this.EndTime != "")
                {
                    try
                    {
                        this._endHour = Convert.ToInt32(this.EndTime.Substring(0, 2));
                        this._endMinute = Convert.ToInt32(this.EndTime.Substring(3, 2));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Convert end time to Int32 error:" + ex.Message);
                    }
                }
            }


            XmlNode nodeEveryWeekDay = node.SelectSingleNode("EveryWeekDay");
            if (nodeEveryWeekDay != null)
            {
                string everyWeekDayString = nodeEveryWeekDay.InnerText.Trim();
                if (everyWeekDayString != "")
                    this.EveryWeekDay = Convert.ToInt32(everyWeekDayString);
            }

            XmlNode nodeEveryMonthDay = node.SelectSingleNode("EveryMonthDay");
            if (nodeEveryMonthDay != null)
            {
                string everyMonthDayString = nodeEveryMonthDay.InnerText.Trim();
                if (everyMonthDayString != "")
                    this.EveryMonthDay = Convert.ToInt32(everyMonthDayString);
            }

            XmlNode nodeIntervalMinutes = node.SelectSingleNode("IntervalMinutes");
            if (nodeIntervalMinutes != null)
            {
                this.IntervalMinutes = Convert.ToInt32(nodeIntervalMinutes.InnerText.Trim());
            }

            // 间隔JOB
            if (this.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_Interval)
            {
                // 把上次执行时间设为当前时间
                this._lastRunTime = DateTime.Now;
                // 写日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "设上次运行时间为'" + this._lastRunTime.ToString() + "'");
            }
        }


        #region 检查是否到达运行时间

        /// <summary>
        /// 检查是否到达运行时间点
        /// </summary>
        /// <returns></returns>
        public bool IsRun(DateTime time)
        {
            if (this.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_EVERY_DAY)
            {
                // 每天
                return this.CheckEveryDay(time);
            }
            else if (this.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_EVERY_WEEK)
            {
                // 每周
                return this.CheckEveryWeek(time);
            }
            else if (this.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_EVERY_MONTH)
            {
                // 每月
                return this.CheckEveryMonth(time);
            }
            else if (this.ScheduleType == ScheduleEntity.SCHEDULE_TYPE_Interval)
            {
                // 间隔时间
                return this.CheckInterval(time);
            }

            return false;
        }

        /// <summary>
        /// 检查时间点为每天的情况
        /// </summary>
        /// <returns></returns>
        public bool CheckEveryDay(DateTime time)
        {
            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的IsRun()开始，传入的时间为'" + time.ToString() + "'");

            DateTime runTime = new DateTime(time.Year,
                time.Month,
                time.Day,
                this._startHour,
                this._startMinute,
                0);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的运行时间为'" + runTime.ToString() + "'。");


            DateTime curTime = time;
            DateTime nextTime = time.AddMilliseconds(DDPConst.TIMER_INTERVAL);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，当前时间为'" + curTime.ToString() + "'，下次检查时间为'" + nextTime.ToString() + "'。");


            // 必须是大于等于当前时间，小于下一次时间，
            // 例如  8.59<=9:00<9:00 
            //           9:00<=9:00<9:01
            if (runTime.CompareTo(curTime) >= 0
                && runTime.CompareTo(nextTime) < 0)
            {
                //LogManager.Current.WriteCommonLog(this.JobCode,"到达运行时间点123。");
                return true;
            }

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，未到时间点，IsRun()结束。");


            return false;
        }

        /// <summary>
        /// 检查时间点为每周的情况
        /// </summary>
        /// <returns></returns>
        public bool CheckEveryWeek(DateTime time)
        {
            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的IsRun()开始，传入的时间为'" + time.ToString() + "'");

            //~~~~~~~~~~~~~~
            // 先检查星期
            int curDayOfWeek = (int)time.DayOfWeek;
            if (curDayOfWeek == 0)
                curDayOfWeek = 7;

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule,传入的星期为'" + curDayOfWeek.ToString() + "'。运行的星期为'" + this.EveryWeekDay.ToString()+ "'");

            if (curDayOfWeek != this.EveryWeekDay)
            {
                // 日志
                //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule,星期不一致，未到时间点，IsRun()结束。");

                return false;
            }

            //~~~~~~~~~~~~~~
            // 检查时间
            DateTime runTime = new DateTime(time.Year,
                time.Month,
                time.Day,
                this._startHour,
                this._startMinute,
                0);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的运行时间为'" + runTime.ToString() + "'。");


            DateTime curTime = time;
            DateTime nextTime = time.AddMilliseconds(DDPConst.TIMER_INTERVAL);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，当前时间为'" + curTime.ToString() + "'，下次检查时间为'" + nextTime.ToString() + "'。");


            // 必须是大于等于当前时间，小于下一次时间，
            // 例如  8.59<=9:00<9:00 
            //           9:00<=9:00<9:01
            if (runTime.CompareTo(curTime) >= 0
                && runTime.CompareTo(nextTime) < 0)
            {
                //LogManager.Current.WriteCommonLog(this.JobCode, "到达运行时间点。");
                return true;
            }

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，未到时间点，IsRun()结束。");


            return false;
        }


        /// <summary>
        /// 检查时间点为每月的情况
        /// </summary>
        /// <returns></returns>
        public bool CheckEveryMonth(DateTime time)
        {
            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的IsRun()开始，传入的时间为'" + time.ToString() + "'");

            //~~~~~~~~~~~~~~
            // 检查时间
            DateTime runTime = new DateTime(time.Year,
                time.Month,
                this.EveryMonthDay,
                this._startHour,
                this._startMinute,
                0);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule的运行时间为'" + runTime.ToString() + "'。");


            DateTime curTime = time;
            DateTime nextTime = time.AddMilliseconds(DDPConst.TIMER_INTERVAL);

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，当前时间为'" + curTime.ToString() + "'，下次检查时间为'" + nextTime.ToString() + "'。");


            // 必须是大于等于当前时间，小于下一次时间，
            // 例如  8.59<=9:00<9:00 
            //           9:00<=9:00<9:01
            if (runTime.CompareTo(curTime) >= 0
                && runTime.CompareTo(nextTime) < 0)
            {
                //LogManager.Current.WriteCommonLog(this.JobCode, "到达运行时间点。");
                return true;
            }

            // 日志
            //FileLogManager.WriteLog("'" + this.JobCode + "'Schedule，未到时间点，IsRun()结束。");
            return false;
        }

        /// <summary>
        /// 检查时间点为间隔的情况
        /// </summary>
        /// <returns></returns>
        public bool CheckInterval(DateTime time)
        {
            //// 写日志
            //LogManager.Current.WriteCommonLog(this.JobCode, "1.CheckInterval()开始，传入的时间为'" + time.ToString() + "'");

            //~~~~~~~~~~08/07/07加时间段~~~~

            // 开始时间,默认为0:0:0
            DateTime startTime = new DateTime(time.Year,
                time.Month,
                time.Day,
                0,
                0,
                0);
            if (this.StartTime != "")
            {
                startTime = new DateTime(time.Year,
                               time.Month,
                               time.Day,
                               this._startHour,
                               this._startMinute,
                               0);
            }

            // 结束时间:默认为23:59:59
            DateTime endTime = new DateTime(time.Year,
                time.Month,
                time.Day,
                23,
                59,
                59);
            if (this.EndTime != "")
            {
                endTime = new DateTime(time.Year,
                time.Month,
                time.Day,
                this._endHour,
                this._endMinute,
                0);
            }

            //// 写日志
            //LogManager.Current.WriteCommonLog(this.JobCode, "批次轮循时间段为'" + StartTime.ToString() + "'--'" + EndTime.ToString() + "'");

            // 如果当前时间在开始时间和结束之间段之外，则不运行
            if (startTime.CompareTo(time) > 0 || endTime.CompareTo(time) < 0)
            {
                //// 写日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "当前时间'" + time.ToString() + "'在轮循时间段之外。");

                //// 写日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "未到时间点,IsRun()结束。");
                return false;
            }

            // 如果当前时间与循环开始时间相等，则运行
            if (this.StartTime != "" && startTime.CompareTo(time) == 0)
            {
                //// 写日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "当前时间'" + time.ToString() + "'正好为循环时间段开始时间。");

                //// 日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "批次到达运行时间点");

                // 设最后运行时间
                this._lastRunTime = startTime;
                return true;
            }
            //~~~~~~~~~~08/07/07加时间段~~~~



            // 上次运行时间加上间隔时间，来与当前时间比较
            DateTime runTime = this._lastRunTime.AddMinutes(this.IntervalMinutes);
            //// 写日志
            //LogManager.Current.WriteCommonLog(this.JobCode, "2.上次执行时间为'" + this._lastRunTime.ToString() + "'，本次执行时间应该为'" + runTime.ToString() + "'。");


            DateTime curTime = time;
            DateTime nextTime = time.AddMilliseconds(DDPConst.TIMER_INTERVAL);

            // 检查时间小于本次应该执行的时间，说明中途办事耽误了时间，应该及时补上，所以到达时间点
            if (runTime.CompareTo(curTime) < 0)
            {
                //// 写日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "3.当前时间'" + curTime.ToString() + "'小于应该运行时间。到达运行时间点");

                // 设最后运行时间
                this._lastRunTime = runTime;

                return true;
            }

            //// 写日志
            //LogManager.Current.WriteCommonLog(this.JobCode, "4.当前时间为'" + curTime.ToString() + "'，下次检查时间为'" + nextTime.ToString() + "'。");

            // 必须是大于等于当前时间，小于下一次时间，
            // 例如  8.59<=9:00<9:00 
            //           9:00<=9:00<9:01
            if (runTime.CompareTo(curTime) >= 0
                && runTime.CompareTo(nextTime) < 0)
            {
                //// 日志
                //LogManager.Current.WriteCommonLog(this.JobCode, "5.下次检查>运行时间>=当前时间,到达运行时间点");

                // 设最后运行时间
                this._lastRunTime = runTime;

                return true;
            }

            //// 写日志
            //LogManager.Current.WriteCommonLog(this.JobCode, "6.未到时间点,IsRun()结束。");

            return false;
        }


        #endregion


        /*
	<Schedule>
		<ScheduleID>s1</ScheduleID>
		<JobCode>job_province</JobCode>
		<ScheduleType>EveryDay</ScheduleType>
		<StartTime>04:00</StartTime>
		<EveryWeekDay></EveryWeekDay>
		<EveryMonthDay></EveryMonthDay>
		<SortNo>1</SortNo>
	</Schedule>
 */
        /// <summary>
        /// 得到对应的InnerXml
        /// </summary>
        /// <returns></returns>
        public string GetInnerXml()
        {
            return "<ScheduleID>" + this.ScheduleID + "</ScheduleID>"
               + "<JobCode>" + this.JobCode + "</JobCode>"
               + "<ScheduleType>" + this.ScheduleType + "</ScheduleType>"
               + "<StartTime>" + this.StartTime + "</StartTime>"
               + "<EndTime>" + this.EndTime + "</EndTime>"
               + "<EveryWeekDay>" + this.EveryWeekDay.ToString() + "</EveryWeekDay>"
               + "<EveryMonthDay>" + this.EveryMonthDay.ToString() + "</EveryMonthDay>"
               + "<IntervalMinutes>" + this.IntervalMinutes.ToString() + "</IntervalMinutes>";
        }
    }
}
