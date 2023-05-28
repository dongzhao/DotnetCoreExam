using Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderId { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.MinValue.Date;
        [Display(Name = "Order By")]
        [CaseSensitive]
        public string OrderBy { get; set; } = string.Empty;
    }
}
