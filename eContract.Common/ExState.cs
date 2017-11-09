using System;
using System.Data;

namespace eContract.Common {

    #region ExState

    /// <summary>
    /// ExState
    /// manoen
    /// 2015.10.04 08:51
    /// </summary>
    public class ExState {

        private string m_Flag = "1";
        private string m_Message = string.Empty;
        private string m_MoreMessage = string.Empty;
        private int m_SuccessNum = 0;
        private int m_ErrorNum = 0;
        private string m_SuccessMsg = string.Empty;
        private string m_ErrorMsg = string.Empty;
        private string m_MsgId = string.Empty;

        private string m_Data = string.Empty;
        private string m_DataOther = string.Empty;
        private DataTable m_DataTable = null;
        private Object m_DataObject;

        public ExState() {
        }

        /// <summary> 
        /// Flag
        /// </summary>
        public string Flag {
            get { return m_Flag; }
            set {
                if (String.IsNullOrEmpty(value)) {
                    m_Flag = "1";
                } else {
                    m_Flag = value;
                }
            }
        }

        /// <summary> 
        /// Message
        /// </summary>
        public string Message {
            get { return m_Message; }
            set { m_Message = value; }
        }

        /// <summary> 
        /// SuccessNum
        /// </summary>
        public int SuccessNum {
            get { return m_SuccessNum; }
            set { m_SuccessNum = value; }
        }

        /// <summary> 
        /// ErrorNum
        /// </summary>
        public int ErrorNum {
            get { return m_ErrorNum; }
            set { m_ErrorNum = value; }
        }

        /// <summary> 
        /// SuccessMsg
        /// </summary>
        public string SuccessMsg {
            get { return m_SuccessMsg; }
            set { m_SuccessMsg = value; }
        }

        /// <summary> 
        /// ErrorMsg
        /// </summary>
        public string ErrorMsg {
            get { return m_ErrorMsg; }
            set { m_ErrorMsg = value; }
        }

        /// <summary> 
        /// MoreMessage
        /// </summary>
        public string MoreMessage {
            get { return m_MoreMessage; }
            set { m_MoreMessage = value; }
        }

        /// <summary> 
        /// MsgId
        /// </summary>
        public string MsgId {
            get { return m_MsgId; }
            set { m_MsgId = value; }
        }

        /// <summary> 
        /// Data
        /// </summary>
        public string Data {
            get { return m_Data; }
            set { m_Data = value; }
        }

        /// <summary> 
        /// DataOther
        /// </summary>
        public string DataOther {
            get { return m_DataOther; }
            set { m_DataOther = value; }
        }

        #region 对象定义

        /// <summary>
        /// 页码
        /// </summary>
        public int m_PageNo;
        public int PageNo {
            get { return m_PageNo; }
            set { m_PageNo = value; }
        }

        /// <summary>
        /// 每页多少行
        /// </summary>
        public int m_PageSize;
        public int PageSize {
            get { return m_PageSize; }
            set { m_PageSize = value; }
        }

        /// <summary>
        /// 共有多少页
        /// </summary>
        public int m_TotalPage;
        public int TotalPage {
            get { return m_TotalPage; }
            set { m_TotalPage = value; }
        }

        #endregion

        /// <summary> 
        /// DataTable
        /// </summary>
        public DataTable DataTable {
            get { return m_DataTable; }
            set { m_DataTable = value; }
        }

        /// <summary>
        /// DataObject
        /// manoen + 
        /// 2015.10.04 08:50
        /// </summary>
        public Object DataObject {
            get { return m_DataObject; }
            set { m_DataObject = value; }
        }

        /// <summary>
        /// 错误信息Exception
        /// </summary>
        private Exception m_Exception;
        public Exception Exception {
            get { return m_Exception; }
            set { m_Exception = value; }
        }

    }

    #endregion

    #region ExState<T>

    /// <summary>
    /// ExState<T>
    /// manoen
    /// </summary>
    public class ExState<T> : ExState {

        public ExState() {
        }

        /// <summary>
        /// 返回值ResultData
        /// </summary>
        public T ResultData;

    }

    #endregion

}