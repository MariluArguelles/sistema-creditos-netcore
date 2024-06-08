using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public  interface IUtilidades
    {
        public DataTable ToDataTable<T>(List<T> items);

        public bool GetFullUrl();
    }
}
