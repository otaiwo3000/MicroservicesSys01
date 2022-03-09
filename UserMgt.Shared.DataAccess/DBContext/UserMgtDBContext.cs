using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Entities;

namespace UserMgt.Shared.DataAccess.DBContext
{
    public partial class UserMgtDBContext : DbContext
    {
        public UserMgtDBContext(DbContextOptions<UserMgtDBContext> options)
       : base(options)
        {

        }
       
        public virtual DbSet<OrganizationContacts> OrganizationContactsSet { get; set; }
        public virtual DbSet<Organizations> OrganizationsSet { get; set; }
        public virtual DbSet<Privileges> PrivilegesSet { get; set; }
        public virtual DbSet<Roles> RolesSet { get; set; }
        public virtual DbSet<RolesPrivileges> RolesPrivilegesSet { get; set; }      
        public virtual DbSet<UserRole> UserRoleSet { get; set; }
        public virtual DbSet<Users> UsersSet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

           
          
            modelBuilder.Entity<OrganizationContacts>().Ignore(e => e.EntityId).HasKey(e => e.OrganizationContactId); //Primary key
            modelBuilder.Entity<OrganizationContacts>().ToTable("OrganizationContacts");
            modelBuilder.Entity<Organizations>().Ignore(e => e.EntityId).HasKey(e => e.OrganizationId); //Primary key
            modelBuilder.Entity<Organizations>().ToTable("Organizations");
            modelBuilder.Entity<Privileges>().Ignore(e => e.EntityId).HasKey(e => e.PrivilegeId); //Primary key
            modelBuilder.Entity<Privileges>().ToTable("Privileges");
            modelBuilder.Entity<Roles>().Ignore(e => e.EntityId).HasKey(e => e.RoleId); //Primary key
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<RolesPrivileges>().Ignore(e => e.EntityId).HasKey(e => e.RolePrivilegeId); //Primary key
            modelBuilder.Entity<RolesPrivileges>().ToTable("RolesPrivileges");
            modelBuilder.Entity<RolesPrivileges>().Property(c => c.RolePrivilegeId).HasColumnType("bigint");
            modelBuilder.Entity<UserRole>().Ignore(e => e.EntityId).HasKey(e => e.UserRoleId); //Primary key
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Users>().Ignore(e => e.EntityId).HasKey(e => e.UserId); //Primary key
            modelBuilder.Entity<Users>().ToTable("Users");


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
