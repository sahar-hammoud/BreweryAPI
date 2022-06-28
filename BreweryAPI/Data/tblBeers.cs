using System.Data.Entity;

namespace BreweryAPI.Data
{  
    public class tblBeer
    {
        public int Id { get; set; }
        public int BreweryID { get; set; }
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public double Price { get; set; }
    }
}
