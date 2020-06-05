using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace microsoft_documents_npoi
{
    public class TestExcel
    {
        public IWorkbook ReadWorkbook(string path)
        {
            IWorkbook book;

            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Try to read workbook as XLSX:
                try
                {
                    book = new XSSFWorkbook(fs);
                }
                catch
                {
                    book = null;
                }

                // If reading fails, try to read workbook as XLS:
                if (book == null)
                {
                    book = new HSSFWorkbook(fs);
                }

                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EvaluateSheet(ISheet sheet)
        {
            var wb = sheet.Workbook;
            var evaluator = wb.GetCreationHelper().CreateFormulaEvaluator();

            for (var rowIndex = sheet.FirstRowNum; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null)
                {
                    continue;
                }
                foreach (var cell in row.Cells)
                {
                    if (cell == null)
                    {
                        continue;
                    }
                    if (cell.CellType == CellType.Formula)
                    {
                        evaluator.EvaluateFormulaCell(cell);
                    }
                }
            }
        }
    }
}
