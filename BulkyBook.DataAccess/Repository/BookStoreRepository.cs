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
    public class BookStoreRepository : Repository<BookStore>, IBookStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public BookStoreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.BookStore;
        }

        public void Update(BookStore bookStore)
        {
            var obj = DbSet.FirstOrDefault(item => item.Id == bookStore.Id);
            if (obj != null)
            {
                obj.Name = bookStore.Name;
                obj.PhoneNumber = bookStore.PhoneNumber;
                obj.PostalCode = bookStore.PostalCode;
                obj.State = bookStore.State;
                obj.StreetAddress = bookStore.StreetAddress;
                obj.IsAuthorizedBookStore = bookStore.IsAuthorizedBookStore;

                _db.SaveChanges();
            }
        }
    }
}
