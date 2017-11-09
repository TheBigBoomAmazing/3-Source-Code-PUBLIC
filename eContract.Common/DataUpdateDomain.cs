using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public class DataUpdateDomain
    {
        private DataUpdateHeader _msgHeader = new DataUpdateHeader();
        public DataUpdateHeader MsgHeader
        {
            get { return _msgHeader; }
            set { _msgHeader = value; }
        }

        private DataUpdateBody _msgBody = new DataUpdateBody();
        public DataUpdateBody MsgBody
        {
            get { return _msgBody; }
            set { _msgBody = value; }
        }
    }
}
