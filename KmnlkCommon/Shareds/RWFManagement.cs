using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KmnlkCommon.Shareds
{
    public static class RWFManagement
    {
        public interface IXMLFileObject
        {
            object Read(object key);
            Dictionary<object, object> ReadAll();
            Dictionary<object, object> ReadAll(List<object> keys);
            void WriteAll(string root,Dictionary<object, object> contents);
        }
        public interface IJSONFileObject
        {
            //object Read(object key);
            //object Read();
            //List<object> ReadAll();
            //List<object> ReadAll(List<object> keys);
            //void Write(object content);
            //void WriteAll(List<object> contents);
            //void Write(object key, object content);
            //void WriteAll(List<object> keys, List<object> contents);
        }
        public class XMLFileObject : IXMLFileObject
        {
            private string path="";

            public XMLFileObject(string path)
            {
                this.path = path;
                
            }

            public object Read(object key)
            {
                XmlTextReader reader = new XmlTextReader(path);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                XmlElement root = doc.DocumentElement;
             
                var obj= "";
                foreach (XmlNode item in root.ChildNodes)
                {
                    if (item.Name == key.ToString())
                        obj= item.InnerText;
                }

                reader.Close();
                return obj;
            }

            public Dictionary<object,object> ReadAll()
            {
                Dictionary<object, object> objs = new Dictionary<object, object>();
                XmlTextReader reader = new XmlTextReader(path);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                XmlElement root = doc.DocumentElement;
                foreach(XmlNode item in root.ChildNodes)
                {
                    objs.Add(item.Name, item.InnerText);
                }
                
                reader.Close();
                return objs;
            }
            public Dictionary<object, object> ReadAll(List<object> keys)
            {
                Dictionary<object, object> objs = new Dictionary<object, object>();
                XmlTextReader reader = new XmlTextReader(path);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                XmlElement root = doc.DocumentElement;

                foreach (XmlNode item in root.ChildNodes)
                {
                    foreach (var key in keys)
                    {
                        if (item.Name == key.ToString())
                            objs.Add(item.Name,item.InnerText);
                    }
                 
                }
                reader.Close();
                return objs;
            }

         
            public void WriteAll(string root,Dictionary<object, object> contents)
            {
                XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8);
                XmlDocument doc = new XmlDocument();
                String PItext = "type=\"text/xsl\" href=\"book.xsl\"";
                writer.WriteProcessingInstruction("xml-stylesheet", PItext);
                writer.WriteStartDocument();
                writer.WriteStartElement(root);
                foreach (var item in contents)
                {
                    writer.WriteStartElement(item.Key.ToString());
                    writer.WriteString(item.Value.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
         
        }

    }
}
