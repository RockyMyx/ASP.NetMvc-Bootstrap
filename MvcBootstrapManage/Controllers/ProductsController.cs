using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MvcBootstrap.Models;
using System.Net;

namespace MvcBootstrap.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]  
        {  
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },  
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },  
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }  
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }
    } 
}