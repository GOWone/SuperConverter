/*********************************************************************
 * Copyright(c) YaMoStudio All Rights Reserved.
 * 开发者：YaMoStudio
 * 命名空间：SuperConverter.Test.Analysis
 * 文件名：Dt2ExcelConverterTest
 * 版本号：V1.0.0.0
 * 创建时间：2024/4/28 22:07:28
 ******************************************************/
using NUnit.Framework;
using SuperConverter.Converter.HybridConverter;
using SuperConverter.Parameters;
using SuperConverter.Test.SampleData;
using System.Data;

namespace SuperConverter.Test.Analysis
{
    public class DtExcelConverterTest
    {
        [Test]
        public static void Dt2Excel()
        {
            DataTable source = DataTableSample.SampleData();
            Dt2ExcelBuilder.Dt2Excel(source, ExcelMode.Xlsx, @"D:\excel", "test");
        }

        [Test]
        public static void Excel2Dt()
        {
            DataTable dt = Excel2DtBuilder.Excel2Dt(@"D:\excel\test.xlsx");
        }
    }
}
