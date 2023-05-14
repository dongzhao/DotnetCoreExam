using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TemplateCore
{
    public interface IEntity<ID>
    {
        ID Id { get; set; }
    }
}
