using DelegateDemo.Models;
using DelegateDemo.Utilities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelegateDemo.Utilities.Service
{
    public interface IUserService
    {
        bool AddUserUsingEF(UserViewModel UserInfo);
        bool AddUserUsingAdoNet(UserViewModel UserInfo);
    }

    public class UserService : IUserService
    {
        private IUserRepository _UserRepository { get; set; }
        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public bool AddUserUsingEF(UserViewModel UserInfo)
        {
            var DBResult = _UserRepository.AddUserUsingEF(new MstUser
            {
                FirstName = UserInfo.FirstName,
                LastName = UserInfo.LastName,
                MobileNumber = UserInfo.MobileNumber
            });
            if (DBResult.TransactionResult)
            {
                // You can return model and pass it to view
                //return DBResult.Data;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddUserUsingAdoNet(UserViewModel UserInfo)
        {
            var DBResult = _UserRepository.AddUserUsingAdoNet(new MstUser
            {
                FirstName = UserInfo.FirstName,
                LastName = UserInfo.LastName,
                MobileNumber = UserInfo.MobileNumber
            });
            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows[0]["ReturnCode"].ToString().Equals("200"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
