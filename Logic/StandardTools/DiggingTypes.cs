//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;

//namespace StandardTools.Reflexions;
//public class DiggingTypes
//{
//    public PropertyInfo[] GetPropertyInfoList(Type objType, bool publicOnly)
//    {
//        PropertyInfo[] infoList;

//        if (publicOnly)
//        {
//            infoList = objType.GetProperties(BindingFlags.Instance
//                                           | BindingFlags.Public
//                                           ); // default case...
//        }
//        else
//        {
//            infoList = objType.GetProperties(BindingFlags.Instance
//                                           | BindingFlags.Public
//                                           | BindingFlags.NonPublic
//                                           );
//        }
//        return infoList;

//    }

//    public List<T> ParseNoQuestionsAsked<T>(List<Dictionary<string, string>> objValues, bool publicOnly)
//            where T : new()
//    {
//        List<T> result;
//        result = new List<T>();
//        foreach (Dictionary<string, string> row in objValues)
//        {
//            T singleObj;
//            singleObj = ParseNoQuestionsAsked<T>(row, publicOnly);
//            result.Add(singleObj);
//        }
//        return result;
//    }
//    private T ParseNoQuestionsAsked<T>(Dictionary<string, string> objValues, bool publicOnly)
//            where T : new()
//    {
//        PropertyInfo[] infoList;
//        infoList = GetPropertyInfoList(typeof(T), publicOnly);
//        T obj;
//        int i = 0;

//        obj = new T();
//        foreach (PropertyInfo field in infoList)
//        {
//            string actualValue;
//            bool foundValue;
//            foundValue = objValues.TryGetValue(field.Name, out actualValue);
//            if (foundValue)
//            {
//                TypeCode propertyType;
//                propertyType = Type.GetTypeCode(field.PropertyType);
//                switch (propertyType)
//                {
//                    case TypeCode.Int32:
//                    case TypeCode.Int64:
//                    case TypeCode.UInt32:
//                    case TypeCode.UInt64:
//                        field.SetValue(obj, int.Parse(actualValue));
//                        break;
//                    case TypeCode.String:
//                        field.SetValue(obj, actualValue);
//                        break;
//                    case TypeCode.DateTime:
//                        field.SetValue(obj, DateTime.Parse(actualValue));
//                        break;
//                    case TypeCode.Boolean:
//                        field.SetValue(obj, bool.Parse(actualValue));
//                        break;
//                    default:
//                        string msg;
//                        Type unknown;
//                        unknown = field.GetValue(obj, null) as Type;
//                        msg = $"Type {propertyType} inconnu";
//                        throw new NotImplementedException(msg);
//                        break;
//                }
//            }
//        }

//        return obj;
//    }
//}
