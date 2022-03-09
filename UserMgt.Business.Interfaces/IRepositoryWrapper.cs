using System;
using System.Collections.Generic;
using System.Text;
using UserMgt.Business.Interfaces.RepositoryInterfaces;

namespace UserMgt.Business.Interfaces
{
    public interface IRepositoryWrapper
    {
        IPrivilegesRepository privileges { get; }
        IRolesRepository roles { get; }
        IRolesPrivilegesRepository rolesprivileges { get; }
        IUsersRepository users { get; }
        IUserRoleRepository userrole { get; }
        IPendingEmailRepository pendingemail { get; }
      //o  IOrganizatinsRepository organizations { get; }
      //  IOrganizationContactsRepository organizationcontacts { get; }
      


        void Save();
    }
}
