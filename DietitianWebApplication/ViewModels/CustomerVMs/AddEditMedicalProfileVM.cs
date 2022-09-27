using DietitianWebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace DietitianWebApplication.ViewModels.CustomerVMs
{
    public class AddEditMedicalProfileVM
    {
        public int ID { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        [Display(Name = "Goal OF Diet")]
        public string GoalOfDiet { get; set; }
        [Display(Name = "Exercise Time")]
        public string ExerciseTime { get; set; }
        [Display(Name = "Level Of Exercise")]
        public string LevelOfExercise { get; set; }
        [Display(Name = "Daily Food")]
        public string DailyFood { get; set; }
        public string Vitamins { get; set; }
        [Display(Name = "Problem History")]
        public string ProblemHistory { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
    }
}
