using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T4TemplateCore
{
    [Table("Order")]
    public class Order : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set ; }
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
