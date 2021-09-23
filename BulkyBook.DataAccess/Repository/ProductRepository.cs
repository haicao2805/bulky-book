using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Product;
        }
        public void Update(Product product)
        {
            var obj = DbSet.FirstOrDefault(item => item.Id == product.Id);
            if (obj != null)
            {
                if (product.ImageUrl != null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
                obj.Title = product.Title;
                obj.Description = product.Description;
                obj.ISBN = product.ISBN;
                obj.Author = product.Author;
                obj.Price = product.Price;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;
                obj.ListPrice = product.ListPrice;
                obj.CategoryId = product.CategoryId;
                obj.CoverTypeId = product.CoverTypeId;

                _db.SaveChanges();
            }
        }
    }
}
