using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain.Entities.Principal;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.DataAccess
{
    public class BookDA : BaseDapper
    {


       
        public Book Ins(Book book)
        {

            const string sql = "";

            return Query<Book>(sql, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
       
    }
}
