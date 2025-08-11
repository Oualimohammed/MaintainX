namespace Pri.Ek2.Client.Models
{
    public class PriorityTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Link { get; set; }
    }
}
