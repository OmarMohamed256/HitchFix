﻿namespace HitchFix.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public DateTime OrderTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OrderStatus { get; set; }
        public string Location { get; set; }
        public Device Device { get; set; }
        public List<OrderProblem> OrderProblems { get; set; }
    }
}
