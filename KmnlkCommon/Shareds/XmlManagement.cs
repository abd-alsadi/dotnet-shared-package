using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KmnlkCommon.Shareds
{
   public class XmlManagement
    {
        public static string toXml(object obj)
        {
            try
            {
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(stringwriter, obj);
                    return stringwriter.ToString();
                }
            }catch(Exception e)
            {
                throw;
            }
        }

        public static T toObject<T>(string xml)
        {
            try
            {
                using (var stringReader = new System.IO.StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }catch(Exception e)
            {
                throw;
            }
        }
    }
}
