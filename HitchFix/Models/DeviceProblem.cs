using System.ComponentModel.DataAnnotations.Schema;

namespace HitchFix.Models
{
    public class DeviceProblem : Problem
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
