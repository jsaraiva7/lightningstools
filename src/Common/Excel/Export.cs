using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Common.Excel
{
    public class Export
    {
        public static ExcelPackage ListToExcel<T>(List<T> query, ExcelPackage pck, string scheetName)
        {
             

            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(scheetName);

            //get our column headings
            var t = typeof(T);
            var Headings = t.GetProperties();
            for (int i = 0; i < Headings.Count(); i++)
            {

                ws.Cells[1, i + 1].Value = Headings[i].Name;
            }

            //populate our Data
            if (query.Count() > 0)
            {
                ws.Cells["A2"].LoadFromCollection(query);
            }

            //Format the header
            using (ExcelRange rng = ws.Cells["A1:BZ1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189)); //Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White);
            }



            return pck;
        }
    }
}