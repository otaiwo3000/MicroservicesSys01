using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IUsersRepository : IRepositoryBase<Users>
    {
        IEnumerable<Users> GetAllUsers(int organizationID);
        //IEnumerable<Users> GetUsersByOrganizationId(int organizationId);
        Users GetUserById(int organizationID, int Id);
        Users GetUserWithDetails(int organizationID, int Id);
        void CreateUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
        Users GetUserByUserName(string username);

    }
}
