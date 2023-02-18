//using System;
//using System.Collections.Generic;
//using System.Data;
//using _rfx = System.Reflection;

//namespace VsConsole.Logic
//{
//    public class DiggingClass
//    {
//        public DiggingFormatParameter format { get; private set; }
//        public DiggingClass()
//        {
//            this.format = new DiggingFormatParameter()
//            {
//                equalsSign = "=",
//                separationSymbol = "&",
//                includeNullValues = true,
//                includeNonPublicProperties = false
//            }; // same as default values
//        }
//        public DiggingClass(DiggingFormatParameter format)
//        {
//            this.format = format;
//        }

//        public Dictionary<string, string> GetPropertyValues_Dictionnary(object obj)
//        {
//            Dictionary<string, string> result;
//            _rfx.PropertyInfo[] allProperties;

//            allProperties = this.GetPropertyInfoList(obj);
//            result = new Dictionary<string, string>();
//            foreach (_rfx.PropertyInfo field in allProperties)
//            {
//                string property, value;

//                property = field.Name;
//                value = field.GetValue(obj, null).ToString();

//                if (this.format.includeNullValues || value != null)
//                {
//                    result.Add(property, value); // By default, doesn't return null values
//                }
//            }

//            return result;
//        }
//        public List<string> GetValues_List(object obj)
//        {
//            List<string> result;
//            _rfx.PropertyInfo[] allProperties;

//            allProperties = this.GetPropertyInfoList(obj);
//            result = new List<string>();
//            foreach (_rfx.PropertyInfo field in allProperties)
//            {
//                string value;

//                value = (string)field.GetValue(obj, null).ToString();

//                if (this.format.includeNullValues || value != null)
//                {
//                    result.Add(value); // By default, doesn't return null values
//                }
//            }

//            return result;
//        }
//        public List<string> GetPropertyNames_List(object obj)
//        {
//            List<string> result;
//            _rfx.PropertyInfo[] allProperties;

//            allProperties = this.GetPropertyInfoList(obj);
//            result = new List<string>();
//            foreach (_rfx.PropertyInfo field in allProperties)
//            {
//                string property, value;

//                property = field.Name;
//                value = (string)field.GetValue(obj, null).ToString();

//                if (this.format.includeNullValues || value != null)
//                {
//                    result.Add(property); // By default, doesn't return null values
//                }
//            }

//            return result;
//        }

//        public string GetFormatedString_PropertyEqualsValues(object obj)
//        {
//            Dictionary<string, string> allFields;

//            allFields = GetPropertyValues_Dictionnary(obj);

//            return FormatDictionnaryToString(allFields, this.format.equalsSign, this.format.separationSymbol);
//        }
//        private string FormatDictionnaryToString(Dictionary<string, string> propertyValueDictionnary, string equalsSign, string separatorSimbol)
//        {
//            string result;

//            result = string.Empty;

//            foreach (KeyValuePair<string, string> a in propertyValueDictionnary)
//            {
//                result += ($"{a.Key}{equalsSign}{a.Value}{separatorSimbol}");
//            }

//            if (result.Length > 0)
//            {
//                result = result.Substring(0, result.Length - 1);
//            }

//            return result;
//        }
//        private _rfx.PropertyInfo[] GetPropertyInfoList(object obj)
//        {
//            Type paramType;
//            _rfx.PropertyInfo[] infoList;

//            paramType = obj.GetType();

//            if (!this.format.includeNonPublicProperties)
//            {
//                infoList = paramType.GetProperties(_rfx.BindingFlags.Instance
//                                               | _rfx.BindingFlags.Public
//                                               ); // default case...
//            }
//            else
//            {
//                infoList = paramType.GetProperties(_rfx.BindingFlags.Instance
//                                               | _rfx.BindingFlags.Public
//                                               | _rfx.BindingFlags.NonPublic
//                                               );
//            }
//            return infoList;
//        }
//    }
//}
