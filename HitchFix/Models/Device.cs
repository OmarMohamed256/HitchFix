using System.ComponentModel.DataAnnotations.Schema;

namespace HitchFix.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<DeviceProblem> DeviceProblems { get; set; }
    }
}
