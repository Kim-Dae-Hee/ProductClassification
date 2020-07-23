using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClassification.Data
{
    public class ProductData
    {
        public ProductClassificationEntities CreateContext()
        {
            ProductClassificationEntities context = new ProductClassificationEntities();
            context.Configuration.ProxyCreationEnabled = false;
            context.Database.Log = x => Console.WriteLine(x);

            return context;
        }

        public List<Product> GetAll()
        {
            ProductClassificationEntities context = CreateContext();

            return context.Products.ToList();
        }

        public Product Get(int productId)
        {
            ProductClassificationEntities context = CreateContext();

            return context.Products.FirstOrDefault(x => x.ProductId == productId);
        }

        public int GetCount()
        {
            ProductClassificationEntities context = CreateContext();

            return context.Products.Count();
        }

        public void Insert(Product product)
        {
            ProductClassificationEntities context = CreateContext();

            context.Entry(product).State = EntityState.Added;

            context.SaveChanges();
        }

        public void Update(Product product)
        {
            ProductClassificationEntities context = CreateContext();

            context.Entry(product).State = EntityState.Modified;
                        
            context.SaveChanges();
        }

        public void Delete(int productId)
        {
            Product product = Get(productId);

            if (product == null)
                return;

            Delete(product);

        }
        public void Delete(Product product)
        {
            ProductClassificationEntities context = CreateContext();

            context.Entry(product).State = EntityState.Deleted;

            context.SaveChanges();
        }

    }

}
