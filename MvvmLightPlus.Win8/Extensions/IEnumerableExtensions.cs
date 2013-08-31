using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.MvvmLightPlus.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var l in list)
            {
                action(l);
            }
        }
    }
}
