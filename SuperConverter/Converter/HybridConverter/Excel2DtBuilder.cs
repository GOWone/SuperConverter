/*********************************************************************
 * Copyright(c) YaMoStudio All Rights Reserved.
 * 开发者：YaMoStudio
 * 命名空间：SuperConverter.Converter.HybridConverter
 * 文件名：Excel2DtBuilder
 * 版本号：V1.0.0.0
 * 创建时间：2024/4/26 11:16:21
 ******************************************************/

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;

namespace SuperConverter.Converter.HybridConverter
{
    public class Excel2DtBuilder
    {
        /// <summary>
        /// Convert excel file to DataTable data type
        /// </summary>
        /// <param name="filePath">Source</param>
        /// <returns>DataTable</returns>
        public static DataTable Excel2Dt(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath).ToLower();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xlsx")
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileExt == ".xls")
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                ISheet sheet = workbook.GetSheetAt(0);

                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                    {
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    }
                    columns.Add(i);
                }

                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:    //BLANK:  
                    return null;
                case CellType.Boolean:  //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric:  //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String:   //STRING:  
                    return cell.StringCellValue;
                case CellType.Error:    //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula:  //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}
