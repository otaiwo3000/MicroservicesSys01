using System;
using System.Collections.Generic;
using System.Text;
using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Interfaces.RepositoryInterfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {
        IEnumerable<Users> GetAllUsers(int organizationID);
        Users GetUserById(int organizationID, int Id);
        Users GetUserWithDetails(int organizationID, int Id);
        //IEnumerable<Users> GetAllUsers();
        //Users GetUserById(int Id);
        //Users GetUserWithDetails(int Id);
        void CreateUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
        Users GetUserByUserName(string username);

    }
}
