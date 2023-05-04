namespace HitchFrontEnd.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public DateTime OrderTime { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderProblemDto> OrderProblems { get; set; }
        public OrderUserDataDto UserDataDto { get; set; }
    }
