namespace TipTracker.Models
{
    public class Tip
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Shift { get; set; }
    }
}
