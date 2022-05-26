using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;
using Helpdesk.Business.Repositories.DataRepositories;

using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace Helpdesk.Business.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Interface declaration

        private HelpDeskDBContext _helpDeskDBContext;




        //private ITestBulkRepository _testBulk;
        //private IRepositoryBase<TestBulk> _repoBaseTestBulk;

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

        //private IAgentEngagementTypesRepository _agentengagementtypes;
        //private IRepositoryBase<AgentEngagementTypes> _repoBaseAgentEngagementTypes;

        //private IAgentTypesRepository _agenttypes;
        //private IRepositoryBase<AgentTypes> _repoBaseAgentTypes;

        //private IGroupTypesRepository _grouptypes;
        //private IRepositoryBase<GroupTypes> _repoBaseGroupTypes;

        //private IGroupsRepository _groups;
        //private IRepositoryBase<Groups> _repoBaseGroups;

        private IUsersRepository _users;
        private IRepositoryBase<Users> _repoBaseUsers;

        //private IUserGroupRepository _usergroup;
        //private IRepositoryBase<UserGroup> _repoBaseUserGroup;

        private IUserRoleRepository _userrole;
        private IRepositoryBase<UserRole> _repoBaseUserRole;

        //private ICustomBusinessDayHoursRepository _custombusinessdayhours;
        //private IRepositoryBase<CustomBusinessDayHours> _repoBaseCustomBusinessDayHours;

        //private IBusinessHoursRepository _businesshours;
        //private IRepositoryBase<BusinessHours> _repoBaseBusinessHours;

        //private IModulesRepository _modules;
        //private IRepositoryBase<Modules> _repoBaseModules;

        //private IProductsRepository _products;
        //private IRepositoryBase<Products> _repoBaseProducts;

        //private IRequestersRepository _requesters;
        //private IRepositoryBase<Requesters> _repoBaseRequesters;

        //private ISLAPolicyRepository _slapolicy;
        //private IRepositoryBase<SLAPolicy> _repoBaseSLAPolicy;

        //private ISLAPriorityRepository _slapriority;
        //private IRepositoryBase<SLAPriority> _repoBaseSLAPriority;

        //private ISLAPolicyPriorityRepository _slapolicypriority;
        //private IRepositoryBase<SLAPolicyPriority> _repoBaseSLAPolicyPriority;

        //private ISupportChannelsRepository _supportchannels;
        //private IRepositoryBase<SupportChannels> _repoBaseSupportChannels;

        //private ITicketTypesRepository _tickettypes;
        //private IRepositoryBase<TicketTypes> _repoBaseTicketTypes;

        //private ITicketStatusRepository _ticketstatus;
        //private IRepositoryBase<TicketStatus> _repoBaseTicketStatus;

        private ITicketsRepository _tickets;
        private IRepositoryBase<Tickets> _repoBaseTickets;

        //private IRuleCasesRepository _rulecases;
        //private IRepositoryBase<RuleCases> _repoBaseRuleCases;

        //private IRuleConditionsRepository _ruleconditions;
        //private IRepositoryBase<RuleConditions> _repoBaseRuleConditions;

        //private IRuleActionRepository _ruleaction;
        //private IRepositoryBase<RuleAction> _repoBaseRuleAction;

        //private ITicketRulesRepository _ticketrules;
        //private IRepositoryBase<TicketRules> _repoBaseTicketRules;

        //private IRuleTicketCreationRepository _ruleticketcreation;
        //private IRepositoryBase<RuleTicketCreation> _repoBaseRuleTicketCreation;

        private IPendingEmailRepository _pendingemail;
        private IRepositoryBase<PendingEmail> _repoBasePendingEmail;

        //private IProductModulesRepository _productmodules;
        //private IRepositoryBase<ProductModules> _repoBaseProductModules;

        //private IInitializedTicketsRepository _initializedtickets;
        //private IRepositoryBase<InitializedTickets> _repoBaseInitializedTickets;

        //private IRuleAction_2Repository _ruleaction_2;
        //private IRepositoryBase<RuleAction_2> _repoBaseRuleAction_2;

        //private IReceivedIssueMailsRepository _receivedissuemails;
        //private IRepositoryBase<ReceivedIssueMails> _repoBaseReceivedIssueMails;

        //private IActionPropertyRepository _actionproperties;
        //private IRepositoryBase<ActionProperty> _repoBaseActionProperties;

        //private ICustomMessagesRepository _custommessages;
        //private IRepositoryBase<CustomMessages> _repoBaseCustomMessages;

        //private IReceivedEmailFilterRepository _receivedemailfilter;
        //private IRepositoryBase<ReceivedEmailFilter> _repoBaseReceivedEmailFilter;

        //private IOrganizationDocumentsRepository _organizationdocuments;
        //private IRepositoryBase<OrganizationDocuments> _repoBaseOrganizationDocuments;

        #endregion


        #region Interfaces

        //public ITestBulkRepository testBulk
        //{
        //    get
        //    {
        //        if (_testBulk == null)
        //        {
        //            _testBulk = new TestBulkRepository(_helpDeskDBContext, _repoBaseTestBulk);
        //        }
        //        return _testBulk;
        //    }
        //}

        public IOrganizationsRepository organizations
        {
            get
            {
                if (_organizations == null)
                {
                    _organizations = new OrganizationsRepository(_helpDeskDBContext, _repoBaseOrgs);
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
                    _organizationcontacts = new OrganizationContactsRepository(_helpDeskDBContext, _repoBaseOrgContacts);
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
                    _privileges = new PrivilegesRepository(_helpDeskDBContext, _repoBasePrivileges);
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
                    _roles = new RolesRepository(_helpDeskDBContext, _repoBaseRoles);
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
                    _rolesprivileges = new RolesPrivilegesRepository(_helpDeskDBContext, _repoBaseRolesPrivileges);
                }
                return _rolesprivileges;
            }
        }

        //public IAgentEngagementTypesRepository agentengagementtypes
        //{
        //    get
        //    {
        //        if (_agentengagementtypes == null)
        //        {
        //            _agentengagementtypes = new AgentEngagementTypesRepository(_helpDeskDBContext, _repoBaseAgentEngagementTypes);
        //        }
        //        return _agentengagementtypes;
        //    }
        //}

        //public IAgentTypesRepository agenttypes
        //{
        //    get
        //    {
        //        if (_agenttypes == null)
        //        {
        //            _agenttypes = new AgentTypesRepository(_helpDeskDBContext, _repoBaseAgentTypes);
        //        }
        //        return _agenttypes;
        //    }
        //}

        //public IGroupTypesRepository grouptypes
        //{
        //    get
        //    {
        //        if (_grouptypes == null)
        //        {
        //            _grouptypes = new GroupTypesRepository(_helpDeskDBContext, _repoBaseGroupTypes);
        //        }
        //        return _grouptypes;
        //    }
        //}

        //public IGroupsRepository groups
        //{
        //    get
        //    {
        //        if (_groups == null)
        //        {
        //            _groups = new GroupsRepository(_helpDeskDBContext, _repoBaseGroups);
        //        }
        //        return _groups;
        //    }
        //}

        public IUsersRepository users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UsersRepository(_helpDeskDBContext, _repoBaseUsers);
                }
                return _users;
            }
        }

        //public IUserGroupRepository usergroup
        //{
        //    get
        //    {
        //        if (_usergroup == null)
        //        {
        //            _usergroup = new UserGroupRepository(_helpDeskDBContext, _repoBaseUserGroup);
        //        }
        //        return _usergroup;
        //    }
        //}

        public IUserRoleRepository userrole
        {
            get
            {
                if (_userrole == null)
                {
                    _userrole = new UserRoleRepository(_helpDeskDBContext, _repoBaseUserRole);
                }
                return _userrole;
            }
        }

        //public ICustomBusinessDayHoursRepository custombusinessdayhours
        //{
        //    get
        //    {
        //        if (_custombusinessdayhours == null)
        //        {
        //            _custombusinessdayhours = new CustomBusinessDayHoursRepository(_helpDeskDBContext, _repoBaseCustomBusinessDayHours);
        //        }
        //        return _custombusinessdayhours;
        //    }
        //}

        //public IBusinessHoursRepository businesshours
        //{
        //    get
        //    {
        //        if (_businesshours == null)
        //        {
        //            _businesshours = new BusinessHoursRepository(_helpDeskDBContext, _repoBaseBusinessHours);
        //        }
        //        return _businesshours;
        //    }
        //}

        //public IModulesRepository modules
        //{
        //    get
        //    {
        //        if (_modules == null)
        //        {
        //            _modules = new ModulesRepository(_helpDeskDBContext, _repoBaseModules);
        //        }
        //        return _modules;
        //    }
        //}

        //public IProductsRepository products
        //{
        //    get
        //    {
        //        if (_products == null)
        //        {
        //            _products = new ProductsRepository(_helpDeskDBContext, _repoBaseProducts);
        //        }
        //        return _products;
        //    }
        //}

        //public IRequestersRepository requesters
        //{
        //    get
        //    {
        //        if (_requesters == null)
        //        {
        //            _requesters = new RequestersRepository(_helpDeskDBContext, _repoBaseRequesters);
        //        }
        //        return _requesters;
        //    }
        //}

        //public ISLAPolicyRepository slapolicy
        //{
        //    get
        //    {
        //        if (_slapolicy == null)
        //        {
        //            _slapolicy = new SLAPolicyRepository(_helpDeskDBContext, _repoBaseSLAPolicy);
        //        }
        //        return _slapolicy;
        //    }
        //}

        //public ISLAPriorityRepository slapriority
        //{
        //    get
        //    {
        //        if (_slapriority == null)
        //        {
        //            _slapriority = new SLAPriorityRepository(_helpDeskDBContext, _repoBaseSLAPriority);
        //        }
        //        return _slapriority;
        //    }
        //}

        //public ISLAPolicyPriorityRepository slapolicypriority
        //{
        //    get
        //    {
        //        if (_slapolicypriority == null)
        //        {
        //            _slapolicypriority = new SLAPolicyPriorityRepository(_helpDeskDBContext, _repoBaseSLAPolicyPriority);
        //        }
        //        return _slapolicypriority;
        //    }
        //}

        //public ISupportChannelsRepository supportchannels
        //{
        //    get
        //    {
        //        if (_supportchannels == null)
        //        {
        //            _supportchannels = new SupportChannelsRepository(_helpDeskDBContext, _repoBaseSupportChannels);
        //        }
        //        return _supportchannels;
        //    }
        //}

        //public ITicketTypesRepository tickettypes
        //{
        //    get
        //    {
        //        if (_tickettypes == null)
        //        {
        //            _tickettypes = new TicketTypesRepository(_helpDeskDBContext, _repoBaseTicketTypes);
        //        }
        //        return _tickettypes;
        //    }
        //}

        //public ITicketStatusRepository ticketstatus
        //{
        //    get
        //    {
        //        if (_ticketstatus == null)
        //        {
        //            _ticketstatus = new TicketStatusRepository(_helpDeskDBContext, _repoBaseTicketStatus);
        //        }
        //        return _ticketstatus;
        //    }
        //}

        public ITicketsRepository tickets
        {
            get
            {
                if (_tickets == null)
                {
                    _tickets = new TicketsRepository(_helpDeskDBContext, _repoBaseTickets);
                }
                return _tickets;
            }
        }

        //public IRuleCasesRepository rulecases
        //{
        //    get
        //    {
        //        if (_rulecases == null)
        //        {
        //            _rulecases = new RuleCasesRepository(_helpDeskDBContext, _repoBaseRuleCases);
        //        }
        //        return _rulecases;
        //    }
        //}

        //public IRuleConditionsRepository ruleconditions
        //{
        //    get
        //    {
        //        if (_ruleconditions == null)
        //        {
        //            _ruleconditions = new RuleConditionsRepository(_helpDeskDBContext, _repoBaseRuleConditions);
        //        }
        //        return _ruleconditions;
        //    }
        //}

        //public IRuleActionRepository ruleaction
        //{
        //    get
        //    {
        //        if (_ruleaction == null)
        //        {
        //            _ruleaction = new RuleActionRepository(_helpDeskDBContext, _repoBaseRuleAction);
        //        }
        //        return _ruleaction;
        //    }
        //}

        //public ITicketRulesRepository ticketrules
        //{
        //    get
        //    {
        //        if (_ticketrules == null)
        //        {
        //            _ticketrules = new TicketRulesRepository(_helpDeskDBContext, _repoBaseTicketRules);
        //        }
        //        return _ticketrules;
        //    }
        //}

        //public IRuleTicketCreationRepository ruleticketcreation
        //{
        //    get
        //    {
        //        if (_ruleticketcreation == null)
        //        {
        //            _ruleticketcreation = new RuleTicketCreationRepository(_helpDeskDBContext, _repoBaseRuleTicketCreation);
        //        }
        //        return _ruleticketcreation;
        //    }
        //}

        public IPendingEmailRepository pendingemail
        {
            get
            {
                if (_pendingemail == null)
                {
                    _pendingemail = new PendingEmailRepository(_helpDeskDBContext, _repoBasePendingEmail);
                }
                return _pendingemail;
            }
        }

        //public IProductModulesRepository productmodules
        //{
        //    get
        //    {
        //        if (_productmodules == null)
        //        {
        //            _productmodules = new ProductModulesRepository(_helpDeskDBContext, _repoBaseProductModules);
        //        }
        //        return _productmodules;
        //    }
        //}

        //public IInitializedTicketsRepository initializedtickets
        //{
        //    get
        //    {
        //        if (_initializedtickets == null)
        //        {
        //            _initializedtickets = new InitializedTicketsRepository(_helpDeskDBContext, _repoBaseInitializedTickets);
        //        }
        //        return _initializedtickets;
        //    }
        //}

        //public IRuleAction_2Repository ruleaction_2
        //{
        //    get
        //    {
        //        if (_ruleaction_2 == null)
        //        {
        //            _ruleaction_2 = new RuleAction_2Repository(_helpDeskDBContext, _repoBaseRuleAction_2);
        //        }
        //        return _ruleaction_2;
        //    }
        //}

        //public IReceivedIssueMailsRepository receivedissuemails
        //{
        //    get
        //    {
        //        if (_receivedissuemails == null)
        //        {
        //            _receivedissuemails = new ReceivedIssueMailsRepository(_helpDeskDBContext, _repoBaseReceivedIssueMails);
        //        }
        //        return _receivedissuemails;
        //    }
        //}

        //public IActionPropertyRepository actionproperties
        //{
        //    get
        //    {
        //        if (_actionproperties == null)
        //        {
        //            _actionproperties = new ActionPropertyRepository(_helpDeskDBContext, _repoBaseActionProperties);
        //        }
        //        return _actionproperties;
        //    }
        //}

        //public ICustomMessagesRepository custommessages
        //{
        //    get
        //    {
        //        if (_custommessages == null)
        //        {
        //            _custommessages = new CustomMessagesRepository(_helpDeskDBContext, _repoBaseCustomMessages);
        //        }
        //        return _custommessages;
        //    }
        //}

        //public IReceivedEmailFilterRepository receivedemailfilter
        //{
        //    get
        //    {
        //        if (_receivedemailfilter == null)
        //        {
        //            _receivedemailfilter = new ReceivedEmailFilterRepository(_helpDeskDBContext, _repoBaseReceivedEmailFilter);
        //        }
        //        return _receivedemailfilter;
        //    }
        //}

        //public IOrganizationDocumentsRepository organizationdocuments
        //{
        //    get
        //    {
        //        if (_organizationdocuments == null)
        //        {
        //            _organizationdocuments = new OrganizationDocumentsRepository(_helpDeskDBContext, _repoBaseOrganizationDocuments);
        //        }
        //        return _organizationdocuments;
        //    }
        //}

        #endregion



        public RepositoryWrapper(HelpDeskDBContext helpDeskDBContext)
        {
            _helpDeskDBContext = helpDeskDBContext;
        }

        public void Save()
        //public int Save()
        {
            OnBeforeSaveChanges();
            _helpDeskDBContext.SaveChanges();
        }


        //private void OnBeforeSaveChanges(int userId)
        //private void OnBeforeSaveChanges(string username)
        private void OnBeforeSaveChanges()
        {
            //var httpContext = new HttpContextAccessor().HttpContext;
            //string username = Convert.ToString(httpContext.User.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault());
            //int userId = int.Parse(httpContext.User.Claims.Where(x => x.Type == "userid").Select(x => x.Value).FirstOrDefault());

            _helpDeskDBContext.ChangeTracker.DetectChanges();
            foreach (var entry in _helpDeskDBContext.ChangeTracker.Entries())
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
