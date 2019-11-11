using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace Data
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public static Product Get(int Id, DatabaseEntities db)
        {
            if (Id == 0) return new Product()
            {
                Id = 0
            };
            return db.Products.SingleOrDefault(i => i.Id == Id);
        }

        public static int Save(Product product, DatabaseEntities db)
        {
            if(product.Id == 0)
            {
                db.Products.Add(product);
            }
            else
            {
                db.Products.Attach(product);
                db.Entry(product).State = EntityState.Modified;
            }
            return db.SaveChanges();
        }

        public static List<Product> GetList(DatabaseEntities db)
        {
            return db.Products.ToList();
        }

        public static int Delete(int Id, DatabaseEntities db)
        {
            Product product = db.Products.SingleOrDefault(i => i.Id == Id);
            if(product == null) return 0;
            db.Products.Remove(product);
            return db.SaveChanges();
        }
    }

    public class ProductMetadata
    {
        public int Id { get; set; }
        [LocalizedValidationAttribute("Product", "SKU")]
        [LocalizedDisplayName("Product", "SKU")]
        public string Sku { get; set; }
        [LocalizedValidationAttribute("Product", "NAME")]
        [LocalizedDisplayName("Product", "NAME")]
        public string Name { get; set; }
        [LocalizedDisplayName("Product", "DESCRIPTION")]
        public string Description { get; set; }
        [LocalizedValidationAttribute("Product", "PRICE")]
        [LocalizedDisplayName("Product", "PRICE")]
        public decimal Price { get; set; }
    }
}