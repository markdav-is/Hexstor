using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace Devin.Module.Targeting.Repository
{
    public class TargetingRepository : ITargetingRepository, ITransientService
    {
        private readonly IDbContextFactory<TargetingContext> _factory;

        public TargetingRepository(IDbContextFactory<TargetingContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Targeting> GetTargetings(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Targeting.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Targeting GetTargeting(int TargetingId)
        {
            return GetTargeting(TargetingId, true);
        }

        public Models.Targeting GetTargeting(int TargetingId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Targeting.Find(TargetingId);
            }
            else
            {
                return db.Targeting.AsNoTracking().FirstOrDefault(item => item.TargetingId == TargetingId);
            }
        }

        public Models.Targeting AddTargeting(Models.Targeting Targeting)
        {
            using var db = _factory.CreateDbContext();
            db.Targeting.Add(Targeting);
            db.SaveChanges();
            return Targeting;
        }

        public Models.Targeting UpdateTargeting(Models.Targeting Targeting)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Targeting).State = EntityState.Modified;
            db.SaveChanges();
            return Targeting;
        }

        public void DeleteTargeting(int TargetingId)
        {
            using var db = _factory.CreateDbContext();
            Models.Targeting Targeting = db.Targeting.Find(TargetingId);
            db.Targeting.Remove(Targeting);
            db.SaveChanges();
        }
    }
}
