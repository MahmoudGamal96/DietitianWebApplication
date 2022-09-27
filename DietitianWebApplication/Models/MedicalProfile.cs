namespace DietitianWebApplication.Models
{
    public class MedicalProfile
    {
        public int ID { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string GoalOfDiet { get; set; }
        public string ExerciseTime { get; set; }
        public string LevelOfExercise { get; set; }
        public string DailyFood { get; set; }
        public string Vitamins { get; set; }
        public string ProblemHistory { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
