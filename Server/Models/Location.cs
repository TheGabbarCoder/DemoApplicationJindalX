﻿namespace Server.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PrioritySetup> PrioritySetups { get; set; }
    }
}
