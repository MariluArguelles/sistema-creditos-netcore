﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Dtos.Sales.Request;
using POS.Application.Dtos.Sales.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Infrastructure.Persistences.Repositories;
using POS.Utilities.Statics;
using System.Data;
using System.Reflection;

namespace POS.Application.Services
{
        public class SaleApplication : GenericRepository<Sale>, ISaleApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtilidades _utilidades;

        private readonly SistemaCreditos2Context _context;
        
        public SaleApplication(IUnitOfWork unitOfWork, IMapper mapper,IUtilidades utilidades, SistemaCreditos2Context context):  base (context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilidades = utilidades;
            _context = context;
        }


        public async Task<BaseResponse<BaseEntityResponse<SaleResponseDto>>> ListSales(int customerId, BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<SaleResponseDto>>();
            try
            {
                var sales = await _unitOfWork.Sale.ListSales(customerId, filters);

                if (sales is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<SaleResponseDto>>(sales);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<BaseEntityResponse<SaleForPaymentsResponseDto>>> ListSalesForPayments(int customerId, BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<SaleForPaymentsResponseDto>>();

            var sales = await _unitOfWork.Sale.ListSalesForPayments(customerId, filters);

            try
            {
                List<Sale> sale = QuerySalesForPayments(sales.Items, customerId);

                BaseEntityResponse<SaleForPaymentsResponseDto> baseEntity = new BaseEntityResponse<SaleForPaymentsResponseDto>();
                baseEntity.TotalRecords = sale.Count;
                baseEntity.Items = Mapping(sale);

                if (sales is not null)
                {
                    response.IsSuccess = true;
                    response.Data = baseEntity;
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        private List<Sale> QuerySalesForPayments(List<Sale> sales, int customerId)
        {
            List<Sale> sale = sales
                  .Select(c => new Sale
                  {
                      Id=c.Id,
                      Total = c.Total,
                      SubTotal = c.SubTotal,
                      Balance = c.Balance,
                      Closed=c.Closed,
                      Paid=c.Paid,
                      AuditCreateDate = c.AuditCreateDate,
                      SaleItems = c.SaleItems.Select(si => new SaleItem
                      {
                          Id = si.Id,
                          Quantity = si.Quantity,
                          Product = si.Product,
                          Price = si.Price
                      }).ToList(),
                      Payments = c.Payments.Select(p => new Payment
                      {
                          Quantity = p.Quantity,
                          Balance = p.Balance,
                          PaymentDate = p.PaymentDate
                      }).ToList(),
                      AuditDeleteUser = c.AuditDeleteUser,
                      AuditDeleteDate = c.AuditDeleteDate,
                      CustomerId = c.CustomerId,
                  })
                  .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.CustomerId == customerId)
                  .ToList();

            return sale;

        }


        private List<SaleForPaymentsResponseDto> Mapping(List<Sale> sale)
        {
            var mappedSales = sale.Select(s => new SaleForPaymentsResponseDto
            {
                SaleId = s.Id,
                Total = s.Total.ToString(),
                SubTotal = s.SubTotal.ToString(),
                Balance = s.Balance.ToString(),
                Closed = s.Closed,
                Paid = s.Paid,
                AuditCreateDate = s.AuditCreateDate.ToString(),
                SaleItems = s.SaleItems.Select(si => new SaleItemResponse
                {
                    Id = si.Id,
                    Quantity = si.Quantity,
                    Price = si.Price,
                    Description = si.Product.Description.Trim(),
                }).ToList(),
                Payments = s.Payments.Select(p => new PaymentResponse
                {
                    Quantity = p.Quantity,
                    Balance = p.Balance,
                    PaymentDate = p.PaymentDate,
                }).ToList(),
                AuditDeleteUser = s.AuditDeleteUser,
                AuditDeleteDate = s.AuditDeleteDate,
                CustomerId = s.CustomerId,
            }).ToList();

            return mappedSales;
        }


        public async Task<BaseResponse<SaleResponseDto>> SaleById(int saleId)
        {
            var response = new BaseResponse<SaleResponseDto>();
            try
            {
                var product = await _unitOfWork.Sale.GetByIdAsync(saleId);
               
                if (product is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<SaleResponseDto>(product);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterSale(SaleRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            
            try
            {
                var sale = _mapper.Map<Sale>(requestDto);
                response.Data = await _unitOfWork.Sale.RegisterAsync(sale);

                if (response.Data)
                {
                    response.generatedId = sale.Id;
                    response.IsSuccess = true; //error? IsSucces a IsSuccess
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<bool>> EditSale(int saleId, SaleRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var saleEdit = await SaleById(saleId);

                if (saleEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                var sale = _mapper.Map<Sale>(requestDto);
                sale.Id = saleId;
                response.Data = await _unitOfWork.Sale.EditAsync(sale);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }
        
        public async Task<BaseResponse<bool>> RemoveSale(int saleId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var sale = await SaleById(saleId);

                if (sale.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                response.Data = await _unitOfWork.Sale.RemoveAsync(saleId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }


     
        public async Task<List<DataTable>> GetBill(int saleId)
        {
            var response = new BaseResponse<BaseEntityResponse<SaleResponseDto>>();
            List<DataTable> lis = new List<DataTable>();
            
            try
            {
                //VENTA
                var lista = _context.Sales
                .Include(a => a.SaleItems)
                    .ThenInclude(b => b.Product)
                .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.Id == saleId).ToList();

                //idCliente
                var tablaVenta = _utilidades.ToDataTable(lista);

                int customerId = 0;
                customerId = lista[0].CustomerId;

                //CLIENTES
                var lista2 = _context.Customers.Where((c) => c.Id == customerId).ToList();

                var tablaClientes = _utilidades.ToDataTable(lista2);

                //ELEMENTOS DE VENTA
                var lista3 = _context.SaleItems.Where((sale) => sale.SaleId == saleId).ToList();
                var tablaItems = _utilidades.ToDataTable(lista3);
 
                lis.Add(tablaVenta);
                lis.Add(tablaClientes);
                lis.Add(tablaItems);

            }

            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return lis;
        }

        // Método para convertir una lista genérica en un DataTable
       

    }
}
