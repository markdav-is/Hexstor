using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Devin.Module.Targeting.Repository
{
    public class TargetingContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Targeting> Targeting { get; set; }

        public TargetingContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Targeting>().ToTable(ActiveDatabase.RewriteName("DevinTargeting"));
        }
    }
}
