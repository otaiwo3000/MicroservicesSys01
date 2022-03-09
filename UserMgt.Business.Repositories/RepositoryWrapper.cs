using UserMgt.Business.Repositories.DataRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Interface declaration

        private UserMgtDBContext _userMgtDBContext;

      
        private IOrganizationsRepository _organizations;
        private IRepositoryBase<Organizations> _repoBaseOrgs;

        private IOrganizationContactsRepository _organizationcontacts;
        private IRepositoryBase<OrganizationContacts> _repoBaseOrgContacts;

        private IPrivilegesRepository _privileges;
        private IRepositoryBase<Privileges> _repoBasePrivileges;

        private IRolesRepository _roles;
        private IRepositoryBase<Roles> _repoBaseRoles;

        private IRolesPrivilegesRepository _rolesprivileges;
        private IRepositoryBase<RolesPrivileges> _repoBaseRolesPrivileges;

        private IUsersRepository _users;
        private IRepositoryBase<Users> _repoBaseUsers;
       
        private IUserRoleRepository _userrole;
        private IRepositoryBase<UserRole> _repoBaseUserRole;

        private IPendingEmailRepository _pendingemail;
        private IRepositoryBase<PendingEmail> _repoBasePendingEmail;

        #endregion


        #region Interfaces

        public IOrganizationsRepository organizations
        {
            get
            {
                if (_organizations == null)
                {
                    _organizations = new OrganizationsRepository(_userMgtDBContext, _repoBaseOrgs);
                }
                return _organizations;
            }
        }

        public IOrganizationContactsRepository organizationcontacts
        {
            get
            {
                if (_organizationcontacts == null)
                {
                    _organizationcontacts = new OrganizationContactsRepository(_userMgtDBContext, _repoBaseOrgContacts);
                }
                return _organizationcontacts;
            }
        }

        public IPrivilegesRepository privileges
        {
            get
            {
                if (_privileges == null)
                {
                    _privileges = new PrivilegesRepository(_userMgtDBContext, _repoBasePrivileges);
                }
                return _privileges;
            }
        }

        public IRolesRepository roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolesRepository(_userMgtDBContext, _repoBaseRoles);
                }
                return _roles;
            }
        }

        public IRolesPrivilegesRepository rolesprivileges
        {
            get
            {
                if (_rolesprivileges == null)
                {
                    _rolesprivileges = new RolesPrivilegesRepository(_userMgtDBContext, _repoBaseRolesPrivileges);
                }
                return _rolesprivileges;
            }
        }

        public IUsersRepository users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UsersRepository(_userMgtDBContext, _repoBaseUsers);
                }
                return _users;
            }
        }

        public IUserRoleRepository userrole
        {
            get
            {
                if (_userrole == null)
                {
                    _userrole = new UserRoleRepository(_userMgtDBContext, _repoBaseUserRole);
                }
                return _userrole;
            }
        }

        public IPendingEmailRepository pendingemail
        {
            get
            {
                if (_pendingemail == null)
                {
                    _pendingemail = new PendingEmailRepository(_userMgtDBContext, _repoBasePendingEmail);
                }
                return _pendingemail;
            }
        }

        #endregion



        public RepositoryWrapper(UserMgtDBContext userMgtDBContext)
        {
            _userMgtDBContext = userMgtDBContext;
        }

        public void Save()
        //public int Save()
        {
            OnBeforeSaveChanges();
            _userMgtDBContext.SaveChanges();
        }


        //private void OnBeforeSaveChanges(int userId)
        //private void OnBeforeSaveChanges(string username)
        private void OnBeforeSaveChanges()
        {
            //var httpContext = new HttpContextAccessor().HttpContext;
            //string username = Convert.ToString(httpContext.User.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault());
            //int userId = int.Parse(httpContext.User.Claims.Where(x => x.Type == "userid").Select(x => x.Value).FirstOrDefault());

            _userMgtDBContext.ChangeTracker.DetectChanges();
            foreach (var entry in _userMgtDBContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                }
            }
        }

    }
}
