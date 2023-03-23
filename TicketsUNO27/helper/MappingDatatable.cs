using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TicketsUNO27.helper
{
    public static class MappingDatatable
    {
        public static DataTable ToDataTableUsuario(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            table.Columns.Add("DOCUMENTO");
            table.Columns.Add("NOMBRES");
            table.Columns.Add("TELEFONO");
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row - 1; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                }
                if (!string.IsNullOrEmpty(newRow.ItemArray.First().ToString()))
                {
                    table.Rows.Add(newRow);
                }
            }
            return table;
        }
    }
}