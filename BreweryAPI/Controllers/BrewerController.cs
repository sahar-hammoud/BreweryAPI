using BreweryAPI.Data;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewerController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        IManagementRepository repository;
        public BrewerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
             repository = new ManagementRepository(_dbContext);
        }

        [HttpGet("GetBeers")]
        public IActionResult Get(int id)
        {
            try
            { 
                if(id == 0)
                {
                    return StatusCode(400, "The id of brewery is required");
                } 
                else 
                {
                    var beer  = repository.GetBeerByBrewery(id);
                    if (beer == null)
                    {
                        return StatusCode(400, "the value " + id + " is not valid");
                    }

                    return Ok(beer);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
            }
        }

        [HttpPost("CreateBeer")]    
        public IActionResult Create([FromBody] BeerModel model)
        {
            try
            {

                tblBeer beer = new tblBeer();
                beer.Name = model.Name;
                beer.BreweryID = model.BreweryID;
                beer.AlcoholContent = model.AlcoholContent.Value;
                beer.Price = model.Price;

               
                var GetBreweryById = repository.GetBeerByBrewery(model.BreweryID); 
                
                if(GetBreweryById == null)
                {
                    return StatusCode(400, "the value " + model.BreweryID + " is not valid");
                } 
                else
                {
                    _dbContext.tblBeers.Add(beer);
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error has occured");
            }

            var beers = _dbContext.tblBeers.ToList();
            return Ok(beers);
        }

        [HttpPost("AddSale")]
        public IActionResult SaleBeer([FromBody] SaleModel model)
        {
            try
            {
                var foundBeer = repository.GetBeerById(model.BeerID.Value);
                if (foundBeer == null)
                {
                    return StatusCode(400, "the value " + model.BeerID.Value + " is not valid");
                }

                var foundWhoulsaler = repository.GetWholesalerById(model.WholesalerID.Value);
                if (foundWhoulsaler == null)
                {
                    return StatusCode(400, "the value " + model.WholesalerID + " is not valid");
                }

                tblSale sale = new tblSale();
                sale.BeerID = model.BeerID.Value;
                sale.WholesalerID = model.WholesalerID.Value;
                sale.SalesQuantity = model.SalesQuantity;
                sale.Date = DateTime.Now;
                var beerPrice = _dbContext.tblBeers.First(x => x.Id == model.BeerID).Price;
                sale.TotalPrice = model.SalesQuantity * beerPrice;

                _dbContext.tblSales.Add(sale);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
            }

            var sales = _dbContext.tblSales.ToList();
            return Ok(sales);
        }

        [HttpPut("UpdateStockQuantity")]
        public IActionResult Update([FromBody] WholesalersStockModel model)
        {
            try {
             
                var foundBeer = repository.GetBeerById(model.BeerID.Value);
                if (foundBeer == null)
                {
                    return StatusCode(400, "the value "+model.BeerID+" is not valid");
                }

                var foundWhoulsaler = repository.GetWholesalerById(model.WholesalerID.Value);
                if (foundWhoulsaler == null)
                {
                    return StatusCode(400, "the value " +model.WholesalerID+" is not valid");
                }

                var updateStock = repository.CheckWholesalersStocks(model.WholesalerID.Value, model.BeerID.Value);

                if (updateStock == null)
                {
                    return StatusCode(400, "No Stock found");
                }

                updateStock.StockQuantity = model.StockQuantity;
                _dbContext.Entry(updateStock).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
             }

            var wholesalerStock = _dbContext.tblWholesalersStocks.ToList();
            return Ok(wholesalerStock);
        }

        [HttpDelete("DeleteBeer/{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return StatusCode(400, "The id of brewery is required");
                }
                else
                {
                    var beer = repository.GetBeerById(Id);
                    if (beer == null)
                    {
                        return StatusCode(400, "the value " + Id + " is not valid");
                    }

                    _dbContext.Entry(beer).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
            }

            var beers = _dbContext.tblBeers.ToList();
            return Ok(beers);
        }

        [HttpGet("GetQuote")]
        public IActionResult GetQuote([FromBody] OrderModel model)
        {
            try
            {
                var isValid = CheckValidateRequest(model);

                if (isValid.StatusCode == 200)
                {
                    var quantityOrdered = model.QuantityOrdered;
                    var beerPrice = _dbContext.tblBeers.First(x => x.Id == model.BeerID).Price;
                    var price = quantityOrdered * beerPrice;
                    var quote = price;

                    tblOrder order = new tblOrder();
                    order.ClientID = model.ClientID.Value;
                    order.BeerID = model.BeerID.Value;
                    order.WholesalerID = model.WholesalerID.Value;
                    order.QuantityOrdered = model.QuantityOrdered.Value;


                    if (quantityOrdered > 20)
                    {
                        quote = price - (price * 20 / 100);
                    }
                    else if (quantityOrdered > 10 && quantityOrdered < 20)
                    {
                        quote = price - (price * 10 / 100);
                    }
                    else
                    {
                        quote = price;
                    }

                    order.QuotePrice = quote.Value;
                    order.OrderDate = DateTime.Now;
                    _dbContext.tblOrders.Add(order);
                    _dbContext.SaveChanges();

                    return Ok("price:" + price + " ,"+ "Quote:" + quote);
                }
                else
                {                  
                     return BadRequest(isValid.Value);
                }

            }
            catch (Exception)
            {
               return StatusCode(500, "An error has occured");
            }

        }
        private ObjectResult CheckValidateRequest(OrderModel model)
        {

            var foundClient = repository.GetClientById(model.ClientID.Value);
            if (foundClient == null)
            {
                return StatusCode(400, "the value " + model.ClientID + " is not valid");
            }

            var foundBeer = repository.GetBeerById(model.BeerID.Value);
            if (foundBeer == null)
            {
                return StatusCode(400, "the value " + model.BeerID + " is not valid");
            }

            var foundWhoulsaler = repository.GetWholesalerById(model.WholesalerID.Value);
            if (foundWhoulsaler == null)
            {
                return StatusCode(400, "the value " + model.WholesalerID + " is not valid");
            }

            var beerSold = repository.CheckSoldByWholesaler(model.WholesalerID.Value, model.BeerID.Value);
            if (beerSold == null)
            {
                return StatusCode(400, "The beer doesn't sold by this wholesaler ");
            }

            var checkDuplicateOrder = repository.CheckDuplicateOrder(model.WholesalerID.Value, model.BeerID.Value, model.ClientID.Value);

            if (checkDuplicateOrder != null && checkDuplicateOrder.OrderDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
            {
                string text = "Sorry, duplicate order!" + System.Environment.NewLine + "We recommend you cancel the order if it needs any changes";
                return StatusCode(400, text);
            } 

            var updateStock = repository.CheckWholesalersStocks(model.WholesalerID.Value, model.BeerID.Value);            
            if (updateStock == null)
            {
                return StatusCode(400, "The product is out of stock");
            }
            else if (updateStock.StockQuantity < model.QuantityOrdered)
            {
                return StatusCode(400, "Sorry, you have reached maximum available quantity");
            }

             return StatusCode(200, "Success");
        }

    }
}
