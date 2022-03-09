using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Helpdesk.Shared.DataAccess.DBContext
{
    public partial class HelpDeskDBContext : DbContext
    {
        //public HelpDeskDBContext(DbContextOptions options)
        //    : base(options)
        //{
        //}

        //public HelpDeskDBContext()
        //    : base()
        //{

        //}

        public HelpDeskDBContext(DbContextOptions<HelpDeskDBContext> options)
        : base(options)
        {

        }

        //public HelpDeskDBContext(string connectionString, int n) : base(GetOptions(connectionString))
        //{
        //}

        //private static DbContextOptions GetOptions(string connectionString)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        //}


        public virtual DbSet<TestBulk> TestBulkSet { get; set; }
        public virtual DbSet<AgentEngagementTypes> AgentEngagementTypesSet { get; set; }
        public virtual DbSet<AgentTypes> AgentTypesSet { get; set; }
        public virtual DbSet<BusinessHours> BusinessHoursSet { get; set; }
        public virtual DbSet<CustomBusinessDayHours> CustomBusinessDayHoursSet { get; set; }
        public virtual DbSet<Days> DaysSet { get; set; }
        public virtual DbSet<Groups> GroupsSet { get; set; }
        public virtual DbSet<Modules> ModulesSet { get; set; }
        public virtual DbSet<OrganizationContacts> OrganizationContactsSet { get; set; }
        public virtual DbSet<Organizations> OrganizationsSet { get; set; }
        public virtual DbSet<Privileges> PrivilegesSet { get; set; }
        public virtual DbSet<Products> ProductsSet { get; set; }
        public virtual DbSet<ProductModules> ProductsModulesSet { get; set; }
        public virtual DbSet<Requesters> RequestersSet { get; set; }
        public virtual DbSet<Roles> RolesSet { get; set; }
        public virtual DbSet<RolesPrivileges> RolesPrivilegesSet { get; set; }
        public virtual DbSet<SLAPolicy> SlapolicySet { get; set; }
        public virtual DbSet<SLAPriority> SlaprioritySet { get; set; }
        public virtual DbSet<SLAPolicyPriority> SLAPolicyPrioritySet { get; set; }
        public virtual DbSet<SupportChannels> SupportChannelsSet { get; set; }
        public virtual DbSet<TicketStatus> TicketStatusSet { get; set; }
        public virtual DbSet<TicketTypes> TicketTypesSet { get; set; }
        public virtual DbSet<Tickets> TicketsSet { get; set; }
        public virtual DbSet<UserGroup> UserGroupSet { get; set; }
        public virtual DbSet<UserRole> UserRoleSet { get; set; }
        public virtual DbSet<Users> UsersSet { get; set; }

        public virtual DbSet<RuleCases> RuleCasesSet { get; set; }
        public virtual DbSet<RuleConditions> RuleConditionsSet { get; set; }
        public virtual DbSet<TicketRules> TicketRulesSet { get; set; }
        public virtual DbSet<RuleAction_2> RuleAction_2Set { get; set; }
        public virtual DbSet<RuleTicketCreation> RuleTicketCreationSet { get; set; }
        public virtual DbSet<PendingEmail> PendingEmailSet { get; set; }
        public virtual DbSet<RuleAction> RuleActionSet { get; set; }
        public virtual DbSet<ReceivedIssueMails> ReceivedIssueMailsSet { get; set; }
        public virtual DbSet<ActionProperty> ActionPropertySet { get; set; }
        public virtual DbSet<CustomMessages> CustomMessagesSet { get; set; }
        public virtual DbSet<ReceivedEmailFilter> ReceivedEmailFilterSet { get; set; }
        public virtual DbSet<OrganizationDocuments> OrganizationDocumentSet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();


            //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>();
            //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>();
            //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>();
            //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>();
            //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityUser<string>>();
            ////modelBuilder.Ignore<ApplicationUser>();

            ////TestBulk
            modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasKey(e => e.TestBulkId); //Primary key
            ////modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasKey(e => new { e.TestBulkId, e.Name }); //Composite key
            //modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasIndex(e => e.Name);
            //modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasIndex(e => new { e.Name, e.Sex}); //Composite Index
            //modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasIndex(e => e.Name).HasName("indexName"); //using a named index
            //modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).HasIndex(e => e.Name).HasName("indexName").IsUnique(); //unique constraint on the column
            //modelBuilder.Entity<TestBulk>().Ignore(e => e.EntityId).Property(e=>e.Sex).HasDefaultValue("MF"); //default value
            // modelBuilder.Entity<Team>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TestBulk>().ToTable("testbulk");

            modelBuilder.Entity<AgentEngagementTypes>().Ignore(e => e.EntityId).HasKey(e => e.AgentEngagementTypeId); //Primary key
            modelBuilder.Entity<AgentEngagementTypes>().ToTable("AgentEngagementTypes");

            modelBuilder.Entity<AgentTypes>().Ignore(e => e.EntityId).HasKey(e => e.AgentTypeId); //Primary key
            modelBuilder.Entity<AgentTypes>().ToTable("AgentTypes");

            modelBuilder.Entity<BusinessHours>().Ignore(e => e.EntityId).HasKey(e => e.BusinessHourId); //Primary key
            modelBuilder.Entity<BusinessHours>().ToTable("BusinessHours");

            modelBuilder.Entity<CustomBusinessDayHours>().Ignore(e => e.EntityId).HasKey(e => e.CustomBusinessDayHourId); //Primary key
            modelBuilder.Entity<CustomBusinessDayHours>().ToTable("CustomBusinessDayHours");

            modelBuilder.Entity<Days>().Ignore(e => e.EntityId).HasKey(e => e.DayId); //Primary key
            modelBuilder.Entity<Days>().ToTable("Days");

            modelBuilder.Entity<Groups>().Ignore(e => e.EntityId).HasKey(e => e.GroupId); //Primary key
            modelBuilder.Entity<Groups>().ToTable("Groups");

            modelBuilder.Entity<GroupTypes>().Ignore(e => e.EntityId).HasKey(e => e.GroupTypeId); //Primary key
            modelBuilder.Entity<GroupTypes>().ToTable("GroupTypes");

            modelBuilder.Entity<Modules>().Ignore(e => e.EntityId).HasKey(e => e.ModuleId); //Primary key
            modelBuilder.Entity<Modules>().ToTable("Modules");

            modelBuilder.Entity<OrganizationContacts>().Ignore(e => e.EntityId).HasKey(e => e.OrganizationContactId); //Primary key
            modelBuilder.Entity<OrganizationContacts>().ToTable("OrganizationContacts");

            modelBuilder.Entity<Organizations>().Ignore(e => e.EntityId).HasKey(e => e.OrganizationId); //Primary key
            modelBuilder.Entity<Organizations>().ToTable("Organizations");

            modelBuilder.Entity<Privileges>().Ignore(e => e.EntityId).HasKey(e => e.PrivilegeId); //Primary key
            modelBuilder.Entity<Privileges>().ToTable("Privileges");

            modelBuilder.Entity<Products>().Ignore(e => e.EntityId).HasKey(e => e.ProductId); //Primary key
            modelBuilder.Entity<Products>().ToTable("Products");

            modelBuilder.Entity<ProductModules>().Ignore(e => e.EntityId).HasKey(e => e.ProductModuleId); //Primary key
            modelBuilder.Entity<ProductModules>().ToTable("ProductModules");

            modelBuilder.Entity<Requesters>().Ignore(e => e.EntityId).HasKey(e => e.RequesterId); //Primary key
            modelBuilder.Entity<Requesters>().ToTable("Requesters");

            modelBuilder.Entity<Roles>().Ignore(e => e.EntityId).HasKey(e => e.RoleId); //Primary key
            modelBuilder.Entity<Roles>().ToTable("Roles");
            //modelBuilder.Entity<Roles>().Property(c => c.RoleId).HasColumnType("bigint");
            //modelBuilder.Entity<Roles>().Property(p => p.RoleId).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Roles>(entity =>
            //{
            //    var pb = entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            //    if (Database.IsInMemory())
            //        pb.HasValueGenerator<InMemoryIntegerValueGenerator<long>>();
            //});

            modelBuilder.Entity<RolesPrivileges>().Ignore(e => e.EntityId).HasKey(e => e.RolePrivilegeId); //Primary key
            modelBuilder.Entity<RolesPrivileges>().ToTable("RolesPrivileges");
            modelBuilder.Entity<RolesPrivileges>().Property(c => c.RolePrivilegeId).HasColumnType("bigint");

            modelBuilder.Entity<SLAPolicy>().Ignore(e => e.EntityId).HasKey(e => e.SLAPolicyId); //Primary key
            modelBuilder.Entity<SLAPolicy>().ToTable("Slapolicy");

            modelBuilder.Entity<SLAPriority>().Ignore(e => e.EntityId).HasKey(e => e.SLAPriorityId); //Primary key
            modelBuilder.Entity<SLAPriority>().ToTable("Slapriority");

            modelBuilder.Entity<SLAPolicyPriority>().Ignore(e => e.EntityId).HasKey(e => e.SLAPolicyPriorityId); //Primary key
            modelBuilder.Entity<SLAPolicyPriority>().ToTable("SLAPolicyPriority");

            modelBuilder.Entity<SupportChannels>().Ignore(e => e.EntityId).HasKey(e => e.SupportChannelId); //Primary key
            modelBuilder.Entity<SupportChannels>().ToTable("SupportChannels");

            modelBuilder.Entity<Tickets>().Ignore(e => e.EntityId).HasKey(e => e.TicketId); //Primary key
            modelBuilder.Entity<Tickets>().ToTable("Tickets");

            modelBuilder.Entity<TicketStatus>().Ignore(e => e.EntityId).HasKey(e => e.TicketStatusId); //Primary key
            modelBuilder.Entity<TicketStatus>().ToTable("TicketStatus");

            modelBuilder.Entity<TicketTypes>().Ignore(e => e.EntityId).HasKey(e => e.TicketTypeId); //Primary key
            modelBuilder.Entity<TicketTypes>().ToTable("TicketTypes");

            modelBuilder.Entity<UserGroup>().Ignore(e => e.EntityId).HasKey(e => e.UserGroupId); //Primary key
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");

            modelBuilder.Entity<UserRole>().Ignore(e => e.EntityId).HasKey(e => e.UserRoleId); //Primary key
            modelBuilder.Entity<UserRole>().ToTable("UserRole");

            modelBuilder.Entity<Users>().Ignore(e => e.EntityId).HasKey(e => e.UserId); //Primary key
            modelBuilder.Entity<Users>().ToTable("Users");

            modelBuilder.Entity<RuleCases>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<RuleCases>().ToTable("RuleCases");

            modelBuilder.Entity<RuleConditions>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<RuleConditions>().ToTable("RuleConditions");

            modelBuilder.Entity<TicketRules>().Ignore(e => e.EntityId).HasKey(e => e.RuleId); //Primary key
            modelBuilder.Entity<TicketRules>().ToTable("TicketRules");

            modelBuilder.Entity<RuleAction_2>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<RuleAction_2>().ToTable("RuleAction_2");

            modelBuilder.Entity<RuleTicketCreation>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<RuleTicketCreation>().ToTable("RuleTicketCreation");

            modelBuilder.Entity<PendingEmail>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<PendingEmail>().ToTable("PendingEmail");

            modelBuilder.Entity<RuleAction>().Ignore(e => e.EntityId).HasKey(e => e.RuleActionId); //Primary key
            modelBuilder.Entity<RuleAction>().ToTable("RuleAction");

            modelBuilder.Entity<ReceivedIssueMails>().Ignore(e => e.EntityId).HasKey(e => e.Id); //Primary key
            modelBuilder.Entity<ReceivedIssueMails>().ToTable("ReceivedIssueMails");

            modelBuilder.Entity<ActionProperty>().Ignore(e => e.EntityId).HasKey(e => e.ActionPropertyId); //Primary key
            modelBuilder.Entity<ActionProperty>().ToTable("ActionProperty");

            modelBuilder.Entity<CustomMessages>().Ignore(e => e.EntityId).HasKey(e => e.CustomMessageId); //Primary key
            modelBuilder.Entity<CustomMessages>().ToTable("CustomMessages");

            modelBuilder.Entity<ReceivedEmailFilter>().Ignore(e => e.EntityId).HasKey(e => e.ReceivedEmailFilterId); //Primary key
            modelBuilder.Entity<ReceivedEmailFilter>().ToTable("ReceivedEmailFilter");

            modelBuilder.Entity<OrganizationDocuments>().Ignore(e => e.EntityId).HasKey(e => e.OrganizationDocumentId); //Primary key
            modelBuilder.Entity<OrganizationDocuments>().ToTable("OrganizationDocuments");


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        ////public async Task<int> SaveChanges()
        ////{
        ////    return await base.SaveChangesAsync();
        ////}
        //public int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}

    }
}
