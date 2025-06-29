using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // new line added to push code to github
    public class OrderController : ControllerBase
    {
        private static List<Order> orders = new List<Order>
        {
            new Order { Id = 1, ProductId = 1, Quantity = 2, CustomerName = "Fahad" },
            new Order { Id = 2, ProductId = 2, Quantity = 1, CustomerName = "Ayesha" }
        };

        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderWithProduct(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            // 🔁 Call ProductService for product details
            string productServiceUrl = "https://localhost:44343/api/CRUDoperations/" + order.ProductId;

            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>(productServiceUrl);

                return Ok(new
                {
                    order.Id,
                    order.CustomerName,
                    order.Quantity,
                    Product = product
                });
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Product service is unavailable.");
            }
        }
    }
}




//using Microsoft.AspNetCore.Mvc;
//using OrderService.Models;

//namespace OrderService.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class OrderController : ControllerBase
//    {
//        private static List<Order> orders = new List<Order>
//        {
//            new Order { Id = 1, ProductId = 1, Quantity = 2, CustomerName = "Fahad" },
//            new Order { Id = 2, ProductId = 2, Quantity = 1, CustomerName = "Ayesha" }
//        };

//        [HttpGet]
//        public ActionResult<IEnumerable<Order>> GetOrders()
//        {
//            return Ok(orders);
//        }

//        [HttpPost]
//        public ActionResult<IEnumerable<Order>> AddOrder(Order order)
//        {
//            orders.Add(order);
//            return Ok(orders);
//        }
//    }
//}
