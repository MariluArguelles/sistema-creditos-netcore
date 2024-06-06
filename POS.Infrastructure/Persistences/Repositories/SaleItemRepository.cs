using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class SaleItemRepository : GenericRepository<SaleItem>, ISaleItemRepository
    {
        private readonly SistemaCreditos2Context _context;

        public SaleItemRepository(SistemaCreditos2Context context) : base(context) {
            _context = context;
        }
        public async Task<BaseEntityResponse<SaleItem>> ListSaleItems(int saleId, BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<SaleItem>();

            var saleItems = _context.SaleItems
                .Include(a => a.Product)
                .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.Sale.Id == saleId);

               if (filters.Sort is null) filters.Sort = "Id";
               response.TotalRecords = await saleItems.CountAsync();
               response.Items = await Ordering(filters, saleItems, !(bool)filters.Download).ToListAsync();

            return response;
        }


        //public async Task<BaseEntityResponse<SaleItem>> ListSaleItems(int saleId, BaseFiltersRequest filters)
        //{
        //    var response = new BaseEntityResponse<SaleItem>();

        //    //MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA//
        //    var saleItem = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.Sale.Id == saleId);
        //    //MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA////MAGIA//
        //    if (filters.Sort is null) filters.Sort = "Id";
        //    response.TotalRecords = await saleItem.CountAsync();
        //    response.Items = await Ordering(filters, saleItem, !(bool)filters.Download).ToListAsync();
        //    return response;
        //}
    }
}
