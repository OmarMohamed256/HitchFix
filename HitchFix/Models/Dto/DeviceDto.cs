namespace HitchFix.Models.Dto
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<DeviceProblem> DeviceProblems { get; set; }
    }
}
