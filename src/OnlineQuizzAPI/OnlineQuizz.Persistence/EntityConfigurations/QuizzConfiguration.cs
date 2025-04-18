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
    public class QuizzConfiguration : IEntityTypeConfiguration<Quizz>
    {
        public void Configure(EntityTypeBuilder<Quizz> builder)
        {
            // Primary key configuration
            builder.HasKey(qz => qz.Id);

            // Configure Name as required and with a max length
            builder.Property(qz => qz.Name)
                   .IsRequired() // Make it required
                   .HasMaxLength(200); // Example max length for quiz name

            // Configure URL as required and with a max length
            builder.Property(qz => qz.URL)
                   .IsRequired(false) // Make it optional
                   .HasMaxLength(500); // Example max length for URL

            // Configure IsActive as required
            builder.Property(qz => qz.IsActive)
                   .IsRequired(); // Ensure it's required to have an active state

            // Configure relationship with Questions (One-to-many)
            builder.HasMany(qz => qz.Questions) // One Quizz has many Questions
                   .WithOne(q => q.Quizz) // Each Question has one Quizz
                   .HasForeignKey(q => q.QuizzId) // Foreign key is QuizzId in Question
                   .IsRequired(); // Relationship is required (every Question must be part of a Quizz)

            builder.HasMany(q => q.Attempts)
                .WithOne(a => a.Quizz) // Each Attempt has one Quizz
                     .HasForeignKey(a => a.QuizzId) // Foreign key is QuizzId in Attempt
                     .IsRequired(); // Relationship is required (every Attempt must be part of a Quizz)
        }
    }

}
