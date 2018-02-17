using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Winforms.Report
{
    public class WeeklyReporter
    {
        private readonly WeeklyReportDTO _weeklyReport;
        private readonly string _path;
        private IXLWorksheet _sheet;
        private int _rowIndex = 1;

        private List<DaySection> _daySections;
        private int _maximumColumn;

        public WeeklyReporter(WeeklyReportDTO weeklyReport, string path){
            _weeklyReport = weeklyReport;
            _path = path;
        }

        public void Export(){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add("Weekly");
            _daySections = _weeklyReport.DaySections.OrderBy(d => d.Day).ToList();

            DisplayCoveredDateRange();
            DisplayColumns();
            DisplayItemsForEachRows();
            DisplayTotalAtButtom();

            SetAutoAdjustColumnToContent();
            wb.SaveAs(_path);
        }

        private void DisplayCoveredDateRange()
        {
            var dateLabel = _sheet.Cell(_rowIndex, 2);
            dateLabel.Value = "Date Covered: " + _weeklyReport.DateCoverage;

            _rowIndex = _rowIndex + 2;
        }

        private void DisplayColumns(){
            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "Id";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = _sheet.Cell(_rowIndex, 2);
            itemNameColumn.Value = "Item Name";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            int currentColumnIndex = 2;
            foreach (var d in _daySections){
                currentColumnIndex++;

                d.ColumnIndex = currentColumnIndex;

                var dayColumn = _sheet.Cell(_rowIndex, currentColumnIndex);
                dayColumn.Value = d.WeekDay;
                dayColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                dayColumn.Style.Font.Bold = true;
            }

            currentColumnIndex++;
            var totalColumn = _sheet.Cell(_rowIndex, currentColumnIndex);
            totalColumn.Value = "Total";
            totalColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            totalColumn.Style.Font.Bold = true;

            _maximumColumn = currentColumnIndex;
            _rowIndex++;
        }

        private void DisplayItemsForEachRows(){
            foreach (var item in _weeklyReport.Items){
                DisplayItem(item);
                _rowIndex++;
            }
        }

        private void DisplayItem(ItemRow row){
            var id = _sheet.Cell(_rowIndex, 1);
            id.Value = row.Id;

            var itemName = _sheet.Cell(_rowIndex, 2);
            itemName.Value = row.ItemName;

            foreach (var d in _daySections){
                DisplayQty(d, row);
            }

            DisplayTotal(row);
        }

        private void DisplayQty(DaySection d, ItemRow row){
            double qty = 0;
            DailyOut dOut = row.DailyOuts.FirstOrDefault(o => o.DaySection.DateId == d.DateId);

            if (dOut != null){
                qty = dOut.OutQty;
            }

            var qtyCell = _sheet.Cell(_rowIndex, d.ColumnIndex);
            qtyCell.Value = qty;
        }

        private void DisplayTotal(ItemRow row){
            var total = _sheet.Cell(_rowIndex, _maximumColumn);
            total.Value = row.Total;
        }

        private void DisplayTotalAtButtom(){
            _rowIndex = _rowIndex + 2;
            var totalLabel = _sheet.Cell(_rowIndex, 2);
            totalLabel.Value = "Total :";
            totalLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            totalLabel.Style.Font.Bold = true;

            var total = _sheet.Cell(_rowIndex, _maximumColumn);
            total.Value = _weeklyReport.Total;
            total.Style.Border.TopBorder = XLBorderStyleValues.Medium;
            total.Style.Font.Bold = true;
        }

        private void SetAutoAdjustColumnToContent(){
            for (int i = 1; i <= _maximumColumn; i++){
                _sheet.Column(i).AdjustToContents();
            }
        }
    }
}
