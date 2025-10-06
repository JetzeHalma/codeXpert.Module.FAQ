using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace codeXpert.Module.FAQ.Repository
{
    public class FAQRepository : IFAQRepository, ITransientService
    {
        private readonly IDbContextFactory<FAQContext> _factory;

        public FAQRepository(IDbContextFactory<FAQContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.FAQ> GetFAQs(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.FAQ.Where(item => item.ModuleId == ModuleId).OrderBy(e => e.Order).ToList();
        }

        public Models.FAQ GetFAQ(int FAQId)
        {
            return GetFAQ(FAQId, true);
        }

        public Models.FAQ GetFAQ(int FAQId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.FAQ.Find(FAQId);
            }
            else
            {
                return db.FAQ.AsNoTracking().FirstOrDefault(item => item.FAQId == FAQId);
            }
        }

        public Models.FAQ AddFAQ(Models.FAQ FAQ)
        {
            using var db = _factory.CreateDbContext();
            db.FAQ.Add(FAQ);
            db.SaveChanges();
            return FAQ;
        }

        public Models.FAQ UpdateFAQ(Models.FAQ FAQ)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(FAQ).State = EntityState.Modified;
            db.SaveChanges();
            return FAQ;
        }

        public void DeleteFAQ(int FAQId)
        {
            using var db = _factory.CreateDbContext();
            Models.FAQ FAQ = db.FAQ.Find(FAQId);
            db.FAQ.Remove(FAQ);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.FAQ>> GetFAQsAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.FAQ.Where(item => item.ModuleId == ModuleId).OrderBy(e => e.Order).ToListAsync();
        }

        public async Task<Models.FAQ> GetFAQAsync(int FAQId)
        {
            return await GetFAQAsync(FAQId, true);
        }

        public async Task<Models.FAQ> GetFAQAsync(int FAQId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.FAQ.FindAsync(FAQId);
            }
            else
            {
                return await db.FAQ.AsNoTracking().FirstOrDefaultAsync(item => item.FAQId == FAQId);
            }
        }

        public async Task<Models.FAQ> AddFAQAsync(Models.FAQ FAQ)
        {
            using var db = _factory.CreateDbContext();
            db.FAQ.Add(FAQ);
            await db.SaveChangesAsync();
            return FAQ;
        }

        public async Task<Models.FAQ> UpdateFAQAsync(Models.FAQ FAQ)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(FAQ).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return FAQ;
        }

        public async Task DeleteFAQAsync(int FAQId)
        {
            using var db = _factory.CreateDbContext();
            Models.FAQ FAQ = db.FAQ.Find(FAQId);
            db.FAQ.Remove(FAQ);
            await db.SaveChangesAsync();
        }
    }
}
