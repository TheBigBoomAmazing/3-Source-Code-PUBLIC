using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasAttachmentTable : TableInfo
    {
        public const string C_TableName = "CAS_ATTACHMENT";

        public const string C_ATTACHMENT_ID = "ATTACHMENT_ID";

        public const string C_FILE_NAME = "FILE_NAME";

        public const string C_FILE_SUFFIX = "FILE_SUFFIX";

        public const string C_FILE_PATH = "FILE_PATH";

        public const string C_PDF_FILE_PATH = "PDF_FILE_PATH";

        public const string C_CONVERTED = "CONVERTED";

        public const string C_ATTACHMENT_TYPE = "ATTACHMENT_TYPE";

        public const string C_SOURCE_ID = "SOURCE_ID";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasAttachmentTable()
        {
            _tableName = "CAS_ATTACHMENT";
        }

        protected static CasAttachmentTable _current;
        public static CasAttachmentTable Current
        {
            get
            {
                if (_current == null)
                {
                    Initial();
                }
                return _current;
            }
        }

        private static void Initial()
        {
            _current = new CasAttachmentTable();

            _current.Add(C_ATTACHMENT_ID, new ColumnInfo(C_ATTACHMENT_ID, "attachment_id", true, typeof(string)));

            _current.Add(C_FILE_NAME, new ColumnInfo(C_FILE_NAME, "file_name", false, typeof(string)));

            _current.Add(C_FILE_SUFFIX, new ColumnInfo(C_FILE_SUFFIX, "file_suffix", false, typeof(string)));

            _current.Add(C_FILE_PATH, new ColumnInfo(C_FILE_PATH, "file_path", false, typeof(string)));
            _current.Add(C_PDF_FILE_PATH, new ColumnInfo(C_PDF_FILE_PATH, "pdf_file_path", false, typeof(string)));

            _current.Add(C_CONVERTED, new ColumnInfo(C_CONVERTED, "converted", false, typeof(bool)));

            _current.Add(C_ATTACHMENT_TYPE, new ColumnInfo(C_ATTACHMENT_TYPE, "attachment_type", false, typeof(int)));

            _current.Add(C_SOURCE_ID, new ColumnInfo(C_SOURCE_ID, "source_id", false, typeof(string)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo ATTACHMENT_ID
        {
            get { return this[C_ATTACHMENT_ID]; }
        }

        public ColumnInfo FILE_NAME
        {
            get { return this[C_FILE_NAME]; }
        }

        public ColumnInfo FILE_SUFFIX
        {
            get { return this[C_FILE_SUFFIX]; }
        }

        public ColumnInfo FILE_PATH
        {
            get { return this[C_FILE_PATH]; }
        }
        public ColumnInfo PDF_FILE_PATH
        {
            get { return this[C_PDF_FILE_PATH]; }
        }

        public ColumnInfo CONVERTED
        {
            get { return this[C_CONVERTED]; }
        }

        public ColumnInfo ATTACHMENT_TYPE
        {
            get { return this[C_ATTACHMENT_TYPE]; }
        }

        public ColumnInfo SOURCE_ID
        {
            get { return this[C_SOURCE_ID]; }
        }

        public ColumnInfo IS_DELETED
        {
            get { return this[C_IS_DELETED]; }
        }

        public ColumnInfo CREATED_BY
        {
            get { return this[C_CREATED_BY]; }
        }

        public ColumnInfo CREATE_TIME
        {
            get { return this[C_CREATE_TIME]; }
        }

        public ColumnInfo LAST_MODIFIED_BY
        {
            get { return this[C_LAST_MODIFIED_BY]; }
        }

        public ColumnInfo LAST_MODIFIED_TIME
        {
            get { return this[C_LAST_MODIFIED_TIME]; }
        }

    }
}
