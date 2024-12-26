using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace Hexstor.Module.Template.Repository
{
    public class TemplateRepository : ITransientService
    {
        private readonly IDbContextFactory<TemplateContext> _factory;

        public TemplateRepository(IDbContextFactory<TemplateContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Template> GetTemplates()
        {
            using var db = _factory.CreateDbContext();
            return db.Template.ToList();
        }

        public Models.Template GetTemplate(int TemplateId)
        {
            return GetTemplate(TemplateId, true);
        }

        public Models.Template GetTemplate(int TemplateId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Template.Find(TemplateId);
            }
            else
            {
                return db.Template.AsNoTracking().FirstOrDefault(item => item.TemplateId == TemplateId);
            }
        }

        public Models.Template AddTemplate(Models.Template Template)
        {
            using var db = _factory.CreateDbContext();
            db.Template.Add(Template);
            db.SaveChanges();
            return Template;
        }

        public Models.Template UpdateTemplate(Models.Template Template)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Template).State = EntityState.Modified;
            db.SaveChanges();
            return Template;
        }

        public void DeleteTemplate(int TemplateId)
        {
            using var db = _factory.CreateDbContext();
            Models.Template Template = db.Template.Find(TemplateId);
            db.Template.Remove(Template);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.Template>> GetTemplatesAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.Template.Where(item => item.ModuleId == ModuleId).ToListAsync();
        }

        public async Task<Models.Template> GetTemplateAsync(int TemplateId)
        {
            return await GetTemplateAsync(TemplateId, true);
        }

        public async Task<Models.Template> GetTemplateAsync(int TemplateId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.Template.FindAsync(TemplateId);
            }
            else
            {
                return await db.Template.AsNoTracking().FirstOrDefaultAsync(item => item.TemplateId == TemplateId);
            }
        }

        public async Task<Models.Template> AddTemplateAsync(Models.Template Template)
        {
            using var db = _factory.CreateDbContext();
            db.Template.Add(Template);
            await db.SaveChangesAsync();
            return Template;
        }

        public async Task<Models.Template> UpdateTemplateAsync(Models.Template Template)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Template).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Template;
        }

        public async Task DeleteTemplateAsync(int TemplateId)
        {
            using var db = _factory.CreateDbContext();
            Models.Template Template = db.Template.Find(TemplateId);
            db.Template.Remove(Template);
            await db.SaveChangesAsync();
        }
    }
}
