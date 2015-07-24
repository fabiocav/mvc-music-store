using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MappedIdAttribute : Attribute
    {
    }
}
