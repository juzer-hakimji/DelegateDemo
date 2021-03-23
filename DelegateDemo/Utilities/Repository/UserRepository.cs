using DelegateDemo.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DelegateDemo.Utilities.Repository
{
    public interface IUserRepository
    {
        DBContextResult<MstUser> AddUserUsingEF(MstUser p_MST_User);
        DBContextResult<DataTable> AddUserUsingAdoNet(MstUser p_MST_User);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        private DelegateDemoContext db;

        public UserRepository()
        {
            db = new DelegateDemoContext();
        }
        public DBContextResult<MstUser> AddUserUsingEF(MstUser p_MST_User)
        {
            return ExecuteEntityFrameworkMethod<MstUser, MstUser>(db, (DataContext, P_MST_UserInfo) =>
            {
                DataContext.MstUsers.Add(P_MST_UserInfo);
                DataContext.SaveChanges();
                return p_MST_User;
            }, p_MST_User);
        }

        public DBContextResult<DataTable> AddUserUsingAdoNet(MstUser p_MST_User)
        {
            using SqlConnection conn = new SqlConnection("Server=DESKTOP-2KLD6BE;Database=DelegateDemo;User ID=Admin;pwd=admin123");
            conn.Open();
            using SqlCommand cmd = new SqlCommand("usp_AddUser", conn);
            cmd.Parameters.Add(new SqlParameter("@FirstName", p_MST_User.FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", p_MST_User.LastName));
            cmd.Parameters.Add(new SqlParameter("@MobileNumber", p_MST_User.MobileNumber));
            using var da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            DBContextResult<DataTable> result = ExecuteAdoNetMethod<SqlDataAdapter, DataTable>(da, (DataAdapter) =>
            {
                DataTable DataTable = new DataTable();
                da.Fill(DataTable);
                return DataTable;
            });
            conn.Close();
            return result;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        ~UserRepository()
        {
            Dispose();
        }
    }
}
