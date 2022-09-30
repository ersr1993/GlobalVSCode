using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyConsMenu
{
    public static class MsgOut
    {
        public static string DottedLines()
        {
            return "\n -------------- \n";
        }
        public static string FooterMsg()
        {
            string footerMsg;
            footerMsg = "\n Enter  ...\n " +
                "b : Back to previous menu \n " +
                "c : Clear Console \n " +
                "q : QUIT \n";

            return footerMsg;
        }
    }
}
