using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace StockTrackingApi.Controllers
{   //web api controller
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
       static Product products = new Product();

        /*
         * post methodu
         * eklenen ürünü json olarak geri döndürür.
         * 
         */


        [HttpPost]
        [Produces("application/json")]
        [Route("addProduct")]

        //Ürün ekleme
        public IActionResult addProduct ([FromBody] Product product)
        {
            product = products.add(product);

            return new JsonResult(product);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Route("getAllProduct")]
        //Bütün ürünleri çekme
        public IActionResult getAllProduct()
        {

         return new JsonResult(products.GetAllProduct());

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("getProductById/{id}")]
        //Id'ye göre ürün çekme
        public IActionResult getProductById(int id)
        {

            return new JsonResult(products.getProductById(id));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("getProductByBarcode/{barcode}")]
        //Barkod'a ye göre ürün çekme
        public IActionResult getProductByBarcode(string barcode)
        {

            return new JsonResult(products.getProductByBarcode(barcode));

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("updateProduct")]

        //Ürün güncelleme
        public IActionResult updateProduct([FromBody] Product product)
        {
            product = products.update(product);

            return new JsonResult(product);
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("deleteProductById/{id}")]
        //Id'ye göre ürün çekme
        public IActionResult deleteProductById(int id)
        {

            return new JsonResult(products.deleteById(id));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("deleteProductByBarcode/{barcode}")]
        //Barkod'a ye göre ürün çekme
        public IActionResult deleteProductByBarcode(string barcode)
        {

            return new JsonResult(products.deleteByBarcode(barcode));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("updateProductByStock/{id}/{stock}")]
        //Barkod'a ye göre ürün çekme
        public IActionResult updateProductByStock(int id, int stock)
        {

            return new JsonResult(products.updateByStock(id,stock));

        }
    }
}