namespace HitchFrontEnd.Models
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }
        public string ModelName { get; set; }
        public DeviceTypeDto DeviceType { get; set; }
        public ICollection<DeviceProblemDto> DeviceProblems { get; set; }
    }
}
