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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.Category;
        }

        public void Update(Category category)
        {
            var obj = DbSet.FirstOrDefault(item => item.Id == category.Id);
            if (obj != null)
            {
                obj.Name = category.Name;
                _db.SaveChanges();
            }
        }
    }
}
