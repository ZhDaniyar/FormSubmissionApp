using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using FormSubmissionApi.Data;
using FormSubmissionApi.Models;

namespace FormSubmissionApi.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly FormSubmissionContext _context;

        public SubmissionService(FormSubmissionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllSubmissionsAsync()
        {
            var submissions = await _context.Submissions
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();

            return submissions.Select(s => new
            {
                id = s.Id,
                data = JsonDocument.Parse(s.FormData).RootElement,
                submittedAt = s.SubmittedAt
            }).ToList();
        }

        public async Task<FormSubmission> AddSubmissionAsync(JsonElement formData)
        {
            var submission = new FormSubmission
            {
                FormData = formData.ToString(),
                SubmittedAt = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return submission;
        }
    }
}