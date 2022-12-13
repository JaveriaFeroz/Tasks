using System;
using System.Text.Json;

namespace ObjectToJSONConversion
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
    }

    class Program
    {
        public static void Main()
        {
            Product prod = new Product() { ProductId = 101, Category = "Mobile Phone", ProductName = "Samsung" };

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<Product>(prod, opt);
            Console.WriteLine(strJson);
        }
    }
}