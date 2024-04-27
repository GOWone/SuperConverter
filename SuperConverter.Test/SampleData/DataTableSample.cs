using System.Data;

namespace SuperConverter.Test.SampleData
{
    public class DataTableSample
    {
        public static DataTable SampleData()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 5; i++)
            {
                dt.Columns.Add(new DataColumn($"item{i}", typeof(string)));
            }

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[$"item{j}"] = $"data{i}_{j}";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
