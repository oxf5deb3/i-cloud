using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.ESIApi.Utils
{
    public interface IMemoryCache
    {
        object AddOrGet(string key, object value);

        object AddOrGet(string key, object value, TimeSpan? slideExpiredTime);

        void Remove(string key);
    }
}
