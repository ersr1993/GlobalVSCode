using System;
using System.Collections.Generic;
using System.Data;
using StandardTools.Reflexions;

namespace VsConsole.Logic;
public class ObjToString : IObjToString
{
    public string GetHello()
    {
        return "hello";
    }
    public static string ConvertRaw(DataTable dt)
    {
        string stringRow;
        stringRow = string.Empty;

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            stringRow += $"{dt.Columns[i].ColumnName.ToUpper()}";
            stringRow += ";";
        };
        stringRow += "\n";
        foreach (DataRow r in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                stringRow += $"{r[i].ToString()}";
                stringRow += ";";
            }
            stringRow += "\n";
        }

        return stringRow;
    }
    public static string Convert(DataTable dt)
    {
        string stringRow = "";
        List<List<string>> stringList;

        stringList = BuildStringList(dt);
        foreach (List<string> list in stringList)
        {
            string line;
            line = ToSingleLine(list);
            stringRow += $"{line}\n";
        }

        return stringRow;
    }
    private static List<List<string>> BuildStringList<T>(IEnumerable<T> list)
    {
        List<List<string>> stringListList;
        DiggingObject x;
        DiggingTypes xx;
        xx = new DiggingTypes();
        x = new DiggingObject(xx);
        stringListList = new List<List<string>>();
        foreach (var f in list)
        {
            Dictionary<string, string> dic;
            List<string> strings;
            strings = new List<string>();
            dic = x.GetPropertyValues_Dictionnary_string<T>(f);
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                strings.Add(kvp.Value);
            }
        }

        return stringListList;
    }
    private static List<List<string>> BuildStringList(DataTable dt)
    {
        List<List<string>> stringListList;
        List<string> currentRowCells;

        stringListList = new List<List<string>>();

        currentRowCells = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string cell;
            DataColumn col;
            col = dt.Columns[i];
            cell = $"{col.ColumnName.ToUpper()}";
            currentRowCells.Add(cell);
        };
        stringListList.Add(currentRowCells);

        foreach (DataRow r in dt.Rows)
        {
            currentRowCells = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string cellValue;
                cellValue = $"{r[i]}";
                currentRowCells.Add(cellValue);
            }
            stringListList.Add(currentRowCells);
        }

        return stringListList;
    }

    public static string ToSingleLine(IEnumerable<string> messages)
    {
        string fullLine;
        const int lgth = 15;
        const int margin = -(lgth + 3);

        fullLine = string.Empty;
        foreach (string message in messages)
        {
            string shortMsg;

            shortMsg = message.Substring(0, Math.Min(lgth, message.Length));
            if (shortMsg.Length == 0)
            {
                shortMsg += "¤";
            }
            else if (!(shortMsg.Length == message.Length))
            {
                shortMsg += "...";
            }

            fullLine += $"{shortMsg,margin}; ";
        }

        return fullLine;
    }
    public static string Convert<T>(IEnumerable<T> objects) where T : class
    {

        string outputString;
        ParamsStringBuilder paramsStringBuilder;
        DiggingTypes diggingTypes;
        DiggingObject diggingInterface;

        diggingTypes = new DiggingTypes();
        diggingInterface = new DiggingObject(diggingTypes);
        paramsStringBuilder = new ParamsStringBuilder(diggingInterface);

        outputString = string.Empty;
        foreach (T obj in objects)
        {
            //outputString += $"{diggingClass.BuildXml(obj)} \n";
            outputString += $"{paramsStringBuilder.GetFormatedString_PropertyEqualsValues<T>(obj)} \n";
        }

        return outputString;
    }
}
