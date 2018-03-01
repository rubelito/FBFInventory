using ClosedXML.Excel;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Winforms.Report
{
    public class DRReporter
    {
        private readonly DR _dr;
        private readonly string _path;

        private IXLWorksheet _sheet;
        private int _rowIndex = 2;

        private string _supplierOrCustomerLabel;
        private string _SDRorDRLabel;

        public DRReporter(DR dr, string path){
            _dr = dr;
            _path = path;
        }

        public void Export(){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add(_dr.DRNumberToDisplay);

            SetDisplayValues();
            DisplayDRInfo();
            
            DisplayItemsColumn();
            DisplayItemForEachRow();

            wb.SaveAs(_path);
        }

        private void SetDisplayValues(){
            if (_dr.Type == ReceiptType.SDR){
                _supplierOrCustomerLabel = "Supplier :";
                _SDRorDRLabel = "SDR :";
            }
            else if (_dr.Type == ReceiptType.DR){
                _supplierOrCustomerLabel = "Customer :";
                _SDRorDRLabel = "DR :";
            }
        }

        private void DisplayDRInfo(){
            var supplierOrCustomerLabel = _sheet.Cell(_rowIndex, 1);
            supplierOrCustomerLabel.Value = _supplierOrCustomerLabel;
            supplierOrCustomerLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            supplierOrCustomerLabel.Style.Font.Bold = true;

            var supplierOrCustomer = _sheet.Cell(_rowIndex, 2);
            supplierOrCustomer.Value = _dr.RecipientToDisplay;
            supplierOrCustomer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var sdrOrDrLabel = _sheet.Cell(_rowIndex, 1);
            sdrOrDrLabel.Value = _SDRorDRLabel;
            sdrOrDrLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            sdrOrDrLabel.Style.Font.Bold = true;

            var sdrOrDr = _sheet.Cell(_rowIndex, 2);
            sdrOrDr.Value = _dr.DRNumberToDisplay;
            sdrOrDr.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var createdByLabel = _sheet.Cell(_rowIndex, 1);
            createdByLabel.Value = "Created By :";
            createdByLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            createdByLabel.Style.Font.Bold = true;

            var createdBy = _sheet.Cell(_rowIndex, 2);
            createdBy.Value = _dr.CreatedBy;
            createdBy.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            
            if (_dr.Type == ReceiptType.DR){
                _rowIndex++;
                var projectLabel = _sheet.Cell(_rowIndex, 1);
                projectLabel.Value = "Project :";
                projectLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                projectLabel.Style.Font.Bold = true;

                var project = _sheet.Cell(_rowIndex, 2);
                project.Value = _dr.Project;
                project.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                _rowIndex++;
                var addressLabel = _sheet.Cell(_rowIndex, 1);
                addressLabel.Value = "Address :";
                addressLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                addressLabel.Style.Font.Bold = true;

                var address = _sheet.Cell(_rowIndex, 2);
                address.Value = _dr.DeliveryAddress;
                address.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                _rowIndex++;
                var deliveredByLabel = _sheet.Cell(_rowIndex, 1);
                deliveredByLabel.Value = "Delivered By :";
                deliveredByLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                deliveredByLabel.Style.Font.Bold = true;

                var deliveredBy = _sheet.Cell(_rowIndex, 2);
                deliveredBy.Value = _dr.DeliveredBy;
                deliveredBy.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                _rowIndex++;
                var vehicleNumberLabel = _sheet.Cell(_rowIndex, 1);
                vehicleNumberLabel.Value = "Vehicle no. :";
                vehicleNumberLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                vehicleNumberLabel.Style.Font.Bold = true;

                var vehicleNumber = _sheet.Cell(_rowIndex, 2);
                vehicleNumber.Value = _dr.VehiclePlateNumber;
                vehicleNumber.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            _rowIndex++;
            var dateLabel = _sheet.Cell(_rowIndex, 1);
            dateLabel.Value = "Date :";
            dateLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dateLabel.Style.Font.Bold = true;

            var date = _sheet.Cell(_rowIndex, 2);
            date.Value = _dr.Date.ToString("MMM dd, yyyy");
            date.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _rowIndex++;
            var noteLabel = _sheet.Cell(_rowIndex, 1);
            noteLabel.Value = "Note :";
            noteLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            noteLabel.Style.Font.Bold = true;

            var note = _sheet.Cell(_rowIndex, 2);
            note.Value = _dr.Note;
            note.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void DisplayItemsColumn(){
            _rowIndex = _rowIndex + 2;
            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "Id";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = _sheet.Cell(_rowIndex, 2);
            itemNameColumn.Value = "Item Desc";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            var measuredByColumn = _sheet.Cell(_rowIndex, 3);
            measuredByColumn.Value = _dr.Type.ToString();
            measuredByColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            measuredByColumn.Style.Font.Bold = true;

            var qtyLabel = _sheet.Cell(_rowIndex, 4);
            qtyLabel.Value = "QTY";
            qtyLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            qtyLabel.Style.Font.Bold = true;
        }

        private void DisplayItemForEachRow(){          
            foreach (var i in _dr.Items){
                _rowIndex++;
                DisplayItem(i);
            }
        }

        private void DisplayItem(DRItem drItem){
            var id = _sheet.Cell(_rowIndex, 1);
            id.Value = drItem.Id;

            var itemName = _sheet.Cell(_rowIndex, 2);
            itemName.Value = drItem.Item.Name;

            var measuredBy = _sheet.Cell(_rowIndex, 3);
            measuredBy.Value = drItem.Item.MeasuredBy.ToString();

            var qty = _sheet.Cell(_rowIndex, 4);
            qty.Value = drItem.Qty;
        }
    }
}
