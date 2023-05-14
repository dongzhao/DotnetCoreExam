using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T4TemplateCore
{
    [Table("Catalog")]
    public class Catalog : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(7,2)")]
        public decimal? Price { get; set; } = decimal.Zero;

    }
}
