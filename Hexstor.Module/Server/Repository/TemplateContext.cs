using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Hexstor.Module.Template.Repository
{
    public class TemplateContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Template> Template { get; set; }

        public TemplateContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Template>().ToTable(ActiveDatabase.RewriteName("HexstorTemplate"));
        }
    }
}
