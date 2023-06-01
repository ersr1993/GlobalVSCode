using StandardTools;
using StandardTools.Reflexions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanadTools
{
    internal class DataTableUtilities
    {
        internal DiggingClass _diggingClass { get; private set; }
        internal DiggingInterface _diggingInterface { get; private set; }
        public DataTableUtilities(DiggingFormatParameter diggingFormatParameter = null)
        {
            diggingFormatParameter = diggingFormatParameter ?? new DiggingFormatParameter();
            _diggingClass = new DiggingClass(diggingFormatParameter);
        }

        public DataTable ToDataTable<T>(List<T> myObjList)
        {
            DataTable dt;
            bool isEmptyList;

            isEmptyList = myObjList.Count <= 0;
            dt = new DataTable();
            if (!isEmptyList)
            {
                PopulateDataTable<T>(ref dt, myObjList);
            }
            return dt;
        }
        private void PopulateDataTable<T>(ref DataTable dt, List<T> myObjList)
        {
            //DiggingClass diggingClass;
            List<string> titles;


            //diggingClass = BuildDiggingClass();
            //titles = _diggingClass.GetPropertyNames_List(myObjList[0]);
            titles = _diggingInterface.GetPropertyValues_Dictionnary(myObjList[0]).Keys.ToList();

            foreach (string prop in titles)
            {
                dt.Columns.Add(prop);
            }
            foreach (T obj in myObjList)
            {
                AddRowForObject<T>(ref dt, obj);
            }
        }
        //private DiggingClass BuildDiggingClass()
        //{
        //    DiggingFormatParameter format;
        //    DiggingClass diggingClass;
        //    format = new DiggingFormatParameter()
        //    {
        //        includeNullValues = false,         
        //        includeNonPublicProperties = false  
        //    };
        //    diggingClass = new DiggingClass(format)
        //    {
        //    };

        //    return diggingClass;
        //}
        private void AddRowForObject<T>(ref DataTable dt, T obj)
        {
            DataRow row;
            List<string> valueList;
            //DiggingClass diggingClass;

            //diggingClass = BuildDiggingClass();
            row = dt.NewRow();
            valueList = _diggingClass.GetValues_List(obj);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row[i] = valueList[i];
            }
            dt.Rows.Add(row);

        }
    }
}
