using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Winforms.Report
{
    public class WeeklyReporter
    {
        private readonly WeeklyReportDTO _weeklyReport;
        private readonly string _path;

        private XLWorkbook _wb;
        private IXLWorksheet _sheetForIn;
        private IXLWorksheet _sheetForOut;
        private IXLWorksheet _sheetForSupplier;

        private InOrOut _currentSheet;

        private int _rowIndex = 1;

        private List<DaySection> _daySections;
        private int _maximumColumn;

        public WeeklyReporter(WeeklyReportDTO weeklyReport, string path){
            _weeklyReport = weeklyReport;
            _path = path;
        }

        public void Export(){
            _wb = new XLWorkbook();
            _daySections = _weeklyReport.DaySections.OrderBy(d => d.Day).ToList();
            CreateSheetForIn();
            ResetValues();
            CreateSheetForOut();
            ResetValues();

            CreateSheetForEverySupplierIn();

            _wb.SaveAs(_path);
        }

        private void CreateSheetForEverySupplierIn(){
            _sheetForSupplier = _wb.Worksheets.Add("Supplier In");
            
            CreateColumnForSupplierIn();
            DisplaySupplierRecords();

            _rowIndex = _rowIndex + 2;

            DisplayTotalForEachItems(); 
            SetAutoAdjustColumnForSupplierIn();
        }

        private void DisplaySupplierRecords(){
            foreach (var sSection in _weeklyReport.SupplierReport.SupplierSections){
                _rowIndex = _rowIndex + 2;
                var supplierName = _sheetForSupplier.Cell(_rowIndex, 1);
                supplierName.Value = sSection.SupplierName;

                foreach (var item in sSection.Items){
                    _rowIndex++;

                    var itemName = _sheetForSupplier.Cell(_rowIndex, 2);
                    itemName.Value = item.ItemName;

                    var itemQty = _sheetForSupplier.Cell(_rowIndex, 3);
                    itemQty.Value = item.InQty;

                    var measuredBy = _sheetForSupplier.Cell(_rowIndex, 4);
                    measuredBy.Value = item.MeasuredBy;
                }

                _rowIndex++;
                var total = _sheetForSupplier.Cell(_rowIndex, 3);
                total.Value = sSection.Total;
                total.Style.Font.Bold = true;
                total.Style.Border.TopBorder = XLBorderStyleValues.Medium;
            }
        }

        private void DisplayTotalForEachItems(){
            var totalColumn = _sheetForSupplier.Cell(_rowIndex, 1);
            totalColumn.Value = "Total";
            totalColumn.Style.Font.Bold = true;

            foreach (var item in _weeklyReport.SupplierReport.TotalForEachItems)
            {
                _rowIndex++;
                var itemName = _sheetForSupplier.Cell(_rowIndex, 2);
                itemName.Value = item.ItemName;

                var itemQty = _sheetForSupplier.Cell(_rowIndex, 3);
                itemQty.Value = item.InQty;

                var measuredBy = _sheetForSupplier.Cell(_rowIndex, 4);
                measuredBy.Value = item.MeasuredBy;
            }

            _rowIndex++;
            var grandTotal = _sheetForSupplier.Cell(_rowIndex, 3);
            grandTotal.Value = _weeklyReport.SupplierReport.GrandTotal;
            grandTotal.Style.Font.Bold = true;
            grandTotal.Style.Border.TopBorder = XLBorderStyleValues.Medium;
        }

        private void CreateColumnForSupplierIn(){
            var supplierColumn = _sheetForSupplier.Cell(_rowIndex, 1);
            supplierColumn.Value = "Supplier";
            supplierColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            supplierColumn.Style.Font.Bold = true;

            var itemDescColumn = _sheetForSupplier.Cell(_rowIndex, 2);
            itemDescColumn.Value = "Item Desc";
            itemDescColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemDescColumn.Style.Font.Bold = true;

            var itemQty = _sheetForSupplier.Cell(_rowIndex, 3);
            itemQty.Value = "Qty";
            itemQty.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemQty.Style.Font.Bold = true;

            var measuredByColumn = _sheetForSupplier.Cell(_rowIndex, 4);
            measuredByColumn.Value = "Measured By";
            measuredByColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            measuredByColumn.Style.Font.Bold = true;
        }

        private void ResetValues(){
            _rowIndex = 1;
            _maximumColumn = 0;
        }

        private void CreateSheetForIn(){
            _sheetForIn = _wb.Worksheets.Add("Weekly In");
            _currentSheet = InOrOut.In;
            CreateSheet();
        }

        private void CreateSheetForOut(){
            _sheetForOut = _wb.Worksheets.Add("Weekly Out");
            _currentSheet = InOrOut.Out;
            CreateSheet();
        }

        private void CreateSheet(){
            DisplayCoveredDateRange();
            DisplayColumns();
            DisplayItemsForEachRows();
            DisplayTotalAtButtom();

            SetAutoAdjustColumnToContent();
        }

        private void DisplayCoveredDateRange()
        {
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            var dateLabel = tempSheet.Cell(_rowIndex, 2);
            dateLabel.Value = "Date Covered: " + _weeklyReport.DateCoverage;

            _rowIndex = _rowIndex + 2;
        }

        private void DisplayColumns(){
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            var idColumn = tempSheet.Cell(_rowIndex, 1);
            idColumn.Value = "Id";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = tempSheet.Cell(_rowIndex, 2);
            itemNameColumn.Value = "Item Name";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            int currentColumnIndex = 2;
            foreach (var d in _daySections){
                currentColumnIndex++;

                d.ColumnIndex = currentColumnIndex;

                var dayColumn = tempSheet.Cell(_rowIndex, currentColumnIndex);
                dayColumn.Value = d.WeekDay;
                dayColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                dayColumn.Style.Font.Bold = true;
            }

            currentColumnIndex++;
            var totalColumn = tempSheet.Cell(_rowIndex, currentColumnIndex);
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
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            var id = tempSheet.Cell(_rowIndex, 1);
            id.Value = row.Id;

            var itemName = tempSheet.Cell(_rowIndex, 2);
            itemName.Value = row.ItemName;

            foreach (var d in _daySections){
                DisplayQty(d, row);
            }

            DisplayTotal(row);
        }

        private void DisplayQty(DaySection d, ItemRow row){
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            double qty = 0;
            DailyInOut dInOut = row.DailyInOuts.FirstOrDefault(o => o.DaySection.DateId == d.DateId);

            if (dInOut != null){
                if (_currentSheet == InOrOut.In)
                    qty = dInOut.InQty;
                else if (_currentSheet == InOrOut.Out)
                    qty = dInOut.OutQty;
            }

            var qtyCell = tempSheet.Cell(_rowIndex, d.ColumnIndex);
            qtyCell.Value = qty;
        }

        private void DisplayTotal(ItemRow row){
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            var total = tempSheet.Cell(_rowIndex, _maximumColumn);
            total.Value = _currentSheet == InOrOut.In ? row.InTotal : row.OutTotal;
        }

        private void DisplayTotalAtButtom(){
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            _rowIndex = _rowIndex + 2;
            var totalLabel = tempSheet.Cell(_rowIndex, 2);
            totalLabel.Value = "Total :";
            totalLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            totalLabel.Style.Font.Bold = true;

            var total = tempSheet.Cell(_rowIndex, _maximumColumn);
            total.Value = _currentSheet == InOrOut.In ? _weeklyReport.InTotal : _weeklyReport.OutTotal;
            total.Style.Border.TopBorder = XLBorderStyleValues.Medium;
            total.Style.Font.Bold = true;
        }

        private void SetAutoAdjustColumnToContent(){
            IXLWorksheet tempSheet = _currentSheet == InOrOut.In ? _sheetForIn : _sheetForOut;

            for (int i = 1; i <= _maximumColumn; i++){
                tempSheet.Column(i).AdjustToContents();
            }
        }

        private void SetAutoAdjustColumnForSupplierIn(){
            _sheetForSupplier.Column(1).AdjustToContents();
            _sheetForSupplier.Column(2).AdjustToContents();
            _sheetForSupplier.Column(3).AdjustToContents();
            _sheetForSupplier.Column(4).AdjustToContents();
        }
    }
}
