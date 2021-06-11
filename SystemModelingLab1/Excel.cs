using Microsoft.Office.Interop.Excel;
using System;

namespace SystemModelingLab1
{
    public class Excel
    {
        Application excel;
        Workbook workbook;
        Worksheet worksheet;
        int rowCounter = 2;

        public Excel()
        {
            CreateDocument();
        }

        public void WriteRow(string[] dataRow)
        {
            for (int column = 0; column < dataRow.Length; column++)
            {
                worksheet.Cells[rowCounter, column + 1] = dataRow[column];
            }

            rowCounter++;
        }

        public void SaveAndClose()
        {
            var location = AppDomain.CurrentDomain.BaseDirectory;
            workbook.SaveAs(location + "SystemModelingLab1");
            workbook.Close();
            excel.Quit();
        }

        public void CreateDocument()
        {
            try
            {
                excel = new Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                workbook = excel.Workbooks.Add(Type.Missing);

                worksheet = (Worksheet)workbook.ActiveSheet;
                worksheet.Name = "Lab 1";
                worksheet.Cells[1, 1] = "Event";
                worksheet.Cells[1, 2] = "t";
                worksheet.Cells[1, 3] = "e1";
                worksheet.Cells[1, 4] = "e2";
                worksheet.Cells[1, 5] = "h";
                worksheet.Cells[1, 6] = "S";
                worksheet.Cells[1, 7] = "N";
                worksheet.Cells[1, 8] = "Q";
                worksheet.Cells[1, 9] = "Free time coeff.";
                worksheet.Cells[1, 10] = "Average queue count";

  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
