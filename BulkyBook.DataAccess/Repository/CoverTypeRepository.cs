﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DbSet = _db.CoverType;
        }
        public void Update(CoverType coverType)
        {
            var obj = DbSet.FirstOrDefault(item => item.Id == coverType.Id);
            if (obj != null)
            {
                obj.Name = coverType.Name;
                _db.SaveChanges();
            }
        }
    }
}
