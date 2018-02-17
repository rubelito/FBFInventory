using System.Collections.Generic;
using ClosedXML.Excel;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Report
{
    public class ItemsReporter
    {
        private readonly List<Item> _items;
        private readonly string _path;

        private IXLWorksheet _sheet;
        private int _rowIndex = 2;

        public ItemsReporter(List<Item> items, string path){
            _items = items;
            _path = path;
        }

        public void Export(){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add("Items");

            DisplayColumns();
            DisplayItemPerRow();
            DisplayColorLedger();

            SetAutoAdjustColumnToContent();

            wb.SaveAs(_path);
        }

        private void DisplayColumns(){
            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "Id";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = _sheet.Cell(_rowIndex, 2);
            itemNameColumn.Value = "Item Desc";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            var measuredByColumn = _sheet.Cell(_rowIndex, 3);
            measuredByColumn.Value = "Measured By";
            measuredByColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            measuredByColumn.Style.Font.Bold = true;

            var stocksColumn = _sheet.Cell(_rowIndex, 4);
            stocksColumn.Value = "Stock(s)";
            stocksColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            stocksColumn.Style.Font.Bold = true;

            var thresholdColumn = _sheet.Cell(_rowIndex, 5);
            thresholdColumn.Value = "Threshold";
            thresholdColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            thresholdColumn.Style.Font.Bold = true;

            _rowIndex++;
        }

        private void DisplayItemPerRow(){
            foreach (var i in _items){
                DisplayItem(i);
                _rowIndex = _rowIndex + 1;
            }
        }

        private void DisplayItem(Item item){
            var id = _sheet.Cell(_rowIndex, 1);
            id.Value = item.Id;

            var itemDesc = _sheet.Cell(_rowIndex, 2);
            itemDesc.Value = item.Name;

            var measuredBy = _sheet.Cell(_rowIndex, 3);
            measuredBy.Value = item.MeasuredBy.ToString();

            var stocks = _sheet.Cell(_rowIndex, 4);
            stocks.Value = item.GetAppropriateQuantity;

            if (item.IsNearOutOfStock)
                stocks.Style.Fill.BackgroundColor = XLColor.Rose;

            var threshold = _sheet.Cell(_rowIndex, 5);
            threshold.Value = item.Threshold;
        }

        private void DisplayColorLedger(){
            _rowIndex = _rowIndex + 3;

            var rose = _sheet.Cell(_rowIndex, 2);
            rose.Style.Fill.BackgroundColor = XLColor.Rose;

            var belowThresholdText = _sheet.Cell(_rowIndex, 3);
            belowThresholdText.Value = "Below Threshold";
        }

        private void SetAutoAdjustColumnToContent(){
            _sheet.Column(1).AdjustToContents();
            _sheet.Column(2).AdjustToContents();
            _sheet.Column(3).AdjustToContents();
            _sheet.Column(4).AdjustToContents();
            _sheet.Column(5).AdjustToContents();
        }
    }
}
