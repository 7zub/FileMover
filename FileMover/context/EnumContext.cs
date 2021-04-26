using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace FileMover.context
{
    public static class EnumContext
    {
        public enum op
        {
            [Description("Переместить")] move,
            [Description("Копировать")] copy
        };
        public enum ifEx
        {
            [Description("Перезаписать")] rewrite,
            [Description("Переименовать")] rename,
            [Description("Игнорировать")] ignore
        };

        public static Dictionary<string, string> EnumToDic<T>()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                res.Add(value.ToString(), attributes.Length > 0 ? attributes[0].Description : value.ToString());
            }
            return res;
        }

        public class ControlRules
        {
            public enum value { add, edit }

            public enum ExecFW { enable, disable }
        }
    }
}
