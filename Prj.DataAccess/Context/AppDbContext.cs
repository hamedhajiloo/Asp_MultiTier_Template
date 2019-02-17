using Microsoft.AspNet.Identity.EntityFramework;
using Prj.Domain.Users;
using Prj.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prj.DataAccess.Context
{
public class AppDbContext : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim> , IUnitOfWork
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


        #region UnitOfWork
        //#################################################################
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        //#################################################################
        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }
        //#################################################################
        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }
        //#################################################################
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        //#################################################################
        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        //#################################################################
        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
        //#################################################################
        public void RejectChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        //#################################################################
        public async Task SqlExecute(string query, params object[] parameters)
        {
            await Database.ExecuteSqlCommandAsync(query, parameters);
        }
        //#################################################################
        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }
        //#################################################################
        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }
        //#################################################################


        #endregion

        #region User
        public virtual DbSet<UserToken> UserTokens { get; set; }

        #endregion
       
    }
}
