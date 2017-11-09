using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public class EntitySort
    {
        //private bool isReverse = false;
        public static T[] Sort<T>(T[] list, string key)
        {
            int len = list.Length;
            Type type = typeof(T);
            object[] keys = new string[len];
            for (int i = 0; i < len; i++)
            {
                object keyobject = type.InvokeMember(key, System.Reflection.BindingFlags.GetProperty, null, list[i], null);
                if (keyobject != null)
                {
                    keys[i] = keyobject.ToString();
                }
                else
                {
                    keys[i] = string.Empty;
                }
            }
            Array.Sort(keys, list);
            return list;

        }

        public static IList<T> Sort<T>(IList<T> list, string key)
        {
            T[] tlist = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tlist[i] = list[i];
            }
            tlist = Sort<T>(tlist, key);
            IList<T> listt = new List<T>();
            for (int i = 0; i < tlist.Length; i++)
            {
                listt.Add(tlist[i]);
            }
            return listt;
        }
    }
}
