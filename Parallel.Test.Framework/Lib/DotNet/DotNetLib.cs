﻿using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Parallel.Test.Framework.Lib.DotNet {
    public class DotNetLib
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
