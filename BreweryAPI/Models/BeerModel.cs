using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
    public class BeerModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The id of brewery is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid BreweryID")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "BreweryID must be numeric")]
        [ForeignKey("BreweryID")]
        public int BreweryID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a beer name")]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a alcohol percentage in beer")]
        public float? AlcoholContent { get; set; }

        [Required(ErrorMessage = "Please enter a beer price")]
        [Range(1, 9999.99)]
        public float Price { get; set; }
        public virtual BreweryModel Brewery { get; set; }
    }

}
