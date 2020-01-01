using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData.Models;

namespace OData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ODataController
    {
        private List<Product> products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Bread",
                Quantity = 100,
            },
            new Product()
            {
                Id = 2,
                Name = "Yaourt",
                Quantity = 120,
            },
            new Product()
            {
                Id = 3,
                Name = "Pizza",
                Quantity = 110,
            }
        };

        [EnableQuery]
        public List<Product> Get()
        {
            return products;
        }
    }
}