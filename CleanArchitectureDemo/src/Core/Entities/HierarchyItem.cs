using Core.Enums;
using Core.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("HierarchyItem")]
    public class HierarchyItem : BaseEntity<int>
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public HierarchyType ItemType { get; set; } = HierarchyType.Start;
        public ItemMetaData Metadata { get; set; } = new();
        public List<ItemMetaData> MetadataList { get; set; } = new List<ItemMetaData>();
        //public string LinkItem { get; set; } = String.Empty;
        public virtual HierarchyItem Parent { get; set; }
        public virtual List<HierarchyItem> Children { get; set; } = new();
    }
}
