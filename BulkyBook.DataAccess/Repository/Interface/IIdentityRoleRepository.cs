using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Interface
{
    public interface IIdentityRoleRepository : IRepository<IdentityRole>
    {
    }
}
