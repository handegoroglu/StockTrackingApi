using Newtonsoft.Json;
using System.Text;

namespace StockTrackingApi
{
    public class Product
    {
        //Ürün özellikleriyle ilgili değiskenler oluşturuldu
        public int id { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";
        public string barcode { get; set; } = "";
        public string image { get; set; } = "";
        public int stock { get; set; }
        public double unitPrice { get; set; }

        //Ürünlerin tutulacağı bir liste oluşturuldu
        List<Product> products { get; set; }
        public Product()
        {

            products = new List<Product>();

            productGetAllFile();

            if(products == null)
                products = new List<Product>();
        }

        //Ürünlerin eklenmesi için kullanılan fonksiyon (ürünlerin id'si fonksiyon içerisinde otomatik olarak verilir)
        public Product add(Product product)
        {
            if (products.Count() > 0)
            {
                var maxId = products.Max(product => product.id);
                product.id = maxId + 1;
            }
            else
                product.id = 0;

            products.Add(product);

            productSaveAllFile();

            return product;
        }

        //Listedeki ürünleri ID'lere göre silinme fonksiyonu
        public Product deleteById(int id)
        {
            var product = products.FirstOrDefault(product => product.id == id);
            if (product == null)
                return new Product();

            products.Remove(product);

            productSaveAllFile();

            return product;
        }

        //Listedeki ürünleri barkodlarına göre silme fonksiyonu
        public Product deleteByBarcode(string barcode)
        {
            var product = products.FirstOrDefault(product => product.barcode == barcode);
            if (product == null)
                return new Product();

            products.Remove(product);

            productSaveAllFile();

            return product;
        }
        public Product update(Product newProduct)
        {
            var product = products[newProduct.id];
            if (product == null)
                return newProduct;

            product.barcode = newProduct.barcode;
            product.stock = newProduct.stock;
            product.unitPrice = newProduct.unitPrice;
            product.image = newProduct.image;
            product.name = newProduct.name;
            product.description = newProduct.description;

            productSaveAllFile();


            return product;
        }
        public Product updateByStock(int id, int stock)
        {
            var product = products.FirstOrDefault(i=> i.id==id);
            if (product == null)
                return new Product();
            product.stock = stock;

            productSaveAllFile();

            return product;
        }

        public List<Product> GetAllProduct()
        {
            return products;
        }
        public Product getProductById(int id)
        {

            var product = products.FirstOrDefault(product => product.id == id);
            if (product == null)
               return new Product();
            else 
                return product;
        }
        public Product getProductByBarcode(string barcode)
        {

            var product = products.FirstOrDefault(product => product.barcode == barcode);
            if (product == null)
                return new Product();
            else
                return product;
        }
        public static string settingsFileAppPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hande\\StockTracking\\products.json";
        void productSaveAllFile()
        {
         /*   try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(settingsFileAppPath);
                if (!file.Exists)
                {
                    file.Create();
                }

                var fileStream = file.OpenWrite();

                var json = JsonConvert.SerializeObject(products);

                byte[] buffer = new byte[json.Length];
                fileStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception)
            {
            }*/
        }
        void productGetAllFile()
        {
        /*    try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(settingsFileAppPath);
                if (!file.Exists)
                {
                    file.Create();
                }

                var fileStream = file.OpenRead();

                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);

                JsonConvert.DeserializeObject<List<Product>>(Encoding.Default.GetString(buffer));
            }
            catch (Exception)
            {
            }*/
        }
    }
}