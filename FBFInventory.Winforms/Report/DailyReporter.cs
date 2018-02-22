using ClosedXML.Excel;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Winforms.Report
{
    public class DailyReporter
    {
        private readonly DailyReportDTO _dailyReport;
        private readonly string _path;
        private IXLWorksheet _sheet;
        private int _rowIndex = 1;

        public DailyReporter(DailyReportDTO dailyReport, string path){
            _dailyReport = dailyReport;
            _path = path;
        }

        public void Export(){
            var wb = new XLWorkbook();
            _sheet = wb.Worksheets.Add("Daily " + _dailyReport.Date.ToString("MMM dd yyyy"));

            DisplayCoveredDate();
            DisplayColumns();
            DisplayItemForEachRecords();
            DisplayLedger();

            SetAutoAdjustColumnToContent();

            DailyReportDetailsMaker detailsMaker = new DailyReportDetailsMaker(wb, _dailyReport);
            detailsMaker.MakeInDetails();
            detailsMaker.MakeOutDetails();

            wb.SaveAs(_path);
        }

        private void DisplayCoveredDate(){
            var dateLabel = _sheet.Cell(_rowIndex, 2);
            dateLabel.Value = "Date : " + _dailyReport.Date.ToString("MMM dd yyyy");

            _rowIndex = _rowIndex + 2;
        }

        private void DisplayColumns(){
            var idColumn = _sheet.Cell(_rowIndex, 1);
            idColumn.Value = "ID";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = _sheet.Cell(_rowIndex, 2);
            itemNameColumn.Value = "Item Desc";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            var measuredbyColumn = _sheet.Cell(_rowIndex, 3);
            measuredbyColumn.Value = "Measured By";
            measuredbyColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            measuredbyColumn.Style.Font.Bold = true;

            var beginningColumn = _sheet.Cell(_rowIndex, 4);
            beginningColumn.Value = "Beginning";
            beginningColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            beginningColumn.Style.Font.Bold = true;

            var inColumn = _sheet.Cell(_rowIndex, 5);
            inColumn.Value = "+ In";
            inColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            inColumn.Style.Font.Bold = true;

            var outColumn = _sheet.Cell(_rowIndex, 6);
            outColumn.Value = "- Out";
            outColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            outColumn.Style.Font.Bold = true;

            var endingColumn = _sheet.Cell(_rowIndex, 7);
            endingColumn.Value = "Ending";
            endingColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            endingColumn.Style.Font.Bold = true;
            _rowIndex++;
        }

        private void DisplayItemForEachRecords(){
            foreach (var item in _dailyReport.Records){
                DisplayItem(item);
                _rowIndex++;
            }
        }

        private void DisplayItem(DailyItem i){
            var id = _sheet.Cell(_rowIndex, 1);
            id.Value = i.Id;

            var itemName = _sheet.Cell(_rowIndex, 2);
            itemName.Value = i.ItemName;

            var measuredBy = _sheet.Cell(_rowIndex, 3);
            measuredBy.Value = i.MeasuredBy.ToString();

            var beginning = _sheet.Cell(_rowIndex, 4);
            beginning.Value = i.BeginningQty;

            var inQty = _sheet.Cell(_rowIndex, 5);
            inQty.Value = i.In;
            inQty.Comment.Style.Alignment.SetAutomaticSize();
            inQty.Comment.AddText(i.CommentsForIn);
            if (i.HasReturnItems)
                inQty.Style.Fill.BackgroundColor = XLColor.LightPink;

            var outQty = _sheet.Cell(_rowIndex, 6);
            outQty.Value = i.Out;
            outQty.Comment.Style.Alignment.SetAutomaticSize();
            outQty.Comment.AddText(i.CommentsForOut);

            var ending = _sheet.Cell(_rowIndex, 7);
            ending.Value = i.EndingQty;
        }

        private void DisplayLedger(){
            _rowIndex = _rowIndex + 3;

            var lightPink = _sheet.Cell(_rowIndex, 2);
            lightPink.Style.Fill.BackgroundColor = XLColor.LightPink;

            var hasRetunedText = _sheet.Cell(_rowIndex, 3);
            hasRetunedText.Value = "Has Returned Item";
        }

        private void SetAutoAdjustColumnToContent(){
            _sheet.Column(1).AdjustToContents();
            _sheet.Column(2).AdjustToContents();
            _sheet.Column(3).AdjustToContents();
            _sheet.Column(4).AdjustToContents();
            _sheet.Column(5).AdjustToContents();
            _sheet.Column(6).AdjustToContents();
            _sheet.Column(7).AdjustToContents();
        }
    }
}
