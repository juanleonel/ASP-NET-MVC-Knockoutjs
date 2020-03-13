using Dapper;
using Domain.Base;
using Domain.Entities.Principal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootApp.Domain.DataAccess
{
    public class AuthorDA : BaseDapper
    {
        
        public List<Author> GetAll()
        {
            const string sql = "[dbo].[spSelAuthors]";

            return Query<Author>(sql, null, commandType: CommandType.StoredProcedure).ToList();
        }

        public Author Inst(Author author)
        {
            Author result = null;
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BootsCN"].ConnectionString))
            {
                cn.Open();

                SqlTransaction tran = null;

                try
                {
                    tran = cn.BeginTransaction();

                    const string sql = "[dbo].[spInsAuthors]";

                    result = cn.Query<Author>(sql, new {
                                                        @FirstName = author.FirstName, 
                                                        @LastName = author.LastName, 
                                                        @Biography = author.Biography 
                                                    }, transaction: tran, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    tran.Commit();

                    cn.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    cn.Close();
                }
            }
            return result;

        }

        public Author GetByID(int Id)
        {
            const string sql = "[dbo].[spSelAuthors]";

            return Query<Author>(sql, new { @Id = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }




    }
}
