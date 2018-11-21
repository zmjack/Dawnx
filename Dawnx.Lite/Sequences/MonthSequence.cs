using System.Linq;

namespace Dawnx.Sequences
{
    public static class MonthSequence
    {
        public static string GetName(int month)
        {
            switch (month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return null;
            }
        }

        public static string GetShortName(int month)
        {
            switch (month)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return null;
            }
        }

        public static int GetMonth(string name)
        {
            switch (name.ToLower())
            {
                case string s when new[] { "january", "jan" }.Contains(s): return 1;
                case string s when new[] { "february", "feb" }.Contains(s): return 2;
                case string s when new[] { "march", "mar" }.Contains(s): return 3;
                case string s when new[] { "april", "apr" }.Contains(s): return 4;
                case string s when new[] { "may", "may" }.Contains(s): return 5;
                case string s when new[] { "june", "jun" }.Contains(s): return 6;
                case string s when new[] { "july", "jul" }.Contains(s): return 7;
                case string s when new[] { "august", "aug" }.Contains(s): return 8;
                case string s when new[] { "september", "sep" }.Contains(s): return 9;
                case string s when new[] { "october", "oct" }.Contains(s): return 10;
                case string s when new[] { "november", "nov" }.Contains(s): return 11;
                case string s when new[] { "december", "dec" }.Contains(s): return 12;
                default: return 0;
            }
        }

    }
}
