using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCommon.Shareds
{
    public class JsonManagement
    {
        public static string toJson(object ob)
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(ob);
                return json;
            }catch(Exception e)
            {
                throw;
            }
        }
        public static T toObject<T>(string json)
        {
            try
            {
                T ob = (T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                return ob;
            }catch(Exception e)
            {
                throw;
            }
        }
    }
}
