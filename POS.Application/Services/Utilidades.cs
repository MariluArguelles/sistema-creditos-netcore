using POS.Application.Interfaces;
using System.Data;
using System.Reflection;

namespace POS.Application.Services
{
    public class Utilidades : IUtilidades
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Obtén todas las propiedades públicas de T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Agrega las columnas al DataTable basadas en las propiedades de T
            foreach (var prop in properties)
            {
                // Si la propiedad es nullable, usa su tipo subyacente
                var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, propertyType);
            }

            // Agrega las filas al DataTable con los datos de la lista
            foreach (var item in items)
            {
                var values = properties.Select(prop =>
                {
                    var value = prop.GetValue(item);
                    return value == null ? DBNull.Value : value; // Maneja los valores nulos
                }).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        //public DataTable ToDataTable<T>(List<T> items)
        //{
        //    var dataTable = new DataTable(typeof(T).Name);

        //    // Obtén todas las propiedades públicas de T
        //    var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    // Agrega las columnas al DataTable basadas en las propiedades de T
        //    foreach (var prop in properties)
        //    {
        //        dataTable.Columns.Add(prop.Name, prop.PropertyType);
        //    }

        //    // Agrega las filas al DataTable con los datos de la lista
        //    foreach (var item in items)
        //    {
        //        var values = properties.Select(prop => prop.GetValue(item)).ToArray();
        //        dataTable.Rows.Add(values);
        //    }

        //    return dataTable;
        //}
    }
}
