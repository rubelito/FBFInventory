namespace FBFInventory.Infrastructure.Dto
{
    public class ItemSearchParam
    {
        public string Name { get; set; }

        public bool ShouldFilterByStatus { get; set; }
        public bool ActiveOnly { get; set; }
        public bool ShouldIncludeSupplierAndCustomer { get; set; }

        public ItemSearchCriteria Criteria { get; set; }
        public OrdeBy OrberyBy { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
