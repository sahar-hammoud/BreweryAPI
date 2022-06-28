using BreweryAPI.Data;

namespace BreweryAPI.Models
{
    public class ManagementRepository : IManagementRepository
    {
        private ApplicationDbContext appDBContext;

        public ManagementRepository(ApplicationDbContext dbContext)
        {
            appDBContext = dbContext;
        }

        public tblClient GetClientById(int clientId)
        {
            return appDBContext.tblClients.FirstOrDefault(x => x.Id == clientId);
        }

        public tblBeer GetBeerById(int Id)
        {
            return appDBContext.tblBeers.FirstOrDefault(x => x.Id == Id);
        }

        public tblBeer GetBeerByBrewery(int Id)
        {
            return appDBContext.tblBeers.FirstOrDefault(x => x.BreweryID == Id);
        }
        public tblBrewery GetBreweryById(int Id)
        {
            return appDBContext.tblBreweries.FirstOrDefault(x => x.Id == Id);
        }

        public tblWholesaler GetWholesalerById(int Id)
        {
            return appDBContext.tblWholesalers.FirstOrDefault(x => x.Id == Id);
        }
        public tblWholesalersStock CheckWholesalersStocks(int wholesalerID, int beerID)
        {
            return appDBContext.tblWholesalersStocks.FirstOrDefault(x => x.WholesalerID == wholesalerID && x.BeerID == beerID);
        }

        public tblSale CheckSoldByWholesaler(int wholesalerID, int beerID)
        {
            return appDBContext.tblSales.FirstOrDefault(x => x.WholesalerID == wholesalerID && x.BeerID == beerID);
        }

        public tblOrder CheckDuplicateOrder(int wholesalerID, int beerID, int clientId)
        {
            return appDBContext.tblOrders.FirstOrDefault(x => x.WholesalerID == wholesalerID && x.BeerID == beerID && x.ClientID == clientId);
        }
    }
}
