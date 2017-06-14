﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebAPIMongoDB.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIMongoDB.Controllers
{
    [Route("api/Product")]
    public class ProductApiController : Controller
    {
        DataAccess objds;

        public ProductApiController(DataAccess d)
        {
            objds = d;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return objds.GetProducts();
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var product = objds.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product p)
        {
            objds.Create(p);
            return new OkObjectResult(p);
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody]Product p)
        {
            var recId = new ObjectId(id);
            var product = objds.GetProduct(recId);
            if (product == null)
            {
                return NotFound();
            }

            objds.Update(recId, p);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = objds.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }

            objds.Remove(product.Id);
            return new OkResult();
        }
    }
}
