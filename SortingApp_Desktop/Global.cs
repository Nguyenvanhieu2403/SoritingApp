using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop
{
    public static class Global
    {
        public static int ProcessId = 1;
        public static int DividingStage = 1;
        public static float TotalWeight = 0;
        public static int CountItem = 0;
        public static int ConfigId;
        public static string BagNumber = null;
        public static string MailNumber = null;
        public static string OriginalPost = null;
        public static string DestinationPosCode = null;
        public static int ProcessIdReportGeneral = 1;
        public static DateTime FromDateReportGeneral;
        public static DateTime ToDateReportGeneral;
        public static string BagNumberReport = null;
        public static string MailNumberReport = null;
        public static string ServiceCodeReport = null;

    }
}
