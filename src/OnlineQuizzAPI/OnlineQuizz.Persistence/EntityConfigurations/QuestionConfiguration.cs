using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Persistence.EntityConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(o => o.Id);

            // Configure the relationship with Answers (One-to-many, optional)
            builder.HasMany(q => q.Answers)
                   .WithOne(a => a.Question)
                   .HasForeignKey(a => a.QuestionId)
                   .IsRequired(false); // No answers yet by default

            // Configure the relationship with QuestionOptions (One-to-many, optional)
            builder.HasMany(q => q.QuestionOptions)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId)
                   .IsRequired(false); // No options for some questions

            // Configure QuestionText as required
            builder.Property(q => q.QuestionText)
                   .IsRequired()
                   .HasMaxLength(500); // Example max length constraint

            // Configure QuestionType enum (as a value type)
            builder.Property(q => q.QuestionType)
                   .HasConversion<int>(); // Assuming QuestionType is an enum, convert to integer for storage

            // Configure QuestionImage as optional (byte array)
            builder.Property(q => q.QuestionImage)
                   .HasMaxLength(5 * 1024 * 1024) // 5 MB in bytes
                   .IsRequired(false); // Make it optional (nullable)
        }
    }
}
