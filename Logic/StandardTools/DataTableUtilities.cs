//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VsConsole.Logic
//{
//    public class DataTableUtilities
//    {
//        public DiggingClass _diggingClass { get; private set; }
//        public DataTableUtilities(DiggingFormatParameter diggingFormatParameter=null)
//        {
//            diggingFormatParameter = diggingFormatParameter ?? new DiggingFormatParameter();
//            _diggingClass = new DiggingClass(diggingFormatParameter);
//        }

//        public DataTable ToDataTable<T>(List<T> myObjList)
//        {
//            DataTable dt;
//            bool isEmptyList;

//            isEmptyList = myObjList.Count <= 0;
//            dt = new DataTable();
//            if (!isEmptyList)
//            {
//                PopulateDataTable<T>(ref dt, myObjList);
//            }
//            return dt;
//        }
//        private void PopulateDataTable<T>(ref DataTable dt, List<T> myObjList)
//        {
//            //DiggingClass diggingClass;

//            //diggingClass = new DiggingClass()
//            //{
//            //    format = new DiggingFormatParameter()
//            //    {
//            //        includeNullValues = true,           // ** false by default **
//            //        includeNonPublicProperties = false  // as by default
//            //    }
//            //};

//            List<string> propNames;

//            propNames = _diggingClass.GetPropertyNames_List(myObjList[0]);

//            foreach (string prop in propNames)
//            {
//                dt.Columns.Add(prop);
//            }
//            foreach (T obj in myObjList)
//            {
//                AddRowForObject<T>(ref dt, obj);
//            }
//        }
//        private void AddRowForObject<T>(ref DataTable dt, T obj)
//        {
//            DataRow row;
//            List<string> valueList;

//            row = dt.NewRow();
//            valueList = _diggingClass.GetValues_List(obj);
//            for (int i = 0; i < dt.Columns.Count; i++)
//            {
//                row[i] = valueList[i];
//            }
//            dt.Rows.Add(row);

//        }
//    }
//}
