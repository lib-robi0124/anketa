using GlasAnketa.DataAccess.DataContext;
using GlasAnketa.DataAccess.Interfaces;
using GlasAnketa.Domain.Models;
using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GlasAnketa.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionFormRepository _formRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ReportService> _logger;
        private readonly AppDbContext _context;

        public ReportService(
            IAnswerRepository answerRepository,
            IQuestionRepository questionRepository,
            IQuestionFormRepository formRepository,
            IUserRepository userRepository,
            ILogger<ReportService> logger,
            AppDbContext context)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _formRepository = formRepository;
            _userRepository = userRepository;
            _logger = logger;
            _context = context;
        }

        public async Task<List<QuestionReportVM>> GetQuestionReportsAsync()
        {
            try
            {
                // Use efficient query with includes to avoid N+1 problems
                var questions = await _context.Questions
                    .Include(q => q.QuestionForm)
                    .Include(q => q.Answers)
                    .ToListAsync();

                if (!questions.Any())
                {
                    _logger.LogInformation("No questions found for reports");
                    return new List<QuestionReportVM>();
                }

                var report = questions.Select(q => new QuestionReportVM
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text ?? "No Question Text",
                    FormTitle = q.QuestionForm?.Title ?? "No Form Title",
                    TotalResponses = q.Answers?.Count(a => a.ScaleValue.HasValue) ?? 0,
                    TotalScaleValue = q.Answers?.Where(a => a.ScaleValue.HasValue).Sum(a => a.ScaleValue.GetValueOrDefault()) ?? 0,
                    AverageScaleValue = q.Answers?.Any(a => a.ScaleValue.HasValue) == true 
                        ? q.Answers.Where(a => a.ScaleValue.HasValue).Average(a => (double)a.ScaleValue.GetValueOrDefault()) 
                        : 0,
                    ScaleValuePercentage = CalculateScalePercentage(q.Answers?.Where(a => a.ScaleValue.HasValue).Select(a => a.ScaleValue.GetValueOrDefault()).ToList() ?? new List<int>())
                }).ToList();

                _logger.LogInformation($"Generated question reports with {report.Count} entries");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating question reports");
                throw;
            }
        }

        public async Task<List<FormReportVM>> GetFormReportsAsync()
        {
            try
            {
                // Use efficient query with includes to avoid N+1 problems
                var forms = await _context.QuestionForms
                    .Include(f => f.Questions)
                        .ThenInclude(q => q.Answers)
                    .ToListAsync();

                if (!forms.Any())
                {
                    _logger.LogInformation("No forms found for reports");
                    return new List<FormReportVM>();
                }

                var report = forms.Select(f => new FormReportVM
                {
                    FormId = f.Id,
                    FormTitle = f.Title ?? "No Form Title",
                    FormDescription = f.Description ?? "No Description",
                    TotalResponses = f.Questions?.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>()).Count() ?? 0,
                    TotalScaleValue = f.Questions?.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>())
                        .Sum(a => a.ScaleValue.GetValueOrDefault()) ?? 0,
                    AverageScaleValue = f.Questions?.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>()).Any() == true
                        ? f.Questions.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>())
                            .Average(a => (double)a.ScaleValue.GetValueOrDefault())
                        : 0,
                    ScaleValuePercentage = CalculateFormScalePercentage(f.Id, f.Questions?.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>()).ToList() ?? new List<Answer>(), f.Questions?.ToList() ?? new List<Question>()),
                    QuestionCount = f.Questions?.Count() ?? 0
                }).ToList();

                _logger.LogInformation($"Generated form reports with {report.Count} entries");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating form reports");
                throw;
            }
        }

        public async Task<List<QuestionByOUMReportVM>> GetQuestionByOUReportsAsync()
        {
            try
            {
                // Use efficient query with includes to avoid N+1 problems
                var questions = await _context.Questions
                    .Include(q => q.QuestionForm)
                    .Include(q => q.Answers)
                        .ThenInclude(a => a.User)
                    .ToListAsync();

                if (!questions.Any())
                {
                    _logger.LogInformation("No questions found for question by OU reports");
                    return new List<QuestionByOUMReportVM>();
                }

                var report = questions.SelectMany(q =>
                {
                    var answersByOU = q.Answers?.Where(a => a.ScaleValue.HasValue)
                        .GroupBy(a => new { a.User?.OU, a.User?.OU2 })
                        .ToList();

                    if (answersByOU == null || !answersByOU.Any())
                    {
                        return Enumerable.Empty<QuestionByOUMReportVM>();
                    }

                    return answersByOU.Select(g => new QuestionByOUMReportVM
                    {
                        QuestionId = q.Id,
                        QuestionText = q.Text ?? "No Question Text",
                        OU = g.Key.OU ?? "Unknown OU",
                        OU2 = g.Key.OU2 ?? "Unknown OU2",
                        TotalResponses = g.Count(),
                        TotalScaleValue = g.Sum(a => a.ScaleValue.GetValueOrDefault()),
                        AverageScaleValue = g.Any() ? g.Average(a => (double)a.ScaleValue.GetValueOrDefault()) : 0,
                        ScaleValuePercentage = CalculateScalePercentage(g.Select(a => a.ScaleValue.GetValueOrDefault()).ToList())
                    });
                }).ToList();

                _logger.LogInformation($"Generated question by OU reports with {report.Count} entries");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating question by OU reports");
                throw;
            }
        }

        public async Task<List<FormByOUMReportVM>> GetFormByOUReportsAsync()
        {
            try
            {
                // Use efficient query with includes to avoid N+1 problems
                var forms = await _context.QuestionForms
                    .Include(f => f.Questions)
                        .ThenInclude(q => q.Answers)
                            .ThenInclude(a => a.User)
                    .ToListAsync();

                if (!forms.Any())
                {
                    _logger.LogInformation("No forms found for form by OU reports");
                    return new List<FormByOUMReportVM>();
                }

                var report = forms.SelectMany(f =>
                {
                    var answersByOU = f.Questions?.SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>())
                        .GroupBy(a => new { a.User?.OU, a.User?.OU2 })
                        .ToList();

                    if (answersByOU == null || !answersByOU.Any())
                    {
                        return Enumerable.Empty<FormByOUMReportVM>();
                    }

                    return answersByOU.Select(g => new FormByOUMReportVM
                    {
                        FormId = f.Id,
                        FormTitle = f.Title ?? "No Form Title",
                        OU = g.Key.OU ?? "Unknown OU",
                        OU2 = g.Key.OU2 ?? "Unknown OU2",
                        TotalResponses = g.Count(),
                        TotalScaleValue = g.Sum(a => a.ScaleValue.GetValueOrDefault()),
                        AverageScaleValue = g.Any() ? g.Average(a => (double)a.ScaleValue.GetValueOrDefault()) : 0,
                        ScaleValuePercentage = CalculateFormScalePercentage(f.Id, g.ToList(), f.Questions?.ToList() ?? new List<Question>())
                    });
                }).ToList();

                _logger.LogInformation($"Generated form by OU reports with {report.Count} entries");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating form by OU reports");
                throw;
            }
        }

        public async Task<ReportSummaryVM> GetReportSummaryAsync()
        {
            try
            {
                // Use efficient queries with counts to avoid loading all data
                var totalUsers = await _context.Users.CountAsync();
                var totalForms = await _context.QuestionForms.CountAsync();
                var totalQuestions = await _context.Questions.CountAsync();
                var totalAnswers = await _context.Answers.CountAsync();

                // Calculate form completion rate efficiently
                var distinctUserFormAnswers = await _context.Answers
                    .Where(a => a.QuestionFormId > 0)
                    .Select(a => new { a.UserId, a.QuestionFormId })
                    .Distinct()
                    .CountAsync();

                var formCompletionRate = totalUsers > 0 && totalForms > 0 ? 
                    (double)distinctUserFormAnswers / (totalUsers * totalForms) * 100 : 0;

                var totalScaleAnswers = await _context.Answers.CountAsync(a => a.ScaleValue.HasValue);
                var totalTextAnswers = await _context.Answers.CountAsync(a => !string.IsNullOrEmpty(a.TextValue));

                var summary = new ReportSummaryVM
                {
                    TotalUsers = totalUsers,
                    TotalForms = totalForms,
                    TotalQuestions = totalQuestions,
                    TotalAnswers = totalAnswers,
                    TotalScaleAnswers = totalScaleAnswers,
                    TotalTextAnswers = totalTextAnswers,
                    AverageFormCompletion = Math.Round(formCompletionRate, 2)
                };

                _logger.LogInformation($"Generated report summary: {totalUsers} users, {totalForms} forms, {totalQuestions} questions, {totalAnswers} answers");
                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating report summary");
                throw;
            }
        }

        public async Task<List<AgeReportVM>> GetAgeReportsAsync(int? companyId = null)
        {
            try
            {
                var answersQuery = _context.Answers
                    .Include(a => a.User)
                    .Where(a => a.ScaleValue.HasValue);

                if (companyId.HasValue)
                {
                    answersQuery = answersQuery.Where(a => a.CompanyId == companyId.Value);
                }

                var answers = await answersQuery.ToListAsync();
                if (!answers.Any())
                {
                    _logger.LogInformation("No answers found for age reports. Returning zeroed buckets.");
                }

                // Define age buckets
                var buckets = new List<(string Label, int? Min, int? Max)>
                {
                    ("< 25", null, 24),
                    ("25 - 30", 25, 30),
                    ("31 - 40", 31, 40),
                    ("41 - 50", 41, 50),
                    ("> 50", 51, null)
                };

                var report = new List<AgeReportVM>();

                foreach (var bucket in buckets)
                {
                    var bucketAnswers = answers
                        .Where(a => a.User != null &&
                                    ((bucket.Min == null || a.User.Age >= bucket.Min) &&
                                     (bucket.Max == null || a.User.Age <= bucket.Max)))
                        .ToList();

                    var totalIN = bucketAnswers.Count;
                    if (totalIN == 0)
                    {
                        report.Add(new AgeReportVM
                        {
                            AgeRange = bucket.Label,
                            CompanyId = companyId,
                            TotalResponses = 0,
                            AverageScaleValue = 0
                        });
                        continue;
                    }

                    var totalScale = bucketAnswers.Sum(a => a.ScaleValue!.Value);
                    var avgScale = totalIN > 0 ? totalScale / (double)totalIN : 0.0;

                    report.Add(new AgeReportVM
                    {
                        AgeRange = bucket.Label,
                        CompanyId = companyId,
                        TotalResponses = totalIN,
                        AverageScaleValue = avgScale
                    });
                }

                _logger.LogInformation($"Generated age reports with {report.Count} buckets{(companyId.HasValue ? $" for company {companyId.Value}" : string.Empty)}");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating age reports");
                throw;
            }
        }

        private double CalculateScalePercentage(List<int> scaleValues)
        {
            try
            {
                if (scaleValues == null || !scaleValues.Any())
                {
                    _logger.LogInformation("CalculateScalePercentage: No scale values provided");
                    return 0;
                }

                var maxPossibleValue = scaleValues.Count * 5; // Assuming max scale value is 5
                var totalValue = scaleValues.Sum();
                var percentage = ((double)totalValue / maxPossibleValue) * 100;
                
                _logger.LogInformation($"CalculateScalePercentage: {scaleValues.Count} values, total {totalValue}, max possible {maxPossibleValue}, percentage {percentage:F2}%");
                return percentage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating scale percentage");
                return 0;
            }
        }

        private double CalculateFormScalePercentage(int formId, List<Answer> answers, List<Question> questions)
        {
            try
            {
                var formAnswers = answers.Where(a => a.QuestionFormId == formId && a.ScaleValue.HasValue).ToList();
                var formQuestions = questions.Where(q => q.QuestionFormId == formId).ToList();

                if (formAnswers == null || !formAnswers.Any() || formQuestions == null || !formQuestions.Any())
                {
                    _logger.LogInformation($"CalculateFormScalePercentage: Insufficient data for form {formId}");
                    return 0;
                }

                var maxPossible = formAnswers.Count * 5; // Each answer max 5
                var actualTotal = formAnswers.Sum(a => a.ScaleValue.GetValueOrDefault());
                var percentage = (actualTotal / maxPossible) * 100;
                
                _logger.LogInformation($"CalculateFormScalePercentage: Form {formId}, {formAnswers.Count} answers, total {actualTotal}, max possible {maxPossible}, percentage {percentage:F2}%");
                return percentage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calculating form scale percentage for form {formId}");
                return 0;
            }
        }
    }
}
