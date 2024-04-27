using NUnit.Framework;
using SuperConverter.Converter.DataConverter;
using SuperConverter.Test.SampleData;
using SuperConverter.Test.SampleModels;
using System.Data;

namespace SuperConverter.Test.Analysis
{
    public class DataConverTest
    {
        /// <summary>
        /// DataTable2ModelList
        /// </summary>
        [Test]
        public static void DataTableBuilderTest()
        {
            DataTable source = DataTableSample.SampleData();
            var result = DataTableBuilders.Dt2ModelList<DataTableModel>(source);
        }

        /// <summary>
        /// DataRow2Model
        /// </summary>
        [Test]
        public static void DataRowBuilderTest()
        {
            DataTable source = DataTableSample.SampleData();
            DataRow row = source.Rows[0];
            var result = DataRowBuilders.Dr2Model<DataTableModel>(row);

        }
    }
}
