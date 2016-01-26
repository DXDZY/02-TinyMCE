using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Reflection;
using System.Data;

namespace BLL
{
    public class JsonTools
    {
        // 从一个对象信息生成Json串
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        ////序列化
        ////对象转化为Json字符串
        //public static string Serialize(object objectToSerialize)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
        //        serializer.WriteObject(ms, objectToSerialize);
        //        ms.Position = 0;

        //        using (StreamReader reader = new StreamReader(ms))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }
        //}
        // 从一个Json串生成对象信息
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
        ////反序列化
        ////Json字符串转化为对象
        //public static T Deserialize<T>(string jsonString)
        //{
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //    {
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
        //        return (T)serializer.ReadObject(ms);
        //    }
        //}
        //字典转换为对象
        public static T PopulateEntityFromCollection<T>(T entity, Dictionary<string, string> collection) where T : new()
        {
            //初始化 如果为null
            if (entity == null)
            {
                entity = new T();
            }
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                //给属性赋值
                if (collection.Keys.Contains(item.Name))
                {
                    if (item.Name == "freezeMenu" && collection.Keys.Contains("freezeMenu"))
                    {
                        item.SetValue(entity, Convert.ChangeType(1, item.PropertyType), null);
                    }
                    else
                    {
                        item.SetValue(entity, Convert.ChangeType(collection[item.Name], item.PropertyType), null);
                    }
                }
            }
            return entity;
        }

        //序列化表单转字典
        public static Dictionary<string, string> jsonToDictionary(string json)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] formArray = json.Split('&');
            foreach (string s in formArray)
            {
                string[] keyValue = s.Split('=');
                dictionary[keyValue[0]] = keyValue[1];
            }
            return dictionary;
        }
    }
}
