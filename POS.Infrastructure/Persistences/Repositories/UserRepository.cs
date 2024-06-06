using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SistemaCreditos2Context _context;
        public UserRepository(SistemaCreditos2Context context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<User>> ListUsers(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<User>();

            var users = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        users = users.Where(x => x.UserName!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        users = users.Where(x => x.Email!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                users = users.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                users = users.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await users.CountAsync();
            response.Items = await Ordering(filters, users, !(bool)filters.Download).ToListAsync();
            
            return response;
        }

        public async Task<User> UserByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email!.Equals(email));
            return user!;
        }

    }
}
