namespace BreweryAPI.Data
{
    public class tblWholesalersStock
    {
        public int Id { get; set; }
        public int WholesalerID { get; set; }
        public int BeerID { get; set; }
        public int StockQuantity { get; set; }
    }
}
