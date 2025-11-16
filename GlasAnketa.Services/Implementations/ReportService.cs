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

                var report = questions.Select(q =>
                {
                    var allAnswers = q.Answers?.ToList() ?? new List<Answer>();

                    var scaleAnswers = allAnswers.Where(a => a.ScaleValue.HasValue).ToList();
                    var textAnswers = allAnswers
                        .Where(a => !string.IsNullOrWhiteSpace(a.TextValue))
                        .ToList();

                    var totalResponses = allAnswers.Count;
                    var totalScale = scaleAnswers.Sum(a => a.ScaleValue.GetValueOrDefault());
                    var avg = scaleAnswers.Any()
                        ? scaleAnswers.Average(a => (double)a.ScaleValue.GetValueOrDefault())
                        : 0;

                    var companyIds = allAnswers
                        .Select(a => a.CompanyId)
                        .Distinct()
                        .OrderBy(x => x)
                        .ToList();

                    var sampleTexts = textAnswers
                        .Select(a => a.TextValue!)
                        .Where(t => !string.IsNullOrWhiteSpace(t))
                        .Distinct()
                        .Take(5)
                        .ToList();

                    var sampleTextJoined = string.Join(" | ", sampleTexts);

                    return new QuestionReportVM
                    {
                        QuestionId = q.Id,
                        QuestionText = q.Text ?? "No Question Text",
                        QuestionFormId = q.QuestionFormId,
                        FormTitle = q.QuestionForm?.Title ?? "No Form Title",
                        TotalResponses = totalResponses,
                        TotalScaleValue = totalScale,
                        AverageScaleValue = avg,
                        CompanyIdList = string.Join(", ", companyIds),
                        TextResponseCount = textAnswers.Count,
                        SampleTextResponses = sampleTextJoined
                    };
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

                var report = forms.Select(f =>
                {
                    var scaleAnswers = f.Questions?
                        .SelectMany(q => q.Answers?.Where(a => a.ScaleValue.HasValue) ?? new List<Answer>())
                        .ToList() ?? new List<Answer>();

                    var totalResponses = scaleAnswers.Count;
                    var totalScale = scaleAnswers.Sum(a => a.ScaleValue.GetValueOrDefault());
                    var avg = totalResponses > 0 ? scaleAnswers.Average(a => (double)a.ScaleValue.GetValueOrDefault()) : 0;
                    var companyIds = scaleAnswers.Select(a => a.CompanyId).Distinct().OrderBy(x => x).ToList();

                    return new FormReportVM
                    {
                        FormId = f.Id,
                        FormTitle = f.Title ?? "No Form Title",
                        FormDescription = f.Description ?? "No Description",
                        TotalResponses = totalResponses,
                        TotalScaleValue = totalScale,
                        AverageScaleValue = avg,
                        QuestionCount = f.Questions?.Count() ?? 0,
                        CompanyIdList = string.Join(", ", companyIds)
                    };
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

                    return answersByOU.Select(g =>
                    {
                        var scaleAnswers = g.Where(a => a.ScaleValue.HasValue).ToList();
                        var totalResponses = scaleAnswers.Count;
                        var totalScale = scaleAnswers.Sum(a => a.ScaleValue.GetValueOrDefault());
                        var avg = totalResponses > 0 ? scaleAnswers.Average(a => (double)a.ScaleValue.GetValueOrDefault()) : 0;
                        var companyIds = scaleAnswers.Select(a => a.CompanyId).Distinct().OrderBy(x => x).ToList();

                        return new QuestionByOUMReportVM
                        {
                            QuestionId = q.Id,
                            QuestionText = q.Text ?? "No Question Text",
                            OU = g.Key.OU ?? "Unknown OU",
                            OU2 = g.Key.OU2 ?? "Unknown OU2",
                            TotalResponses = totalResponses,
                            TotalScaleValue = totalScale,
                            AverageScaleValue = avg,
                            CompanyIdList = string.Join(", ", companyIds)
                        };
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

                    return answersByOU.Select(g =>
                    {
                        var scaleAnswers = g.Where(a => a.ScaleValue.HasValue).ToList();
                        var totalResponses = scaleAnswers.Count;
                        var totalScale = scaleAnswers.Sum(a => a.ScaleValue.GetValueOrDefault());
                        var avg = totalResponses > 0 ? scaleAnswers.Average(a => (double)a.ScaleValue.GetValueOrDefault()) : 0;
                        var companyIds = scaleAnswers.Select(a => a.CompanyId).Distinct().OrderBy(x => x).ToList();

                        return new FormByOUMReportVM
                        {
                            FormId = f.Id,
                            FormTitle = f.Title ?? "No Form Title",
                            OU = g.Key.OU ?? "Unknown OU",
                            OU2 = g.Key.OU2 ?? "Unknown OU2",
                            TotalResponses = totalResponses,
                            TotalScaleValue = totalScale,
                            AverageScaleValue = avg,
                            CompanyIdList = string.Join(", ", companyIds)
                        };
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
                var answersQuery = _context.Answers.AsQueryable();

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
                        .Where(a => a.Age > 0 && ((bucket.Min == null || a.Age >= (bucket.Min ?? int.MinValue)) &&
                                     (bucket.Max == null || a.Age <= (bucket.Max ?? int.MaxValue))))
                        .ToList();

                    var bucketScaleAnswers = bucketAnswers.Where(a => a.ScaleValue.HasValue).ToList();

                    // Distinct users (CompanyId) in this bucket
                    var countUsers = bucketScaleAnswers.Select(a => a.CompanyId).Distinct().Count();

                    // Total questions answered (scale answers)
                    var questionsAnswered = bucketScaleAnswers.Count;

                    if (questionsAnswered == 0)
                    {
                        report.Add(new AgeReportVM
                        {
                            AgeRange = bucket.Label,
                            CompanyId = companyId,
                            CountUsers = 0,
                            QuestionsAnswered = 0,
                            TotalScaleValue = 0,
                            TotalResponses = 0,
                            AverageScaleValue = 0
                        });
                        continue;
                    }

                    var totalScale = bucketScaleAnswers.Sum(a => a.ScaleValue!.Value);
                    var avgScale = questionsAnswered > 0 ? totalScale / (double)questionsAnswered : 0.0;
                    var companyIds = bucketScaleAnswers.Select(a => a.CompanyId).Distinct().OrderBy(x => x).ToList();

                    report.Add(new AgeReportVM
                    {
                        AgeRange = bucket.Label,
                        CompanyId = companyId,
                        CountUsers = countUsers,
                        QuestionsAnswered = questionsAnswered,
                        TotalScaleValue = totalScale,
                        CompanyIdList = string.Join(", ", companyIds),
                        TotalResponses = countUsers,
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

        public async Task<List<WorkExperienceReportVM>> GetWorkExperienceReportsAsync(int? companyId = null)
        {
            try
            {
                var answersQuery = _context.Answers.AsQueryable();

                if (companyId.HasValue)
                {
                    answersQuery = answersQuery.Where(a => a.CompanyId == companyId.Value);
                }

                var answers = await answersQuery.ToListAsync();
                if (!answers.Any())
                {
                    _logger.LogInformation("No answers found for work experience reports. Returning zeroed buckets.");
                }

                // Define work experience buckets (in years)
                var buckets = new List<(string Label, int? Min, int? Max)>
                {
                    ("< 5", null, 4),
                    ("5 - 10", 5, 10),
                    ("10 - 15", 10, 15),
                    ("15 - 20", 15, 20),
                    ("> 20", 21, null)
                };

                var report = new List<WorkExperienceReportVM>();

                foreach (var bucket in buckets)
                {
                    var bucketAnswers = answers
                        .Where(a => a.WorkExperience > 0 &&
                                    ((bucket.Min == null || a.WorkExperience >= (bucket.Min ?? int.MinValue)) &&
                                     (bucket.Max == null || a.WorkExperience <= (bucket.Max ?? int.MaxValue))))
                        .ToList();

                    var bucketScaleAnswers = bucketAnswers.Where(a => a.ScaleValue.HasValue).ToList();

                    var countUsers = bucketScaleAnswers.Select(a => a.CompanyId).Distinct().Count();
                    var questionsAnswered = bucketScaleAnswers.Count;

                    if (questionsAnswered == 0)
                    {
                        report.Add(new WorkExperienceReportVM
                        {
                            ExperienceRange = bucket.Label,
                            CompanyId = companyId,
                            CountUsers = 0,
                            QuestionsAnswered = 0,
                            TotalScaleValue = 0,
                            TotalResponses = 0,
                            AverageScaleValue = 0
                        });
                        continue;
                    }

                    var totalScale = bucketScaleAnswers.Sum(a => a.ScaleValue!.Value);
                    var avgScale = questionsAnswered > 0 ? totalScale / (double)questionsAnswered : 0.0;
                    var companyIds = bucketScaleAnswers.Select(a => a.CompanyId).Distinct().OrderBy(x => x).ToList();

                    report.Add(new WorkExperienceReportVM
                    {
                        ExperienceRange = bucket.Label,
                        CompanyId = companyId,
                        CountUsers = countUsers,
                        QuestionsAnswered = questionsAnswered,
                        TotalScaleValue = totalScale,
                        CompanyIdList = string.Join(", ", companyIds),
                        TotalResponses = countUsers,
                        AverageScaleValue = avgScale
                    });
                }

                _logger.LogInformation($"Generated work experience reports with {report.Count} buckets{(companyId.HasValue ? $" for company {companyId.Value}" : string.Empty)}");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating work experience reports");
                throw;
            }
        }
    }
}
