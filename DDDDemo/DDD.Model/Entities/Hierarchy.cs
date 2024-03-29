﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Model.Entities
{
    [Table("Hierarchy")]
    public class Hierarchy : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("Children")]
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int? HierarchyType { get; set; } = 0;
        public string HierarchyItem { get; set; } = String.Empty;
        public string LinkHierarchyItem { get; set; } = String.Empty;
        public DateTime CreatedDateTime { get; set; } = DateTime.MinValue;
        public string CreatedBy { get; set; } = String.Empty;
        public virtual Hierarchy Parent { get; set; }
        public virtual List<Hierarchy> Children { get; set; } = new();
    }

    public class HierarchyItem : IEqualityComparer<HierarchyItem>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string DataType { get; set; }
        public string ItemValue { get; set; }

        public bool Equals(HierarchyItem x, HierarchyItem y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            return (x.Id == y.Id &&
                    x.ItemName == y.ItemName &&
                    x.DataType == y.DataType &&
                    x.ItemValue == y.ItemValue);
            //throw new NotImplementedException();
        }

        public int GetHashCode(HierarchyItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
