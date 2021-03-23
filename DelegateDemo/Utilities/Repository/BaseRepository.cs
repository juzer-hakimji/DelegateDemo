using DelegateDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelegateDemo.Utilities.Repository
{
    public abstract class BaseRepository
    {
        protected DBContextResult<U> ExecuteEntityFrameworkMethod<T, U>(DelegateDemoContext db, Func<DelegateDemoContext, T, U> Func, T Obj)
        {
            try
            {
                return new DBContextResult<U>
                {
                    Data = Func(db, Obj),
                    TransactionResult = true
                };
            }
            catch (DbUpdateException ex)
            {
                //Log error
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (SqlException ex)
            {
                //Log error
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (Exception ex)
            {
                //Log error
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
        }

        protected DBContextResult<U> ExecuteAdoNetMethod<T, U>(T obj, Func<T, U> Func)
        {
            try
            {
                return new DBContextResult<U>
                {
                    Data = Func(obj),
                    TransactionResult = true
                };
            }
            catch (SqlException ex)
            {
                //Log error
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
            catch (Exception ex)
            {
                //log error
                return new DBContextResult<U>
                {
                    TransactionResult = false
                };
            }
        }
    }
}
