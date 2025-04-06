using System.Text.Json;
using FormSubmissionApi.Models;

namespace FormSubmissionApi.Services
{
    public interface ISubmissionService
    {
        Task<IEnumerable<object>> GetAllSubmissionsAsync();
        Task<FormSubmission> AddSubmissionAsync(JsonElement formData);
    }
}