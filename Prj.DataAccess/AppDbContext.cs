using Microsoft.AspNet.Identity.EntityFramework;
using Prj.Domain.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prj.DataAccess
{
public class AppDbContext : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim>
    {
        public AppDbContext() : base("Default")
        {
            this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
        }

        static AppDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserClaim>().ToTable("User_Claims");
            builder.Entity<UserRole>().ToTable("User_Roles");
            builder.Entity<UserLogin>().ToTable("User_Logins");

            builder.Entity<UserRole>()
               .HasRequired(c => c.User)
               .WithMany(p => p.Roles)
               .HasForeignKey(c => c.UserId)
               .WillCascadeOnDelete(true);

            builder.Entity<UserRole>()
               .HasRequired(c => c.Role)
               .WithMany(p => p.Users)
               .HasForeignKey(c => c.RoleId)
               .WillCascadeOnDelete(true);

        }
        #region User
        public virtual DbSet<UserToken> UserTokens { get; set; }

        #endregion
       
    }
}
