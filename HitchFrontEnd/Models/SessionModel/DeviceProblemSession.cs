namespace HitchFrontEnd.Models.SessionModel
{
    public class DeviceProblemSession
    {
        public int Id { get; set; }
        public string ProblemName { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        public bool IsChecked { get; set; }
    }
}
