using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsConsole.Logic
{
    public class ObjToString
    {

        public static string Convert(DataTable dt)
        {
            string stringRow;
            const int colWidth = 15;

            stringRow = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                stringRow += $"{dt.Columns[i].ColumnName.ToUpper(),colWidth}";
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
    }
}
