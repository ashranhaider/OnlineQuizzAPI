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
    public class AttemptConfiguration
    {
        public void Configure(EntityTypeBuilder<Attempt> builder)
        {
            builder.ToTable("Attempts");

            builder.HasKey(qa => qa.Id);

            builder.Property(qa => qa.CreatedBy)
                .IsRequired();

            builder.Property(qa => qa.CreatedDate)
                .IsRequired();

            builder.Property(qa => qa.TotalScore)
                .IsRequired();

            builder.HasOne(qa => qa.Quizz)
                .WithMany(q => q.Attempts)
                .HasForeignKey(qa => qa.QuizzId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(qa => qa.Answers)
                .WithOne(a => a.Attempt)
                .HasForeignKey(a => a.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
