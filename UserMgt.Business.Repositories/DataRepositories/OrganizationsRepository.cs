
using System.Collections.Generic;
using System.Linq;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories.DataRepositories
{
    //public class TestBulkRepository : HelpdeskRepositoryBase<TestBulk>, ITestBulkRepository
    public class OrganizationsRepository : RepositoryBase<Organizations>, IOrganizationsRepository
    {
        public OrganizationsRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<Organizations> repo)
           : base(userMgtDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Organizations> _repo;

        //public IQueryable<TestBulk> GetEntities()
        //protected override IQueryable<Organizations> GetEntities()
        public  IEnumerable<Organizations> GetAllOrganizations()
        {
            //return from e in HelpDeskDBContext.Set<Organizations>()                   
            //       select e;

            ////the below also works
            //var res = RepositoryContext.Set<TestBulk>();
            //return res;

            return FindAll().OrderBy(x => x.Name).ToList();
        }

        public Organizations GetOrganizationById(int orgId)
        {
            return FindByCondition(x => x.OrganizationId.Equals(orgId)).FirstOrDefault();
        }

        public Organizations GetOrganizationWithDetails(int orgId)
        {
            return FindByCondition(x => x.OrganizationId.Equals(orgId))
                //.Include(y => y.Accounts)
                .FirstOrDefault();
        }

        public void CreateOrganization(Organizations organization)
        {
            Create(organization);
        }

        public void UpdateOrganization(Organizations organization)
        {
            Update(organization);
        }

        public void DeleteOrganization(Organizations organization)
        {
            Delete(organization);
        }

        //protected override TestBulk AddEntity(TestBulk entity)
        //{
        //    return RepositoryContext.Set<TestBulk>().Add(entity);
        //}
        //protected override TestBulk UpdateEntity(TestBulk entity)
        //{
        //    return RepositoryContext.Set<TestBulk>().Add(entity);
        //}


        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
