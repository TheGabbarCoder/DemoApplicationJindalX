namespace Server.Models
{
    public class PrioritySetup
    {
        public int Id { get; set; }

        public int PayerId { get; set; }
        public Payers Payer { get; set; }

        public int AgeingBucketId { get; set; }
        public AgeingBucket AgeingBucket { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int PriorityTypeId { get; set; }
        public PriorityType PriorityType { get; set; }

        public decimal TotalBalance { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
