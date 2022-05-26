using Helpdesk.Business.Interfaces.RepositoryInterfaces;


//namespace Helpdesk.Contracts.RepositoryInterfaces
namespace Helpdesk.Business.Interfaces
{
    public interface IRepositoryWrapper
    {
        //ITestBulkRepository testBulk { get; }
        IOrganizationsRepository organizations{ get; }
        IOrganizationContactsRepository organizationcontacts { get; }
        IPrivilegesRepository privileges { get; }
        IRolesRepository roles { get; }
        IRolesPrivilegesRepository rolesprivileges { get; }
        //IAgentEngagementTypesRepository agentengagementtypes { get; }
        //IAgentTypesRepository agenttypes { get; }      
        //IGroupTypesRepository grouptypes { get; }
        //IGroupsRepository groups { get; }               
        IUsersRepository users { get; }
        //IUserGroupRepository usergroup { get; }
        IUserRoleRepository userrole { get; }
        //ICustomBusinessDayHoursRepository custombusinessdayhours { get; }
        //IBusinessHoursRepository businesshours { get; }
        //IModulesRepository modules { get; }
        //IProductsRepository products { get; }
        //IRequestersRepository requesters { get; }
        //ISLAPolicyRepository slapolicy { get; }
        //ISLAPriorityRepository slapriority { get; }
        //ISLAPolicyPriorityRepository slapolicypriority { get; }
        //ISupportChannelsRepository supportchannels { get; }
        //ITicketTypesRepository tickettypes { get; }
        //ITicketStatusRepository ticketstatus { get; }
        ITicketsRepository tickets { get; }
        //IRuleActionRepository ruleaction { get; }
        //IRuleCasesRepository rulecases { get; }
        //IRuleConditionsRepository ruleconditions { get; }
       
        //ITicketRulesRepository ticketrules { get; }
        //IRuleTicketCreationRepository ruleticketcreation { get; }
        IPendingEmailRepository pendingemail { get; }
        //IProductModulesRepository productmodules { get; }
        //IInitializedTicketsRepository initializedtickets { get; }
        //IRuleAction_2Repository ruleaction_2 { get; }
        //IReceivedIssueMailsRepository receivedissuemails { get; }
        //IActionPropertyRepository actionproperties { get; }
        //ICustomMessagesRepository custommessages { get; }
        //IReceivedEmailFilterRepository receivedemailfilter { get; }
        //IOrganizationDocumentsRepository organizationdocuments { get; }

        void Save();
        //int Save();         
    }
}
