using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T4TemplateCore
{
    [Table("OrderItem")]
    public class OrderItem : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Catalog")]
        public int CatalogId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public virtual Catalog Catalog { get; set; } = new Catalog();
        public virtual Order Order { get; set; } = new Order();

    }
}
