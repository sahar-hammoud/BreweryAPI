namespace BreweryAPI.Data
{
    public class tblOrder
    {
        public int Id { get; set; }
        public int ClientID { get; set; }
        public int WholesalerID { get; set; }
        public int BeerID { get; set; }
        public int QuantityOrdered { get; set; }
        public double QuotePrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
