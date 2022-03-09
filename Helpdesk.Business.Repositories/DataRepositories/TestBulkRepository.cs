using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    //public class TestBulkRepository : HelpdeskRepositoryBase<TestBulk>, ITestBulkRepository
    public class TestBulkRepository : RepositoryBase<TestBulk>, ITestBulkRepository
    {
        public TestBulkRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<TestBulk> t)
           : base(helpDeskDBContext)
        {
            _t = t;
        }
        IRepositoryBase<TestBulk> _t;

        ////public IQueryable<TestBulk> GetEntities()
        //protected override IQueryable<TestBulk> GetEntities()
        //{
        //    return from e in HelpDeskDBContext.Set<TestBulk>()                   
        //           select e;
                 
        //    ////the below also works
        //    //var res = RepositoryContext.Set<TestBulk>();
        //    //return res;
        //}

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
