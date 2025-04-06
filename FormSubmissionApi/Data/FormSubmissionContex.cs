using Microsoft.EntityFrameworkCore;
using FormSubmissionApi.Models;

namespace FormSubmissionApi.Data
{
    public class FormSubmissionContext : DbContext
    {
        public FormSubmissionContext(DbContextOptions<FormSubmissionContext> options)
            : base(options)
        {
        }

        public DbSet<FormSubmission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormSubmission>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<FormSubmission>()
                .Property(s => s.FormData)
                .IsRequired();

            modelBuilder.Entity<FormSubmission>()
                .Property(s => s.SubmittedAt)
                .IsRequired();
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(FormSubmissionContext context)
        {
            context.Database.EnsureCreated();

            if (context.Submissions.Any())
            {
                return;
            }

            var submissions = new FormSubmission[]
            {
                new FormSubmission
                {
                    FormData = @"{""name"":""Login page error - 500 Internal Server Error"",""category"":""Bug"",""date"":""2025-04-04"",""priority"":""Medium""}",
                    SubmittedAt = DateTime.Now.AddDays(-10)
                },
                new FormSubmission
                {
                    FormData = @"{""name"":""Feature suggestion: Dark Mode for the app"",""category"":""Feature Request"",""date"":""2025-04-20"",""priority"":""High""}",
                    SubmittedAt = DateTime.Now.AddDays(-5)
                },
                new FormSubmission
                {
                    FormData = @"{""name"":""Bug report: Unable to submit contact form after validation"",""category"":""Bug"",""date"":""2025-04-24"",""priority"":""Low""}",
                    SubmittedAt = DateTime.Now.AddDays(-3)
                },
                new FormSubmission
                {
                    FormData = @"{""name"":""Feature request: Multi-language support for the website"",""category"":""Feature Request"",""date"":""2025-04-25"",""priority"":""Medium""}",
                    SubmittedAt = DateTime.Now.AddDays(-1)
                },
                new FormSubmission
                {
                    FormData = @"{""name"":""Critical issue: App crashes on startup after recent update"",""category"":""Bug"",""date"":""2025-05-01"",""priority"":""Critical""}",
                    SubmittedAt = DateTime.Now.AddDays(2)
                }
            };

            foreach (var submission in submissions)
            {
                context.Submissions.Add(submission);
            }
            context.SaveChanges();
        }
    }
}