using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsConsole.Logic
{
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
            string stringRow;
            const int colWidth = 15;

            stringRow = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                stringRow += $"{dt.Columns[i].ColumnName.ToUpper(),colWidth}";
                stringRow += " ;  ";
            };
            stringRow += "\n";
            foreach (DataRow r in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    stringRow += $"{r[i].ToString(),colWidth}";
                    stringRow += " ;  ";
                }
                stringRow += "\n";
            }

            return stringRow;
        }
        public static string Convert<T>(IEnumerable<T> objects) where T : class
        {

            StandardTools.DiggingClass diggingClass;
            string outputString;

            diggingClass = new StandardTools.DiggingClass();
            outputString = string.Empty;
            foreach (object obj in objects)
            {
                outputString += $"{diggingClass.GetFormatedString_PropertyEqualsValues(obj)} \n";
            }

            return outputString;
        }
    }
}
