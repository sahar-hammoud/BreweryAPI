using BreweryAPI.Data;
namespace BreweryAPI.Models
{
    public interface IManagementRepository
    {
        tblClient GetClientById(int clientId);
        tblBeer GetBeerById(int beerId);
        tblBeer GetBeerByBrewery(int breweryId);
        tblBrewery GetBreweryById(int breweryId);
        tblWholesaler GetWholesalerById(int wholesalerId);
        tblWholesalersStock CheckWholesalersStocks(int wholesalerID, int beerID);
        tblSale CheckSoldByWholesaler (int wholesalerID, int beerID);
        tblOrder CheckDuplicateOrder(int wholesalerID, int beerID, int clientId);
    }
}
