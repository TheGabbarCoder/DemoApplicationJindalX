namespace Server.Models
{
    public class PriorityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PrioritySetup> PrioritySetups { get; set; }
    }
}
