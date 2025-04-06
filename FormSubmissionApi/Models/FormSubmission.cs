using System.ComponentModel.DataAnnotations;

namespace FormSubmissionApi.Models
{
    public class FormSubmission
    {
        public int Id { get; set; }
        
        [Required]
        public string FormData { get; set; }
        
        [Required]
        public DateTime SubmittedAt { get; set; }
    }
}