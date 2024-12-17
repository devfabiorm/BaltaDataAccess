namespace BaltaDataAccess.Models
{
    public class Career
    {
        public Career()
        {
            Items = [];
        }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<CareerItem> Items { get; set; }
    }
}