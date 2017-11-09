using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common.ExceptionMessages
{
    public class BizException : Exception
    {
        private string message;
        private string systemName;
        private string description;
        private string code;

        public BizException(string message)
        {
            this.message = message;
        }

        public BizException(string message, string systemName)
        {
            this.message = message;
            this.systemName = systemName;            
        }

        public BizException(string message, string systemName, string description)
        {
            this.message = message;
            this.systemName = systemName;
            this.description = description;
        }

        public BizException(string message, string systemName, string description, string code)
        {
            this.message = message;
            this.systemName = systemName;
            this.description = description;
            this.code = code;
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }

        public string SystemName
        {
            get
            {
                return systemName;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
        }
    }
}
