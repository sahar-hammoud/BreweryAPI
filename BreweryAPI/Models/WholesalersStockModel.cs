using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class WholesalersStockModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "WholesalerID is required")]
        public int? WholesalerID { get; set; }

        [Required(ErrorMessage = "BeerID is required")]
        public int? BeerID { get; set; }

        [Required(ErrorMessage = "StockQuantity is required")]
        public int StockQuantity { get; set; }
    }
}
