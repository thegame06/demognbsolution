using System;
using System.Data.Linq;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace GNB.Infrastructure.Common
{
    public class Serializer
    {

        public static byte[] BinarySerialize(object serializableObject)
        {
            BreakIfArgIsNull(serializableObject);

            byte[] result;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, serializableObject);
                result = memoryStream.ToArray();
            }

            return result;
        }

        private static void BreakIfArgIsNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

        public static string SerializeBase64(object o)
        {
            // Serialize to a base 64 string
            byte[] bytes;
            long length = 0;
            MemoryStream ws = new MemoryStream();
            BinaryFormatter sf = new BinaryFormatter();
            sf.Serialize(ws, o);
            length = ws.Length;
            bytes = ws.GetBuffer();
            string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return encodedData;
        }

        public static object DeserializeBase64(string s)
        {
            // We need to know the exact length of the string - Base64 can sometimes pad us by a byte or two
            int p = s.IndexOf(':');
            int length = Convert.ToInt32(s.Substring(0, p));

            // Extract data from the base 64 string!
            byte[] memorydata = Convert.FromBase64String(s.Substring(p + 1));
            MemoryStream rs = new MemoryStream(memorydata, 0, length);
            BinaryFormatter sf = new BinaryFormatter();
            object o = sf.Deserialize(rs);
            return o;
        }

        public static T SafeDeserialize<T>(Binary serializedObject, bool useCompression = false) where T : class
        {
            T result = null;

            if (serializedObject != null && serializedObject.Length > 0)
            {
                result = BinaryDeserialize(serializedObject.ToArray(), useCompression) as T;
            }

            return result;
        }

        public static T SafeDeserialize<T>(string serializedObjectString, bool useCompression = false) where T : class
        {
            T result = null;

            if (!string.IsNullOrEmpty(serializedObjectString))
            {
                //Remove 0x if contained
                if (serializedObjectString.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
                {
                    serializedObjectString = serializedObjectString.Substring(2);
                }
                var bytes = StringToByteArray(serializedObjectString);

                //Get the value
                if (bytes.Length > 0)
                {
                    result = BinaryDeserialize(bytes, useCompression) as T;
                }
            }

            return result;
        }

        public static object BinaryDeserialize(byte[] serializedObject, bool useCompression = false)
        {
            if (serializedObject == null)
            {
                throw new ArgumentNullException("serializedObject");
            }

            object result;

            using (MemoryStream memoryStream = new MemoryStream(serializedObject))
            {
                if (useCompression)
                {
                    using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        result = binaryFormatter.Deserialize(gZipStream);
                    }
                }
                else
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    result = binaryFormatter.Deserialize(memoryStream);
                }
            }

            return result;
        }

        public static byte[] StringToByteArray(string binaryString)
        {
            int numberChars = binaryString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(binaryString.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
