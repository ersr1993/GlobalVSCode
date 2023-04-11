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
            string stringRow = "";
            List<List<string>> stringList;

            stringList = BuildStringList(dt);
            foreach(List<string> list in stringList)
            {
                string line;
                line = ToSingleLine(list);
                stringRow += $"{line}\n";
            }

            return stringRow;
        }
        private static List<List<string>> BuildStringList(DataTable dt)
        {
            List<List<string>> stringDT;
            List<string> row;

            stringDT = new List<List<string>>();

            row = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string cell;
                string columnTitle;
                columnTitle = dt.Columns[i].ColumnName;
                cell = $"{columnTitle.ToUpper()}";
                row.Add(cell);
            };
            stringDT.Add(row);

            foreach (DataRow r in dt.Rows)
            {
                row = new List<string>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string cell;
                    cell = $"{r[i].ToString()}";
                    row.Add(cell);
                }
                stringDT.Add(row);
            }

            return stringDT;
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
