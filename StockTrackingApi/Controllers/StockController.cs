using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockTrackingApi.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace StockTrackingApi.Controllers
{   //web api controller
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        static ProductObsolate products = new ProductObsolate();

        /*
         * post methodu
         * eklenen ürünü json olarak geri döndürür.
         * 
         */

        private readonly ApplicationDbContext _db;

        public StockController(ApplicationDbContext db)
        {
            _db = db;
        }

        //CRUD - Create, Update, Read, Delete

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null) return BadRequest(new { Error = "Not found"});

            return Ok(product);
        }

        public record ListRequest(string? Query, int Skip = 0, int Take = 0);

        //Page = 1, PageSize = 10

        public record ListResult(IEnumerable<Product> Data, int Count);

        [HttpPost("list")]
        public async Task<IActionResult> ListAsync(ListRequest request)
        {
            var data = _db.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                data = data.Where(x => (x.Name != null && x.Name.Contains(request.Query)) || x.Barcode.Contains(request.Query) || (x.Description != null && x.Description.Contains(request.Query)));
            }

            //SELECT * FROM Products
            //WHERE (NOT Name IS NULL AND Name LIKE '%abc%') OR Barcode LIKE '%abc%' OR (NOT Descriptin IS NULL AND Description LIKE '%abc%')

            var count = await data.CountAsync();

            data = data.OrderBy(x => x.Name);

            if (request.Skip > 0) data = data.Skip(request.Skip);

            var take = request.Take;

            if (take < 2) take = 2;
            if (take > 50) take = 50;

            data = data.Take(take);

            //SELECT TOP(10) * FROM Products
            //WHERE (NOT Name IS NULL AND Name LIKE '%abc%') OR Barcode LIKE '%abc%' OR (NOT Descriptin IS NULL AND Description LIKE '%abc%')
            //ORDER BY Name ASC


            var result = await data.ToListAsync();

            return Ok(new ListResult(result, count));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            var productOnDb = await _db.Products.FindAsync(product.Id);

            if (productOnDb == null) return BadRequest(new { Error = $"Product with id: {product.Id} not found" });

            productOnDb.StockCout = product.StockCout;
            productOnDb.Name = product.Name ?? string.Empty;
            productOnDb.Description = product.Description ?? string.Empty;
            productOnDb.Barcode = product.Barcode ?? string.Empty;
            productOnDb.ProductImageUrl = product.ProductImageUrl;

            await _db.SaveChangesAsync();

            return Ok(productOnDb);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            var product = await _db.Products.FindAsync(id);

            if (product == null) return Ok();

            _db.Products.Remove(product);

            await _db.SaveChangesAsync();

            return Ok();
        }




        [HttpPost("addProduct")]
        //Ürün ekleme
        public IActionResult addProduct([FromBody] ProductObsolate product)
        {
            product = products.add(product);

            return Ok(product);
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("getAllProduct")]
        //Bütün ürünleri çekme
        public IActionResult getAllProduct()
        {

            return Ok(products.GetAllProduct());

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
        public IActionResult updateProduct([FromBody] ProductObsolate product)
        {
            try
            {

                product = products.update(product);

                return new JsonResult(product);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ProductObsolate());
            }
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
        [Route("updateProductByStock/{id}/{stock}")]
        //Barkod'a ye göre ürün çekme
        public IActionResult updateProductByStock(int id, int stock)
        {

            return new JsonResult(products.updateByStock(id, stock));

        }
    }
}