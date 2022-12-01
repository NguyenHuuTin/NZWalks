namespace NZWalks.API.Domain.Models
{
    public class WalkDifficulty
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public IEnumerable<Walk> Walks { get; set; }
    }
}
