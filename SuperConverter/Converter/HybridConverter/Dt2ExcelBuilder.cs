/*********************************************************************
 * Copyright(c) YaMoStudio All Rights Reserved.
 * 开发者：YaMoStudio
 * 命名空间：SuperConverter.Converter.HybridConverter
 * 文件名：Dt2ExcelBuilder
 * 版本号：V1.0.0.0
 * 创建时间：2024/4/26 11:16:21
 ******************************************************/

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SuperConverter.Parameters;
using System.Data;

namespace SuperConverter.Converter.HybridConverter
{
    public class Dt2ExcelBuilder
    {
        /// <summary>
        /// Convert DataTable data type to excel file
        /// </summary>
        /// <param name="dt">Source</param>
        /// <param name="mode">Excel Type</param>
        /// <param name="folder">Save Folder Path</param>
        /// <param name="fileName">File Name</param>
        public static void Dt2Excel(DataTable dt, ExcelMode mode, string folder, string fileName)
        {
            try
            {
                IWorkbook workbook;
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string path = Path.Combine(folder, fileName);

                if (mode == ExcelMode.Xlsx)
                {
                    path += ".xlsx";
                    workbook = new XSSFWorkbook();
                }
                else if (mode == ExcelMode.Xls)
                {
                    path += ".xls";
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    path += ".xlsx";
                    workbook = new XSSFWorkbook();
                }

                ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                var buf = stream.ToArray();

                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
