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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.BookStore;
        }

        public void Update(Company company)
        {
            var obj = DbSet.FirstOrDefault(item => item.Id == company.Id);
            if (obj != null)
            {
                obj.Name = company.Name;
                obj.PhoneNumber = company.PhoneNumber;
                obj.PostalCode = company.PostalCode;
                obj.State = company.State;
                obj.StreetAddress = company.StreetAddress;
                obj.IsAuthorizedBookStore = company.IsAuthorizedBookStore;

                _db.SaveChanges();
            }
        }
    }
}
