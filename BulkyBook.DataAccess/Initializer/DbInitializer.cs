﻿using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }catch(Exception exception)
            {

            }

            if (_db.Roles.Any(item => item.Name == UserRole.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(UserRole.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserRole.Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserRole.User_Individual)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserRole.User_Company)).GetAwaiter().GetResult();

            IConfiguration Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true)
                .Build();
            string adminPassword = Config["AdminPassword"];

            var user = new ApplicationUser()
            {
                UserName = "haicao2805@gmail.com",
                Email = "haicao2805@gmail.com",
                EmailConfirmed = true,
                Name = "Cao Chi Hai"
            };

            var result = _userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, UserRole.Admin);
            }
        }
    }
} 
