using ClosedXML.Excel;
using System.Data;

namespace WebAppChain.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable GetTable(Object o)
        { 
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            var list = o as List<T>;

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo=> propertyInfo.GetValue(x,null)).ToArray();

                table.Rows.Add(values); 
            });

            return table;
        }

        public override object Handle(object o)
        {
            var wb = new XLWorkbook();

            var ds = new DataSet();

            ds.Tables.Add(GetTable(o));

            wb.Worksheets.Add(ds);

            var excelMS = new MemoryStream();

            wb.SaveAs(excelMS);


            return base.Handle(excelMS); //object olarak belirledigimiz icin istenilen her datayi gonderebiliyoruz
        }
    }
}
