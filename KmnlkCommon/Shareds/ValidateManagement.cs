using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCommon.Shareds
{
    public class ValidateManagement
    {
        public static bool validateGuid(string uid)
        {
            try
            {
                Guid guid = Guid.Parse(uid);
                return true;

            }catch (Exception e)
            {
                return false;
            }
        }
    }
}
