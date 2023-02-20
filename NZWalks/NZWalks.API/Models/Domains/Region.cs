namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string code { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public double Lat { get; set; }

        public double Long {get; set;}

        public long Population { get; set; }

        public IEnumerable<Walk> Walks { get; set; }
    }
}
