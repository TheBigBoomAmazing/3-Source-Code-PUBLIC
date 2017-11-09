using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasAttachmentEntity : EntityBase
    {
        public CasAttachmentTable TableSchema
        {
            get
            {
                return CasAttachmentTable.Current;
            }
        }


        public CasAttachmentEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasAttachmentTable.Current;
            }
        }
        #region 属性列表

        public string AttachmentId
        {
            get { return (string)GetData(CasAttachmentTable.C_ATTACHMENT_ID); }
            set { SetData(CasAttachmentTable.C_ATTACHMENT_ID, value); }
        }

        public string FileName
        {
            get { return (string)GetData(CasAttachmentTable.C_FILE_NAME); }
            set { SetData(CasAttachmentTable.C_FILE_NAME, value); }
        }

        public string FileSuffix
        {
            get { return (string)GetData(CasAttachmentTable.C_FILE_SUFFIX); }
            set { SetData(CasAttachmentTable.C_FILE_SUFFIX, value); }
        }

        public string FilePath
        {
            get { return (string)GetData(CasAttachmentTable.C_FILE_PATH); }
            set { SetData(CasAttachmentTable.C_FILE_PATH, value); }
        }

        public string PdfFilePath
        {
            get { return (string)GetData(CasAttachmentTable.C_PDF_FILE_PATH); }
            set { SetData(CasAttachmentTable.C_PDF_FILE_PATH, value); }
        }

        public bool? Converted
        {
            get { return (bool?)(GetData(CasAttachmentTable.C_CONVERTED)); }
            set { SetData(CasAttachmentTable.C_CONVERTED, value); }
        }

        public int? AttachmentType
        {
            get { return (int?)(GetData(CasAttachmentTable.C_ATTACHMENT_TYPE)); }
            set { SetData(CasAttachmentTable.C_ATTACHMENT_TYPE, value); }
        }

        public string SourceId
        {
            get { return (string)GetData(CasAttachmentTable.C_SOURCE_ID); }
            set { SetData(CasAttachmentTable.C_SOURCE_ID, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasAttachmentTable.C_IS_DELETED)); }
            set { SetData(CasAttachmentTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasAttachmentTable.C_CREATED_BY); }
            set { SetData(CasAttachmentTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasAttachmentTable.C_CREATE_TIME)); }
            set { SetData(CasAttachmentTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasAttachmentTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasAttachmentTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasAttachmentTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasAttachmentTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
