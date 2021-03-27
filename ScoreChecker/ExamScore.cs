// James Odeyale - Group 1

namespace ScoreChecker
{
    public enum GradeSystem
    {
        A,
        B,
        C,
        D,
        F
    }
    class ExamScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public GradeSystem Grade { get; set; }
        public override string ToString()
        {
            return $"{Name} - {Score} - {Grade}";
        }
    }
}
