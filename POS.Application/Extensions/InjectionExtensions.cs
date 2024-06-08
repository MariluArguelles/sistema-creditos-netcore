using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.ExportPDF;
using POS.Application.ExportPDF.Report;
using POS.Application.ExportPDF.Reportes;
using POS.Application.Extensions.WatchDog;
using POS.Application.Interfaces;
using POS.Application.Services;
using System.Reflection;

namespace POS.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            //services.AddFluentValidation(options =>
            //{
            //    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            //});

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registrar IHttpContextAccessor como un servicio
            services.AddHttpContextAccessor();

            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<ISaleApplication, SaleApplication>();
            services.AddScoped<ISaleItemApplication, SaleItemApplication>();
            services.AddScoped<IPaymentApplication, PaymentApplication>();
            services.AddScoped<IGenerateExcelApplication, GenerateExcelApplication>();
            services.AddScoped<IGeneratePDF, GeneratePDF>();
            services.AddScoped<IReport, Factura>();
            services.AddScoped<IUtilidades, Utilidades>();
            services.AddScoped<IAuthApplication,AuthApplication>();
            services.AddWatchDog(configuration);



            return services;
        }
    }
}
