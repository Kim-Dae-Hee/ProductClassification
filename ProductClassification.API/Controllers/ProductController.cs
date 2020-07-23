using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ProductClassification.Data;

namespace ProductClassification.API.Controllers
{
    public class ProductController : ApiController
    {
        private ProductClassificationEntities db = new ProductClassificationEntities();

        // GET: api/Product
        public List<Product> GetProducts()
        {
            return DataRepository.Product.GetAll();
        }

        // GET: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = DataRepository.Product.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            Product updateProduct = DataRepository.Product.Get(id);

            product.CheckedDate = updateProduct.CheckedDate;
            try
            {
                DataRepository.Product.Update(product);
            }
            catch(Exception ex)
            {
                string message = ex.GetMessageRecursively();
                return BadRequest(message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            product.CheckedDate = DateTime.Now;
            try
            {
                DataRepository.Product.Insert(product);
            }
            catch (Exception ex)
            {
                string message = ex.GetMessageRecursively();
                return BadRequest(message);
            }

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = DataRepository.Product.Get(id);

            if (product == null)
                return NotFound();

            try
            {
                DataRepository.Product.Delete(product);
            }
            catch (Exception ex)
            {
                string message = ex.GetMessageRecursively();
                return BadRequest(message);
            }

            return Ok(product);
        }
    }
}