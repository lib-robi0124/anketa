
using Anketa.DataAccess.DataContext;
using Anketa.DataAccess.Interfaces;
using Anketa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Anketa.DataAccess.Implementations
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context) { }
        public async Task<List<Question>> GetByCompanyIdAsync(int companyId)
               => await _context.Questions.Where(q => q.CompanyId == companyId).ToListAsync();
       
        public async Task<QuestionForm> GetFormWithQuestionsAsync(int formId)
        {
            return await _context.QuestionForms
                .Include(f => f.Questions)
                .ThenInclude(q => q.QuestionType)
                .FirstOrDefaultAsync(f => f.Id == formId);
        }
    }
}