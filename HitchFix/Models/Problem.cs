namespace HitchFix.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string ProblemName { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }
        public int TotalPriceAfterDiscount { get; set; }
    }
}
