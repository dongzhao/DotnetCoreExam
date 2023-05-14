using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Catalog")]
    public class Catalog : BaseEntity<int>
    {
        [StringLength(100)]
        [Column("CatalogName")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(7,2)")]
        public decimal? Price { get; set; } = decimal.Zero;

    }
}
