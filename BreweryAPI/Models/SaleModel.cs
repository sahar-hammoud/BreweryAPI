using System.ComponentModel.DataAnnotations;
namespace BreweryAPI.Models
{
    public class SaleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "BeerID is required")]
        public int? BeerID { get; set; }

        [Required(ErrorMessage = "WholesalerID is required")]
        public int? WholesalerID { get; set; }

        [Required(ErrorMessage = "SalesQuantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Sales Quantity")]
        public int SalesQuantity { get; set; }
    }
}
