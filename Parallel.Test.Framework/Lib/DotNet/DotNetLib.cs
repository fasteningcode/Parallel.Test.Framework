using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parallel.Test.Framework.Lib.DotNet {
    public static class DotNetLib
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

       
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;
            if (enumerable is ICollection<T> objs)
                return objs.Count < 1;
            if (typeof(T) == typeof(char))
            {
                if ((enumerable.ToString().ToLower() == "null"))
                    return true;
            }
            return !enumerable.Any();
        }
    }
}
