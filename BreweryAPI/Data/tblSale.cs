using System.ComponentModel.DataAnnotations;
namespace BreweryAPI.Data
{
    public class tblSale
    {
        public int Id { get; set; }
        public int BeerID { get; set; }  
        public int WholesalerID { get; set; }
        public int SalesQuantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
