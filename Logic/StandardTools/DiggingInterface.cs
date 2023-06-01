using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StandardTools.Reflexions;
public class DiggingInterface
{
    private DiggingTypes _diggingTypes { get; init; }
    public DiggingInterface(DiggingTypes diggingTypes)
    {
        _diggingTypes = diggingTypes;
    }

    public PropertyInfo[] GetPropertyInfoList_Interface<T>(T obj)
    {
        PropertyInfo[] pList;
        TypeInfo t;
        Type declaringType;
        t = typeof(T).GetTypeInfo();
        declaringType = t.DeclaringType ?? t;
        pList = _diggingTypes.GetPropertyInfoList(declaringType, publicOnly: true);
        return pList;
    }
    public Dictionary<string, string> GetPropertyValues_Dictionnary<T>(T obj, Func<string, string>? fieldNameTransformator = null)
    {
        Dictionary<string, string> result;
        PropertyInfo[] allProperties;

        allProperties = this.GetPropertyInfoList_Interface<T>(obj);
        result = new Dictionary<string, string>();
        foreach (PropertyInfo prop in allProperties)
        {
            string property, value;
            property = prop.Name;
            value = (string)prop.GetValue(obj, null);
            result.Add(property, value);
        }

        return result;
    }
    public List<T> ParseToLists<T>(List<Dictionary<string, string>> dicos) where T : new()
    {
        List<T> output;
        output = _diggingTypes.ParseNoQuestionsAsked<T>(dicos, true);
        return output;
    }

}
