using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.Values
{
    public class CreditCard
    {
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int ExpireYear { get; set; } 
        public int ExpireMonth { get; set; }
        public string CVSNumber { get; set; } = string.Empty;
    }
}
