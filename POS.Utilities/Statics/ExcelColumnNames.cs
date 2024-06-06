namespace POS.Utilities.Statics
{
    public class ExcelColumnNames
    {
        public static List<TableColumn> GetColumns(IEnumerable<(string ColumnName, string PropertyName)> columnsProperties)
        {
            var columns = new List<TableColumn>();

            foreach (var (ColumnName, PropertyName) in columnsProperties)
            {
                var column = new TableColumn()
                {
                    Label = ColumnName,
                    PropertyName = PropertyName
                };
                columns.Add(column);
            }
            return columns;
        }
        #region ColumnsCategories
        public static List<(string ColumnName, string PropertyName)> GetColumnsCategories()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
                {
                    ("NOMBRE","Name"),
                    ("DESCRIPCIÓN","Description"),
                    ("FECHA DE REGISTRO","AuditCreateDate"),
                    //("estado","StateCategory")  asi lo puso el profe
                    ("ESTADO","StateCategory")
                };
            return columnsProperties;
        }
        #endregion

        #region ColumnsProducts
        public static List<(string ColumnName, string PropertyName)> GetColumnsProducts()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
                {
                    ("DESCRIPCIÓN","Description"),
                    ("MARCA","Brand"),
                    ("FECHA DE REGISTRO","AuditCreateDate"),
                    //("estado","StateCategory")  asi lo puso el profe
                    ("ESTADO","StateProduct")
                };
            return columnsProperties;
        }
        #endregion


        #region ColumnsCustomers
        public static List<(string ColumnName, string PropertyName)> GetColumnsCustomers()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
                {
                    ("NOMBRE","Name"),
                    ("AP P","LastName1"),
                    ("AP M","LastName2"),
                    ("F NAC","BirthDate"),
                    ("SEXO","GenderText"),
                    ("CORREO E.","Email"),
                    ("REGISTRO","AuditCreateDate")
                };
            return columnsProperties;
        }
        #endregion

        #region ColumnsCustomers
        public static List<(string ColumnName, string PropertyName)> GetColumnsUsers()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
                {
                    ("USUARIO","UserName"),
                    ("CORREO E.","Email"),
                    ("TIPO","AuthType"),
                    ("ESTADO","StateUser"),
                    ("FECHA DE REGISTRO","AuditCreateDate")
                };
            return columnsProperties;
        }
        #endregion

    }

}


