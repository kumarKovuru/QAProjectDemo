using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace emids.QA.Application.Common
{
    public static class ConverterHelper
    {
        public static string GetStringValue(Object value)
        {
            if (value != DBNull.Value && value != null)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToString(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }

                return Convert.ToString(value).Trim();
            }
            return string.Empty;
        }
        public static int ConvertIntColumnValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt32(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt32(value, CultureInfo.CurrentCulture);
            }
            return 0;
        }
        public static long ConvertLongColumnValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt64(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt64(value, CultureInfo.CurrentCulture);
            }
            return 0;
        }

        public static DateTime ConvertDateColumnValue(object value)
        {
            if (value != DBNull.Value && value != null && (ConverterHelper.GetStringValue(value).Length > 0) &&
                ConverterHelper.GetStringValue(value) != "__/__/____")
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToDateTime(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToDateTime(value, CultureInfo.CurrentCulture);
            }

            return DateTime.Now.Date;
        }
        public static bool? ConvertBitColumnValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToBoolean(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToBoolean(value, CultureInfo.CurrentCulture);
            }
            return false;
        }

        public static bool ConvertBoolColumnValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToBoolean(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToBoolean(value, CultureInfo.CurrentCulture);
            }
            return false;
        }
        public static int? ConvertIntColumn(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt32(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt32(value, CultureInfo.CurrentCulture);
            }
            return null;
        }
        public static short? ConvertShortValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt16(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt16(value, CultureInfo.CurrentCulture);
            }
            return null;
        }
        public static short ConvertShortColumnValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt16(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt16(value, CultureInfo.CurrentCulture);
            }
            return 0;
        }

        public static long? ConvertLongValue(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToInt64(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToInt64(value, CultureInfo.CurrentCulture);
            }
            return null;
        }

        public static float ConvertNumberToSingle(object value)
        {
            if (ConverterHelper.GetStringValue(value).Length > 0)
            {
                if (value.GetType().ToString().IndexOf("MySqlParameter") > 0)
                {
                    return Convert.ToSingle(((MySql.Data.MySqlClient.MySqlParameter)value).Value);
                }
                return Convert.ToSingle(value, CultureInfo.CurrentCulture);
            }
            return 0;
        }

    }
}
