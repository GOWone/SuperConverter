/*********************************************************************
 * Copyright(c) YaMoStudio All Rights Reserved.
 * 开发者：YaMoStudio
 * 命名空间：SuperConverter.Converter.DataConverter
 * 文件名：DataTableBuilders
 * 版本号：V1.0.0.0
 * 创建时间：2024/4/26 11:16:21
 ******************************************************/
using SuperConverter.Converter.Common;
using System.ComponentModel;
using System.Data;

namespace SuperConverter.Converter.DataConverter
{
    public class DataTableBuilders
    {
        /// <summary>
        /// DataTable转数据模型列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> Dt2ModelList<T>(DataTable dt) where T : new()
        {
            var list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Dr2Model<T>(dr));
            }
            return list;
        }

        /// <summary>
        /// DataRow转数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T Dr2Model<T>(DataRow dr) where T : new()
        {
            T t = new T();
            var props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor p in props)
            {
                if (dr.Table.Columns.Contains(p.Name))
                {
                    if (p.IsReadOnly) { continue; }

                    SetValue<T>(t, p, dr[p.Name]);
                }
            }
            return t;
        }

        /// <summary>
        /// 设置类型对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="p"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool SetValue<T>(T obj, PropertyDescriptor p, object value)
        {
            // handle DBNull value
            if (value == DBNull.Value)
            {
                if (p.PropertyType.IsPrimitive && p.PropertyType.IsEnum)
                {
                    return false;
                }
                value = null;
            }
            object boolvalue = null;

            if (!(value is null))
            {
                var vType = value.GetType();

                if (p.PropertyType != vType && !p.PropertyType.IsEnum)
                {
                    // handle integer
                    if (vType == typeof(int) || vType == typeof(long))
                    {
                        try
                        {
                            if (p.PropertyType == typeof(int))
                            {
                                value = Convert.ToInt32(value);
                            }
                            else if (p.PropertyType == typeof(uint))
                            {
                                value = Convert.ToUInt32(value);
                            }
                            else if (p.PropertyType == typeof(long))
                            {
                                value = Convert.ToInt64(value);
                            }
                            else if (p.PropertyType == typeof(bool))
                            {
                                boolvalue = Convert.ToInt32(value) == 1;
                            }
                            else if (p.PropertyType == typeof(int?))
                            {
                                value = Convert.ToInt32(value);
                            }
                            else if (p.PropertyType == typeof(double))
                            {
                                value = Convert.ToInt32(value);
                            }
                            else if (p.PropertyType == typeof(string))
                            {
                                value = Convert.ToInt64(value);
                            }
                            else
                            {
#if DEBUG
                                Console.WriteLine($"type:{p.PropertyType} vtype:{vType}");
#endif
                            }
                        }
                        catch
                        {
                            return false;
                        }
                    }

                    if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(obj, value.ToString());
                        return true;
                    }

                    if (p.PropertyType == typeof(int[]))
                    {
                        string[] vals = (value as string).Split(',');
                        int[] arry = new int[vals.Length];
                        for (int i = 0; i < vals.Length; i++)
                        {
                            int.TryParse(vals[i], out int result);
                            arry[i] = result;
                        }
                        p.SetValue(obj, arry);
                        return true;
                    }
                    
                    if (p.PropertyType == typeof(double[]))
                    {
                        double[] arry = StringBuilders.StrToArr<double>((value as string), Convert.ToDouble);
                        p.SetValue(obj, arry);
                        return true;
                    }
                }
            }

            try
            {
                if (boolvalue != null)
                {
                    p.SetValue(obj, boolvalue);
                }
                else
                {
                    p.SetValue(obj, value);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
