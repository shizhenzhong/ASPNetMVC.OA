using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyWebApp.OA.Common
{
    public class SerializerHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializerToString(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializer ser = JsonSerializer.Create(settings);
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw,obj );

                return sw.ToString();
                
            }
            
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeSerializerToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }

   
}