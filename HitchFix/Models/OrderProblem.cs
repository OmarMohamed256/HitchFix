using System.ComponentModel.DataAnnotations.Schema;

namespace HitchFix.Models
{
    public class OrderProblem : Problem
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
