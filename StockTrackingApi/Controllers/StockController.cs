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
         * eklenen �r�n� json olarak geri d�nd�r�r.
         * 
         */


        [HttpPost]
        [Produces("application/json")]
        [Route("addProduct")]

        //�r�n ekleme
        public IActionResult addProduct ([FromBody] Product product)
        {
            product = products.add(product);

            return new JsonResult(product);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Route("getAllProduct")]
        //B�t�n �r�nleri �ekme
        public IActionResult getAllProduct()
        {

         return new JsonResult(products.GetAllProduct());

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("getProductById/{id}")]
        //Id'ye g�re �r�n �ekme
        public IActionResult getProductById(int id)
        {

            return new JsonResult(products.getProductById(id));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("getProductByBarcode/{barcode}")]
        //Barkod'a ye g�re �r�n �ekme
        public IActionResult getProductByBarcode(string barcode)
        {

            return new JsonResult(products.getProductByBarcode(barcode));

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("updateProduct")]

        //�r�n g�ncelleme
        public IActionResult updateProduct([FromBody] Product product)
        {
            product = products.update(product);

            return new JsonResult(product);
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("deleteProductById/{id}")]
        //Id'ye g�re �r�n �ekme
        public IActionResult deleteProductById(int id)
        {

            return new JsonResult(products.deleteById(id));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("deleteProductByBarcode/{barcode}")]
        //Barkod'a ye g�re �r�n �ekme
        public IActionResult deleteProductByBarcode(string barcode)
        {

            return new JsonResult(products.deleteByBarcode(barcode));

        }
        [HttpGet]
        [Produces("application/json")]
        [Route("updateProductByStock/{id}/{stock}")]
        //Barkod'a ye g�re �r�n �ekme
        public IActionResult updateProductByStock(int id, int stock)
        {

            return new JsonResult(products.updateByStock(id,stock));

        }
    }
}