namespace BuildYourself.Domain.Enities
{
    public class GymTask
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public DateTime TaskDate { get; set; }
        public GymCategory Category { get; set; }
    }
}
