using JARS.Core.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace JARS.Core.Extensions
{
    public static class PluginWithStateExtensions
    {
        //https://stackoverflow.com/questions/3722192/how-do-i-use-gzipstream-with-system-io-memorystream

        /// <summary>
        /// This method takes a dictionary object and serializes it into a binary array.
        /// </summary>
        /// <param name="plugin">the current plugin</param>
        /// <param name="stateInfo">The Dictionary object of string and object</param>
        /// <returns>the dictionary object serialized into byte array</returns>
        public static byte[] SerializeAndCompressStateInformation(this IPluginWithStateInfo plugin, Dictionary<string, object> stateInfo)
        {
            byte[] retArr = new byte[] { byte.MinValue };
            try
            {
                using (MemoryStream msCompressed = new MemoryStream())//what gzip writes to
                {
                    using (GZipStream gZipStream = new GZipStream(msCompressed, CompressionMode.Compress))//setting up gzip
                    using (MemoryStream msToCompress = new MemoryStream())//what the settings will serialize to
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        //serialize the info into bytes
                        formatter.Serialize(msToCompress, stateInfo);
                        //reset to 0 to read from beginning byte[0] fix.
                        msToCompress.Position = 0;
                        //this then does the compression
                        msToCompress.CopyTo(gZipStream);
                    }
                    //the compressed data as an array of bytes
                    retArr = msCompressed.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
            return retArr;
        }

        /// <summary>
        /// This method takes a byte array and de-serializes it into a dictionary of string and object.
        /// </summary>
        /// <param name="plugin">the current plugin</param>
        /// <param name="stateInfo">the byte array that will be converted to a dictionary</param>
        /// <returns></returns>
        public static Dictionary<string, object> DeserializeAndDecompressStateInformation(this IPluginWithStateInfo plugin, byte[] stateInfo)
        {
            Dictionary<string, object> settings = new Dictionary<string, object>();
            try
            {
               
                using (MemoryStream msDecompressed = new MemoryStream()) //the stream that will hold the decompressed data
                {
                    using (MemoryStream msCompressed = new MemoryStream(stateInfo))//the compressed data
                    using (GZipStream gzDecomp = new GZipStream(msCompressed, CompressionMode.Decompress))//the gzip that will decompress
                    {
                        msCompressed.Position = 0;//fix for byte[0]
                        gzDecomp.CopyTo(msDecompressed);//decompress the data
                    }
                    BinaryFormatter formatter = new BinaryFormatter();
                    //prevents 'End of stream encountered' error
                    msDecompressed.Position = 0;
                    //change the decompressed data to the object
                    settings = formatter.Deserialize(msDecompressed) as Dictionary<string, object>;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
            return settings;
        }

    }
}
