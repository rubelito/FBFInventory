using System.Collections.Generic;
using ClosedXML.Excel;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Report
{
    public class ReturnHistoryReporter
    {
        private readonly ReturnedHistory _history;
        private readonly List<ReturnedItem> _goodItems;
        private readonly List<ScrapItem> _scrapItems;

        private int _rowIndex = 2;

        private IXLWorksheet _sheet;

        public ReturnHistoryReporter(ReturnedHistory history,
            List<ReturnedItem> goodItems, List<ScrapItem> scrapItems){
            _history = history;
            _goodItems = goodItems;
            _scrapItems = scrapItems;
        }

        public void Export(string path){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add(_history.DRNumber);

            DisplayDRInfo();
            DisplayColumn();
            DisplayGoodItems();
            DisplayScrapItems();

            wb.SaveAs(path);
        }

        private void DisplayDRInfo(){
            var drLabel = _sheet.Cell(_rowIndex, 2);
            drLabel.Value = "DR :";
            drLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            drLabel.Style.Font.Bold = true;

            var dr = _sheet.Cell(_rowIndex, 3);
            dr.Value = _history.DRNumber;
            dr.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var projectEngLabel = _sheet.Cell(_rowIndex, 2);
            projectEngLabel.Value = "Project Eng :";
            projectEngLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            drLabel.Style.Font.Bold = true;

            var projectEng = _sheet.Cell(_rowIndex, 3);
            projectEng.Value = _history.ProjectEngineer;
            projectEng.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var projectLabel = _sheet.Cell(_rowIndex, 2);
            projectLabel.Value = "Project :";
            projectLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            projectLabel.Style.Font.Bold = true;

            var project = _sheet.Cell(_rowIndex, 3);
            project.Value = _history.DR.Project;
            project.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var addressLabel = _sheet.Cell(_rowIndex, 2);
            addressLabel.Value = "Address :";
            addressLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            addressLabel.Style.Font.Bold = true;

            var address = _sheet.Cell(_rowIndex, 3);
            address.Value = _history.DR.DeliveryAddress;
            address.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var dateLabel = _sheet.Cell(_rowIndex, 2);
            dateLabel.Value = "Date :";
            dateLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dateLabel.Style.Font.Bold = true;

            var date = _sheet.Cell(_rowIndex, 3);
            date.Value = _history.Date.ToString("MMM dd, yyyy");
            date.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var noteLabel = _sheet.Cell(_rowIndex, 2);
            noteLabel.Value = "Note :";
            noteLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            noteLabel.Style.Font.Bold = true;

            var note = _sheet.Cell(_rowIndex, 3);
            note.Value = _history.Note;
            note.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void DisplayColumn(){
            _rowIndex = _rowIndex + 3;

            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "Id";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var nameColumn = _sheet.Cell(_rowIndex, 2);
            nameColumn.Value = "Item Desc";
            nameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            nameColumn.Style.Font.Bold = true;

            var qtyColumn = _sheet.Cell(_rowIndex, 3);
            qtyColumn.Value = "Qty";
            qtyColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            qtyColumn.Style.Font.Bold = true;
        }

        private void DisplayGoodItems(){
            _rowIndex = _rowIndex + 2;

            var goodLabel = _sheet.Cell(_rowIndex, 2);
            goodLabel.Value = "Good";
            goodLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            goodLabel.Style.Font.Bold = true;

            foreach (var good in _goodItems){
                _rowIndex++;
                var id = _sheet.Cell(_rowIndex, 1);
                id.Value = good.Item.Id;

                var itemDesc = _sheet.Cell(_rowIndex, 2);
                itemDesc.Value = good.Item.Name;

                var qty = _sheet.Cell(_rowIndex, 3);
                qty.Value = good.Qty;
            }
        }

        private void DisplayScrapItems(){
            _rowIndex = _rowIndex + 2;
            var goodLabel = _sheet.Cell(_rowIndex, 2);
            goodLabel.Value = "Scrap";
            goodLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            goodLabel.Style.Font.Bold = true;

            foreach (var scrap in _scrapItems){
                _rowIndex++;
                var id = _sheet.Cell(_rowIndex, 1);
                id.Value = scrap.Item.Id;

                var itemDesc = _sheet.Cell(_rowIndex, 2);
                itemDesc.Value = scrap.Item.Name;

                var qty = _sheet.Cell(_rowIndex, 3);
                qty.Value = scrap.Qty;
            }
        }
    }
}
