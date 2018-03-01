using FBFInventory.Domain.Entity;
using FBFInventory.Domain.History;
using FBFInventory.Infrastructure.Dto;

namespace FBFInventory.Infrastructure.Service
{
    public class InOutService
    {
        private readonly ItemService _itemService;
        private readonly HistoryService _historyService;
        private readonly DRService _drService;
        private readonly ReturnedHistoryService _returnedHistoryService;

        public InOutService(ItemService itemService, HistoryService 
            historyService, DRService drService, ReturnedHistoryService returnedHistoryService){
            _itemService = itemService;
            _historyService = historyService;
            _drService = drService;
            _returnedHistoryService = returnedHistoryService;
        }

        public void InOutWithDR(InOutDRParam inParam){
            Item itemAfterInOut = _itemService.Find(inParam.DRItem.Item.Id);
            double oldQty = itemAfterInOut.GetAppropriateQuantity;

            if (inParam.InOrOut == InOrOut.In)
                InOutHelper.AddToAppopriateMeasurement(itemAfterInOut, inParam.DRItem.Qty);
            else if (inParam.InOrOut == InOrOut.Out)
                InOutHelper.SubtractToAppopriateMeasurement(itemAfterInOut, inParam.DRItem.Qty);
           
            double newQty = itemAfterInOut.GetAppropriateQuantity;

            inParam.DRItem.Item = itemAfterInOut;
            var p = CreateHistoryParameterWithDr(inParam, oldQty, newQty);

            ItemHistory h = InOutHelper.MakeHistory(p);
            _historyService.Add(h);
            _itemService.Edit(itemAfterInOut);

            _drService.AddToDR(inParam.DRItem.DR.Id, inParam.DRItem);
        }

        public void RemoveFromDR(InOutDRParam outParam){
            Item itemAfterOut = _itemService.Find(outParam.DRItem.Item.Id);

            if (outParam.InOrOut == InOrOut.In)
                InOutHelper.AddToAppopriateMeasurement(itemAfterOut, outParam.DRItem.Qty);
            else
                InOutHelper.SubtractToAppopriateMeasurement(itemAfterOut, outParam.DRItem.Qty);

            outParam.DRItem.Item = itemAfterOut;
            _itemService.Edit(itemAfterOut);

            _historyService.DeleteHistoryByDRAndItem(outParam.DRItem.DR.Id, itemAfterOut.Id);
            _drService.DeleteDRItem(outParam.DRItem.Id);
        }

        public void In(InOutParam inParam){
            Item itemAfterIn = _itemService.Find(inParam.Item.Id);
            double oldQty = itemAfterIn.GetAppropriateQuantity;

            InOutHelper.AddToAppopriateMeasurement(itemAfterIn, inParam.Qty);
           
            double newQty = itemAfterIn.GetAppropriateQuantity;

            var p = CreateHistoryParameter(inParam, oldQty, newQty);
            ItemHistory h = InOutHelper.MakeHistory(p);
            _historyService.Add(h);
            _itemService.Edit(itemAfterIn);
        }

        public void Out(InOutParam outParam)
        {
            Item itemAfterOut = _itemService.Find(outParam.Item.Id);
            double oldQty = itemAfterOut.GetAppropriateQuantity;

            InOutHelper.SubtractToAppopriateMeasurement(itemAfterOut, outParam.Qty);

            double newQty = itemAfterOut.GetAppropriateQuantity;

            var p = CreateHistoryParameter(outParam, oldQty, newQty);
            ItemHistory h = InOutHelper.MakeHistory(p);
            _historyService.Add(h);
            _itemService.Edit(itemAfterOut);
        }

        public void InOutGoodItems(ReturnInOutParam inOutParam){
            Item ItemAfterInOrOut = _itemService.Find(inOutParam.ItemId);
            double oldQty = ItemAfterInOrOut.GetAppropriateQuantity;

            if (inOutParam.InOrOut == InOrOut.In)
                InOutHelper.AddToAppopriateMeasurement(ItemAfterInOrOut, inOutParam.ReturnedItem.Qty);
            else
                InOutHelper.SubtractToAppopriateMeasurement(ItemAfterInOrOut, inOutParam.ReturnedItem.Qty);

            double newQty = ItemAfterInOrOut.GetAppropriateQuantity;
            
            _itemService.Edit(ItemAfterInOrOut);

            if (inOutParam.InOrOut == InOrOut.In){
                var p = CreateHistoryParameterForReturnedItems(inOutParam, oldQty, newQty);
                ItemHistory h = InOutHelper.MakeHistory(p);
                _historyService.Add(h);

                _returnedHistoryService.AddReturnedGoodItem(inOutParam.ReturnedItem.ReturnedHistory.Id,
                    inOutParam.ReturnedItem);
            }
            else{
                _returnedHistoryService.DeleteGoodItem(inOutParam.ReturnedItem.Id);
                _historyService.DeleteReturnedHistoryByDRAndItem(inOutParam.DrId
                    , inOutParam.ItemId);
            }
        }

        public void InOutScrapItems(ScrapInOutParam inOutparam){
            if (inOutparam.InOrOut == InOrOut.In){
                _returnedHistoryService.AddScrap(inOutparam.Scrap.ReturnedHistory.Id, inOutparam.Scrap);
            }
            else{
                _returnedHistoryService.DeleteScrapItem(inOutparam.Scrap.Id);
            }
        }

        private static HistoryParam CreateHistoryParameter(InOutParam param, double oldQty, double newQty){
            HistoryParam p = new HistoryParam();
            p.InOrOut = param.InOrOut;
            p.ItemToMonitor = param.Item;
            p.OldQty = oldQty;
            p.NewQty = newQty;

            p.InOutQty = param.Qty;
            p.Note = param.Note;
            p.Name = param.Name;

            return p;
        }

        private static HistoryParam CreateHistoryParameterWithDr(InOutDRParam inParam
            , double oldQty, double newQty){
            HistoryParam p = new HistoryParam();
            p.InOrOut = inParam.InOrOut;
            p.ItemToMonitor = inParam.DRItem.Item;
            p.OldQty = oldQty;
            p.NewQty = newQty;

            p.InOutQty = inParam.DRItem.Qty;
            p.Note = inParam.Note;
            p.DR = inParam.DRItem.DR;
            p.Name = inParam.Name;
            
            if (inParam.DRItem.DR != null)
                p.Type = inParam.DRItem.DR.Type;

            return p;
        }

        private static HistoryParam CreateHistoryParameterForReturnedItems(ReturnInOutParam param
            , double oldQty, double newQty){
            HistoryParam p = new HistoryParam();
            p.InOrOut = param.InOrOut;
            p.ItemToMonitor = param.ReturnedItem.Item;
            p.OldQty = oldQty;
            p.NewQty = newQty;

            p.InOutQty = param.ReturnedItem.Qty;
            p.Note = param .Note;
            p.DR = param.ReturnedItem.ReturnedHistory.DR;
            p.IsMistaken = true;
            p.Name = param.Name;

            return p;
        }
    }
}