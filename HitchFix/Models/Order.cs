namespace HitchFix.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public DateTime OrderTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderProblem> OrderProblems { get; set; }


    }
}
