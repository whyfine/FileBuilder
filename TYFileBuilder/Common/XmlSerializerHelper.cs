using TYFileBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Xml.Serialization
{
    internal static class XmlSerializerHelper
    {
        internal static T DeserializerFromFile<T>(this XmlSerializer xmlSerializer, String filePath) where T : class, new()
        {
            T t = default(T);
            TextReader reader = null;
            try
            {
                using (reader = new StreamReader(filePath))
                {
                    t = xmlSerializer.Deserialize(reader) as T;
                }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return t;
        }

        internal static T Deserializer<T>(this XmlSerializer xmlSerializer, string xml) where T : class, new()
        {
            T t = default(T);
            TextReader reader = null;
            try
            {
                using (reader = new StringReader(xml))
                {
                    t = xmlSerializer.Deserialize(reader) as T;
                }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return t;
        }

        internal static void SerializerToFile(this XmlSerializer xmlSerializer, object o, string filePath)
        {
            TextWriter writer = null;
            try
            {
                using (writer = new StreamWriter(filePath))
                {
                    xmlSerializer.Serialize(writer, o);
                }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        internal static string Serializer(this XmlSerializer xmlSerializer, object o, string filePath)
        {
            var sb = new StringBuilder();
            TextWriter writer = null;
            try
            {
                using (writer = new StringWriter(sb))
                {
                    xmlSerializer.Serialize(writer, o);
                }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return sb.ToString();
        }
    }
}
