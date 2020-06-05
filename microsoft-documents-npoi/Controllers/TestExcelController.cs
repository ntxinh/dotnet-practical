using Microsoft.AspNetCore.Mvc;

namespace microsoft_documents_npoi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestExcelController : ControllerBase
    {
        public TestExcelController()
        {

        }

        [HttpGet("TestExcel")]
        public IActionResult TestExcel()
        {
            var testExcel = new TestExcel();

            // Workbook
            var workbook = testExcel.ReadWorkbook("YourExcel.xlsx");

            // Sheet
            var sheetItem = workbook.GetSheet("Item");

            // Row
            var row = sheetItem.GetRow(0);

            // Cell
            var cellUnitCostValue = row.GetCell(1);
            var unitCost = cellUnitCostValue.NumericCellValue;
            var cellBaseCostValue = row.GetCell(4);
            var baseCost = cellBaseCostValue.NumericCellValue;

            cellUnitCostValue.SetCellValue(10);
            unitCost = cellUnitCostValue.NumericCellValue;

            // Evaluate
            testExcel.EvaluateSheet(sheetItem);

            baseCost = cellBaseCostValue.NumericCellValue;

            var row2 = sheetItem.GetRow(2);
            var cellTotalCostValue = row.GetCell(4);
            var totalCost = cellTotalCostValue.NumericCellValue;

            return Ok(baseCost);
        }
    }
}
