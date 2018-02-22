using System.Collections.Generic;
using ClosedXML.Excel;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.ReportPoco;

namespace FBFInventory.Winforms.Report
{
    public class DailyReportDetailsMaker
    {
        private readonly XLWorkbook _wb;
        private readonly DailyReportDTO _dto;

        private IXLWorksheet _inSheet;
        private IXLWorksheet _outSheet;

        private int _inRowIndex = 1;
        private int _outRowIndex = 1;

        public DailyReportDetailsMaker(XLWorkbook wb, DailyReportDTO dto){
            _wb = wb;
            _dto = dto;
        }

        public IXLWorksheet MakeInDetails(){
            _inSheet = _wb.Worksheets.Add("In Details");

            DisplayColumns(InOrOut.In);
            DisplayItems(InOrOut.In);
            SetAutoAdjustColumnToContent(InOrOut.In);

            return _inSheet;
        }

        public IXLWorksheet MakeOutDetails(){
            _outSheet = _wb.Worksheets.Add("Out Details");
            DisplayColumns(InOrOut.Out);
            DisplayItems(InOrOut.Out);
            SetAutoAdjustColumnToContent(InOrOut.Out);

            return _outSheet;
        }

        private void DisplayColumns(InOrOut inOut){
            IXLWorksheet tempSheet = inOut == InOrOut.In ? _inSheet : _outSheet;

            var idColumn = tempSheet.Cell(1, 1);
            idColumn.Value = "ID";
            idColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            idColumn.Style.Font.Bold = true;

            var itemNameColumn = tempSheet.Cell(1, 2);
            itemNameColumn.Value = "Item Desc";
            itemNameColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            itemNameColumn.Style.Font.Bold = true;

            var totalColumn = tempSheet.Cell(1, 3);
            totalColumn.Value = "Total";
            totalColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            totalColumn.Style.Font.Bold = true;

            var commentsColumn = tempSheet.Cell(1, 4);
            commentsColumn.Value = "Comments";
            commentsColumn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            commentsColumn.Style.Font.Bold = true;
        }

        private void DisplayItems(InOrOut inOut){
            IXLWorksheet tempSheet = inOut == InOrOut.In ? _inSheet : _outSheet;

            foreach (var item in _dto.Records){
                IncrementRowIndex(inOut, 2);

                var id = tempSheet.Cell(GetAppopriateRowIndex(inOut), 1);
                id.Value = item.Id;

                var itemDesc = tempSheet.Cell(GetAppopriateRowIndex(inOut), 2);
                itemDesc.Value = item.ItemName;

                var total = tempSheet.Cell(GetAppopriateRowIndex(inOut), 3);
                total.Value = inOut == InOrOut.In ? item.In : item.Out;

                List<string> notes = inOut == InOrOut.In ? item.NotesForIn : item.NotesForOut;

                foreach (var n in notes){
                    IncrementRowIndex(inOut, 1);

                    var comment = tempSheet.Cell(GetAppopriateRowIndex(inOut), 4);
                    comment.Value = n;
                }
            }
        }

        private void IncrementRowIndex(InOrOut inOut, int incrementBy){
            if (inOut == InOrOut.In)
                _inRowIndex = _inRowIndex + incrementBy;
            else if (inOut == InOrOut.Out)
                _outRowIndex = _outRowIndex + incrementBy;
        }

        private int GetAppopriateRowIndex(InOrOut inOut){
            int rowIndex = 0;
            if (inOut == InOrOut.In)
                rowIndex = _inRowIndex;
            else if (inOut == InOrOut.Out)
                rowIndex = _outRowIndex;

            return rowIndex;
        }

        private void SetAutoAdjustColumnToContent(InOrOut inOut){
            if (inOut == InOrOut.In){
                _inSheet.Column(1).AdjustToContents();
                _inSheet.Column(2).AdjustToContents();
                _inSheet.Column(3).AdjustToContents();
                _inSheet.Column(4).AdjustToContents();
            }
            else if (inOut == InOrOut.Out){
                _outSheet.Column(1).AdjustToContents();
                _outSheet.Column(2).AdjustToContents();
                _outSheet.Column(3).AdjustToContents();
                _outSheet.Column(4).AdjustToContents();
            }
        }
    }
}
