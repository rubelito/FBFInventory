using ClosedXML.Excel;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Report
{
    public class ItemHistoryReporter
    {
        private readonly ItemReportDTO _dto;
        private readonly string _path;

        private IXLWorksheet _sheet;
        private int _rowIndex = 2;

        public ItemHistoryReporter(ItemReportDTO dto, string path){
            _dto = dto;
            _path = path;
        }

        public void Export(){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add(_dto.ItemName);

            DisplayItemName();
            DisplayColumn();
            DisplayHistories();

            SetAutoAdjustColumnToContent();

            wb.SaveAs(_path);
        }

        private void DisplayItemName(){
            var itemLabel = _sheet.Cell(_rowIndex, 2);
            itemLabel.Value = "Item :";

            var itemName = _sheet.Cell(_rowIndex, 3);
            itemName.Value = _dto.ItemName;
            itemName.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var measuredByLabel = _sheet.Cell(_rowIndex, 2);
            measuredByLabel.Value = "Measured By :";

            var measuredBy = _sheet.Cell(_rowIndex, 3);
            measuredBy.Value = _dto.MeasuredBy;
            measuredBy.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex = _rowIndex + 2;
        }

        private void DisplayColumn(){
            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "ID";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var receiptTypeColumn = _sheet.Cell(_rowIndex, 2);
            receiptTypeColumn.Value = "Receipt Type";
            receiptTypeColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            receiptTypeColumn.Style.Font.Bold = true;

            var inOutColumn = _sheet.Cell(_rowIndex, 3);
            inOutColumn.Value = "In/Out";
            inOutColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            inOutColumn.Style.Font.Bold = true;

            var drColumn = _sheet.Cell(_rowIndex, 4);
            drColumn.Value = "(S)DR";
            drColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            drColumn.Style.Font.Bold = true;

            var beginningColomn = _sheet.Cell(_rowIndex, 5);
            beginningColomn.Value = "Beginning";
            beginningColomn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            beginningColomn.Style.Font.Bold = true;

            var qtyColumn = _sheet.Cell(_rowIndex, 6);
            qtyColumn.Value = "Qty";
            qtyColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            qtyColumn.Style.Font.Bold = true;

            var endingColumn = _sheet.Cell(_rowIndex, 7);
            endingColumn.Value = "Ending";
            endingColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            endingColumn.Style.Font.Bold = true;

            var dateColumn = _sheet.Cell(_rowIndex, 8);
            dateColumn.Value = "Date";
            dateColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dateColumn.Style.Font.Bold = true;

            var noteColumn = _sheet.Cell(_rowIndex, 9);
            noteColumn.Value = "Note";
            noteColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            noteColumn.Style.Font.Bold = true;

            var createdByColumn = _sheet.Cell(_rowIndex, 10);
            createdByColumn.Value = "In/Out By";
            createdByColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            createdByColumn.Style.Font.Bold = true;

            _rowIndex = _rowIndex + 1;
        }

        private void DisplayHistories(){
            foreach (var h in _dto.ItemHistories){
                DisplayItem(h);
                _rowIndex = _rowIndex + 1;
            }
        }

        private void DisplayItem(ItemHistory h){
            var id = _sheet.Cell(_rowIndex, 1);
            id.Value = h.Id;

            if (h.Type != ReceiptType.Nothing){
                var receiptType = _sheet.Cell(_rowIndex, 2);
                receiptType.Value = h.Type.ToString();
            }

            var inOut = _sheet.Cell(_rowIndex, 3);
            inOut.Value = h.InOrOut.ToString();
            inOut.Style.Fill.BackgroundColor = GetColor(h);

            if (h.DR != null){
                var sdr = _sheet.Cell(_rowIndex, 4);
                sdr.Value = h.DR.DRNumberToDisplay;
            }

            var beginning = _sheet.Cell(_rowIndex, 5);
            beginning.Value = h.AppopriateBeginningQty;

            var qty = _sheet.Cell(_rowIndex, 6);
            qty.Value = h.AppopriateQty;

            var endingQty = _sheet.Cell(_rowIndex, 7);
            endingQty.Value = h.AppopriateEndingQty;

            var date = _sheet.Cell(_rowIndex, 8);
            date.Style.DateFormat.Format = @"[$-409]m/d/yy (h:mm AM/PM);@";
            date.Value = h.DateAdded;
            date.DataType = XLCellValues.DateTime;

            var note = _sheet.Cell(_rowIndex, 9);
            note.Value = h.Note;

            var createdBy = _sheet.Cell(_rowIndex, 10);
            createdBy.Value = h.CreatedBy;
        }

        private XLColor GetColor(ItemHistory h){
            XLColor c = XLColor.White;

            if (h.InOrOut == InOrOut.In)
                c = XLColor.LightGreen;
            if (h.InOrOut == InOrOut.Out)
                c = XLColor.LightCoral;

            if (h.IsMistaken)
                c = XLColor.Orange;

            return c;
        }

        private void SetAutoAdjustColumnToContent(){
            _sheet.Column(1).AdjustToContents(); 
            _sheet.Column(2).AdjustToContents();
            _sheet.Column(3).AdjustToContents(); 
            _sheet.Column(4).AdjustToContents();
            _sheet.Column(5).AdjustToContents();
            _sheet.Column(6).AdjustToContents();
            _sheet.Column(7).AdjustToContents();
            _sheet.Column(8).AdjustToContents();
            _sheet.Column(9).AdjustToContents();
            _sheet.Column(10).AdjustToContents();
        }
    }
}
