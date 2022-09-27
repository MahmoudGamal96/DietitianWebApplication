using DietitianWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietitianWebApplication.Configurations
{
    public class MedicalProfileEntityTypeConfiguration : IEntityTypeConfiguration<MedicalProfile>
    {
        public void Configure(EntityTypeBuilder<MedicalProfile> builder)
        {
            builder
                .HasKey(m => m.ID);

            builder
                .Property(m => m.Weight)
                .IsRequired();

            builder
                .Property(m => m.Height)
                .IsRequired();

            builder
                .Property(m => m.GoalOfDiet)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.ExerciseTime)
                .IsRequired();

            builder
                .Property(m => m.LevelOfExercise)
                .IsRequired();

            builder
                .Property(m => m.DailyFood)
                .IsRequired();

            builder
                .Property(m => m.Vitamins)
                .IsRequired();

            builder
                .Property(m => m.ProblemHistory)
                .IsRequired();
        }
    }
}