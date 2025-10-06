using System.Collections.Generic;
using System.Threading.Tasks;

namespace codeXpert.Module.FAQ.Repository
{
    public interface IFAQRepository
    {
        IEnumerable<Models.FAQ> GetFAQs(int ModuleId);
        Models.FAQ GetFAQ(int FAQId);
        Models.FAQ GetFAQ(int FAQId, bool tracking);
        Models.FAQ AddFAQ(Models.FAQ FAQ);
        Models.FAQ UpdateFAQ(Models.FAQ FAQ);
        void DeleteFAQ(int FAQId);

        Task<IEnumerable<Models.FAQ>> GetFAQsAsync(int ModuleId);
        Task<Models.FAQ> GetFAQAsync(int FAQId);
        Task<Models.FAQ> GetFAQAsync(int FAQId, bool tracking);
        Task<Models.FAQ> AddFAQAsync(Models.FAQ FAQ);
        Task<Models.FAQ> UpdateFAQAsync(Models.FAQ FAQ);
        Task DeleteFAQAsync(int FAQId);
    }
}
