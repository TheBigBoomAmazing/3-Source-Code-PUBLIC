using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace eContract.Common
{
    public class ConstHelper
    {
        public static Dictionary<string, string> GetEnumDescript(Type enumType)
        {
            Dictionary<string, string> listEnum = new Dictionary<string, string>();
            FieldInfo[] fieldInfos = enumType.GetFields();

            if (fieldInfos != null)
            {
                foreach (FieldInfo field in fieldInfos)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs != null && objs.Length > 0)
                    {
                        DescriptionAttribute da = (DescriptionAttribute)objs[0];
                        listEnum.Add(Enum.Parse(enumType, field.Name).GetHashCode().ToString(), da.Description);
                    }
                }
            }
            return listEnum;
        }

        public static Dictionary<string, string> GetEnumNameWithDescript(Type enumType)
        {
            Dictionary<string, string> listEnum = new Dictionary<string, string>();
            FieldInfo[] fieldInfos = enumType.GetFields();

            if (fieldInfos != null)
            {
                foreach (FieldInfo field in fieldInfos)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs != null && objs.Length > 0)
                    {
                        DescriptionAttribute da = (DescriptionAttribute)objs[0];
                        listEnum.Add(field.Name, da.Description);
                    }
                }
            }
            return listEnum;
        }

        public static Dictionary<string, string> GetEnumAndName(Type enumType)
        {
            Dictionary<string, string> listEnum = new Dictionary<string, string>();
            FieldInfo[] fieldInfos = enumType.GetFields();

            if (fieldInfos != null)
            {
                foreach (FieldInfo field in fieldInfos)
                {
                    if (field.Name != "value__")
                    {
                        listEnum.Add(Enum.Parse(enumType, field.Name).GetHashCode().ToString(), field.Name);
                    }
                }
            }
            return listEnum;
        }

        public static string GetEnumDescription(Type enumType, int enumvalue)
        {
            FieldInfo[] fieldinfos = enumType.GetFields();

            if (fieldinfos != null)
            {
                foreach (FieldInfo field in fieldinfos)
                {

                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs != null && objs.Length > 0)
                    {
                        DescriptionAttribute da = (DescriptionAttribute)objs[0];
                        if (Enum.Parse(enumType, field.Name).GetHashCode() == enumvalue)
                            return da.Description;
                    }
                }
            }
            return string.Empty; ;
        }
    }
}
