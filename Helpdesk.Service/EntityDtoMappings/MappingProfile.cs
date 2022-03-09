using AutoMapper;
using Helpdesk.Service.DtoModels;
using Helpdesk.Shared.Entities;


namespace Helpdesk.Service.EntityDtoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organizations, OrganizationModel>();
            CreateMap<OrganizationCreateModel, Organizations>();
            CreateMap<OrganizationUpdateModel, Organizations>();

            CreateMap<OrganizationContacts, OrganizationContactsModel>();
            CreateMap<OrganizationContactsCreateModel, OrganizationContacts>();
            CreateMap<OrganizationContactsUpdateModel, OrganizationContacts>();

            CreateMap<Roles, RolesModel>();
            CreateMap<RolesCreateModel, Roles>();
            CreateMap<RolesUpdateModel, Roles>();

            CreateMap<AgentEngagementTypes, AgentEngagementTypesModel>();
            CreateMap<AgentEngagementTypesCreateModel, AgentEngagementTypes>();
            CreateMap<AgentEngagementTypesUpdateModel, AgentEngagementTypes>();

            CreateMap<AgentTypes, AgentTypesModel>();
            CreateMap<AgentTypesCreateModel, AgentTypes>();
            CreateMap<AgentTypesUpdateModel, AgentTypes>();

            CreateMap<Users, UsersModel>();
            CreateMap<UsersCreateModel, Users>();
            CreateMap<UsersUpdateModel, Users>();

            CreateMap<UserGroup, UserGroupModel>();
            CreateMap<UserGroupCreateModel, UserGroup>();
            CreateMap<UserGroupUpdateModel, UserGroup>();

            CreateMap<GroupTypes, GroupTypesModel>();
            CreateMap<GroupTypesCreateModel, GroupTypes>();
            CreateMap<GroupTypesUpdateModel, GroupTypes>();

            CreateMap<Groups, GroupsModel>();
            CreateMap<GroupsCreateModel, Groups>();
            CreateMap<GroupsUpdateModel, Groups>();

            CreateMap<Privileges, PrivilegesModel>();
            CreateMap<PrivilegesCreateModel, Privileges>();
            CreateMap<PrivilegesUpdateModel, Privileges>();

            CreateMap<RolesPrivileges, RolesPrivilegesModel>();
            CreateMap<RolesPrivilegesCreateModel, RolesPrivileges>();
            CreateMap<RolesPrivilegesUpdateModel, RolesPrivileges>();

            CreateMap<CustomBusinessDayHours, CustomBusinessDayHoursModel>();
            CreateMap<CustomBusinessDayHoursCreateModel, CustomBusinessDayHours>();
            CreateMap<CustomBusinessDayHoursUpdateModel, CustomBusinessDayHours>();

            CreateMap<BusinessHours, BusinessHoursModel>();
            CreateMap<BusinessHoursCreateModel, BusinessHours>();
            CreateMap<BusinessHoursUpdateModel, BusinessHours>();

            CreateMap<Modules, ModulesModel>();
            CreateMap<ModulesCreateModel, Modules>();
            CreateMap<ModulesUpdateModel, Modules>();

            CreateMap<Products, ProductsModel>();
            CreateMap<ProductsCreateModel, Products>();
            CreateMap<ProductsUpdateModel, Products>();

            CreateMap<Requesters, RequestersModel>();
            CreateMap<RequestersCreateModel, Requesters>();
            CreateMap<RequestersUpdateModel, Requesters>();

            CreateMap<SLAPolicy, SLAPolicyModel>();
            CreateMap<SLAPolicyCreateModel, SLAPolicy>();
            CreateMap<SLAPolicyUpdateModel, SLAPolicy>();

            CreateMap<SLAPriority, SLAPriorityModel>();
            CreateMap<SLAPriorityCreateModel, SLAPriority>();
            CreateMap<SLAPriorityUpdateModel, SLAPriority>();

            CreateMap<SLAPolicyPriority, SLAPolicyPriorityModel>();
            CreateMap<SLAPolicyPriorityCreateModel, SLAPolicyPriority>();
            CreateMap<SLAPolicyPriorityUpdateModel, SLAPolicyPriority>();

            CreateMap<SupportChannels, SupportChannelsModel>();
            CreateMap<SupportChannelsCreateModel, SupportChannels>();
            CreateMap<SupportChannelsUpdateModel, SupportChannels>();

            CreateMap<TicketTypes, TicketTypesModel>();
            CreateMap<TicketTypesCreateModel, TicketTypes>();
            CreateMap<TicketTypesUpdateModel, TicketTypes>();

            CreateMap<Tickets, TicketsModel>();
            CreateMap<TicketsCreateModel, Tickets>();
            CreateMap<TicketsUpdateModel, Tickets>();
            CreateMap<ClientTicketsCreateModel, Tickets>();
            CreateMap<ClientTicketsUpdateModel, Tickets>();
            CreateMap<Tickets_SLA, TicketsModel>();
            CreateMap<TicketTicketStatusUpdateModel, Tickets>();

            CreateMap<TicketStatus, TicketStatusModel>();
            CreateMap<TicketStatusCreateModel, TicketStatus>();
            CreateMap<TicketStatusUpdateModel, TicketStatus>();

            CreateMap<RuleCases, RuleCasesModel>();
            CreateMap<RuleCasesCreateModel, RuleCases>();
            CreateMap<RuleCasesUpdateModel, RuleCases>();

            CreateMap<RuleConditions, RuleConditionsModel>();
            CreateMap<RuleConditionsCreateModel, RuleConditions>();
            CreateMap<RuleConditionsUpdateModel, RuleConditions>();

            CreateMap<RuleAction, RuleActionModel>();
            CreateMap<RuleActionCreateModel, RuleAction>();
            CreateMap<RuleActionUpdateModel, RuleAction>();

            CreateMap<TicketRules, TicketRulesModel>();
            //CreateMap<TicketRulesCreateModel, TicketRules>();
            CreateMap<TicketRulesCreateModel, TicketRules_3>();
            CreateMap<TicketRules_2, TicketRules>();
            CreateMap<TicketRulesUpdateModel, TicketRules>();
            CreateMap<RuleActions_B, RuleAction_2>();

            CreateMap<TicketRules, TicketRules_2>();
            CreateMap<RuleAction_2, RuleActions_B>();

            //CreateMap<ProductModules, ProductModulesModel>();
            //CreateMap<ProductModulesCreateModel, ProductModules>();
            //CreateMap<ProductModulesUpdateModel, ProductModules>();

            CreateMap<ProductModules, ProductModulesModel>();            
            CreateMap<ProductModulesCreateModel, ProductModues_List>();
            CreateMap<ModuleIDs, ProductModules>();
            CreateMap<ProductModulesUpdateModel, ProductModues_List>();
            CreateMap<ProModModel, ProductModules>();

            CreateMap<InitializedTickets, InitializedTicketsModel>();
            CreateMap<InitializedTicketsCreateModel, InitializedTickets>();
            CreateMap<InitializedTicketsUpdateModel, InitializedTickets>();

            CreateMap<ActionProperty, ActionPropertyModel>();
            CreateMap<ActionPropertyCreateModel, ActionProperty>();
            CreateMap<ActionPropertyUpdateModel, ActionProperty>();

            CreateMap<CustomMessages, CustomMessagesModel>();
            CreateMap<CustomMessagesCreateModel, CustomMessages>();
            CreateMap<CustomMessagesUpdateModel, CustomMessages>();

            CreateMap<ReceivedEmailFilter, ReceivedEmailFilterModel>();
            CreateMap<ReceivedEmailFilterCreateModel, ReceivedEmailFilter>();
            CreateMap<ReceivedEmailFilterUpdateModel, ReceivedEmailFilter>();

            CreateMap<OrganizationDocuments, OrganizationDocumentsModel>();
            CreateMap<OrganizationDocumentsCreateModel, OrganizationDocuments>();
            CreateMap<OrganizationDocumentsUpdateModel, OrganizationDocuments>();
        }
    }
}
