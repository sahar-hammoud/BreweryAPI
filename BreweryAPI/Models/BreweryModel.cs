using System.ComponentModel.DataAnnotations;
namespace BreweryAPI.Models
{
    public class BreweryModel
    {     
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<BeerModel> BeerModel { get; set; }

    }
}
