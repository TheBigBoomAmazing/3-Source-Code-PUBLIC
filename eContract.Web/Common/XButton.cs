using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eContract.Web
{
    public enum XButtonType
    {
        Submit = 0,
        Button = 1,
        Back = 2
    }
    public class XBtnEvent
    {
        public string EventName { get; set; }
        public string Function { get; set; }
        public XBtnEvent()
        {
 
        }
        public XBtnEvent(string eventName, string funtion)
        {
            EventName = eventName;
            Function = funtion;
        }
    }
    public class XButton
    {
        public string DisplayName = "";
        public XButtonType xButtonType = XButtonType.Submit;
        public string LoadingText = "Loading..";
        public string Id = "";
        public string Icon = "";
        public string ControlName = "";
        public string Action = "";
        public bool disabled = true;
        public bool hide = false;
        public string onClick = "";
        public Array ListEvent;
        public XButton()
        { }
        public XButton(string id):this()
        {
            Id = id;
        }
        public XButton(string id, string displayName)
            : this(id)
        {
            DisplayName = displayName;
        }
        public XButton(string id, string displayName, Array listEvent)
            : this(id, displayName)
        {
            ListEvent = listEvent;
        }
        public XButton(string id, string displayName, Array listEvent, XButtonType xbuutontype)
            : this(id, displayName, listEvent)
        {
            xButtonType = xbuutontype;
        }
        public XButton(string id, string displayName, Array listEvent, XButtonType xbuutontype, string icon)
            : this(id, displayName, listEvent, xbuutontype)
        {
            Icon = icon;
        }
        public XButton(string id, string displayName, Array listEvent, XButtonType xbuutontype, string icon, string action)
            : this(id, displayName, listEvent, xbuutontype, icon)
        {
            Action = action;
        }
        public XButton(string id, string displayName, Array listEvent, XButtonType xbuutontype, string icon, string action, string loadingText)
            : this(id, displayName, listEvent, xbuutontype, icon, action)
        {
            LoadingText = loadingText;
        }
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <returns></returns>
        public bool IsFunction()
        {
            if (string.IsNullOrEmpty(this.ControlName) || string.IsNullOrEmpty(this.Action))
            {
                return true;
            }
            return true;
         // return WebCommonHelper.PageFunctionDisplay(this.ControlName, this.Action);
        }
        
    }

    /// <summary>
    /// 列表按钮
    /// </summary>
    public class XGridButton
    {
        /// <summary>
        /// 控件Id
        /// </summary>
        public string Name { get; set; }
        public string BtnName { get; set; }
        public string GridName { get; set; }
        public string DeleteUrl { get; set; }
        public string Icon { get; set; }
        public string onclick { get; set; }
        public string Url { get; set; }
        public string ControlName { get; set; }
        public string Action { get; set; }
        public bool Translate { get; set; }
        public bool Hide { get; set; }
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <returns></returns>
        public bool IsFunction()
        {
            if (string.IsNullOrEmpty(this.ControlName) || string.IsNullOrEmpty(this.Action))
            {
                return true;
            }
            return true;
           // return WebCommonHelper.PageFunctionDisplay(this.ControlName, this.Action);
        }
        
    }
}