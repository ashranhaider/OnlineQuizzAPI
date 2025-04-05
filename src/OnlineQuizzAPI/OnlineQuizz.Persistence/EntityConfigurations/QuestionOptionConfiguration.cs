using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Persistence.EntityConfigurations
{
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            // Primary key configuration
            builder.HasKey(qo => qo.Id);

            // Configure OptionText as required
            builder.Property(qo => qo.OptionText)
                   .IsRequired()
                   .HasMaxLength(500); // Example max length for option text

            // Configure IsCorrect as required (optional, but commonly required for quiz-type entities)
            builder.Property(qo => qo.IsCorrect)
                   .IsRequired(); // IsCorrect should be a required field

            // Configure OptionImage as optional (byte array with size limit, e.g., 5MB)
            builder.Property(qo => qo.OptionImage)
                   .HasMaxLength(5 * 1024 * 1024) // 5 MB limit
                   .IsRequired(false); // Make it optional, since not all options may have images

            // Configure relationship with Question (Many-to-one)
            builder.HasOne(qo => qo.Question)
                   .WithMany(q => q.QuestionOptions) // One Question has many QuestionOptions
                   .HasForeignKey(qo => qo.QuestionId) // Foreign key for QuestionId in QuestionOption
                   .IsRequired(); // Relationship is required, meaning every QuestionOption must be associated with a Question
        }
    }
}
