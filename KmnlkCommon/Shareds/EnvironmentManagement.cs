using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace KmnlkCommon.Shareds
{
    public class EnvironmentManagement
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string getCurrentMethodName(Type type)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return type.FullName + ";" + sf.GetMethod().Name;
        }
        public static string getRootPath()
        {
            return HttpContext.Current.Server.MapPath("~");

        }

    }
}
