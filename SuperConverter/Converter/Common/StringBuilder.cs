/*********************************************************************
 * Copyright(c) YaMoStudio All Rights Reserved.
 * 开发者：YaMoStudio
 * 命名空间：SuperConverter.Converter.Common
 * 文件名：StringBuilders
 * 版本号：V1.0.0.0
 * 创建时间：2024/4/26 11:16:21
 ******************************************************/

using System;

namespace SuperConverter.Converter.Common
{
    public class StringBuilders
    {
        /// <summary>
        /// 字符串转数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataStr"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static T[] StrToArr<T>(string dataStr, Func<string, T> converter)
        {
            try
            {
                var datas = dataStr.Split(',');
                var length = datas.Length;
                var tDatas = new T[length];
                for (int i = 0; i < length; i++)
                {
                    tDatas[i] = converter(datas[i]);
                }
                return tDatas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
