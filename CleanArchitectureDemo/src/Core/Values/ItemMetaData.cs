using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Values
{
    public class ItemMetaData : IEqualityComparer<ItemMetaData>
    {
        public string Name { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;

        public bool Equals(ItemMetaData x, ItemMetaData y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            //return (x.Key == y.Key &&
            //        x.Name == y.Name &&
            //        x.Type == y.Type &&
            //        x.Value == y.Value);
            return (x.Name == y.Name);
        }

        public int GetHashCode(ItemMetaData obj)
        {
            throw new NotImplementedException();
        }
    }
}
