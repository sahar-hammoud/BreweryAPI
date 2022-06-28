using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ClientID is required")]
        public int? ClientID { get; set; }

        [Required(ErrorMessage = "WholesalerID is required")]
        public int? WholesalerID { get; set; }

        [Required(ErrorMessage = "BeerID is required")]
        public int? BeerID { get; set; }

        [Required(ErrorMessage = "QuantityOrdered is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Quantity")]
        public int? QuantityOrdered { get; set; }

    }
}
