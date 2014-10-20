using System;
using System.Text;

namespace EdiQuery.Extensions
{
    public static class StringExtensions
    {

        public static string EDIDateFromDate(this DateTime dte, bool century)
        {
            if (century)
                return dte.ToString("yyyyMMdd");
            return dte.ToString("yyMMdd");
        }

        public static DateTime DateFromEDIDate(this string ediDate)
        {
            int num;
            if (!int.TryParse(ediDate, out num))
                throw new ApplicationException("Invalid date format: " + ediDate);
            if(ediDate.Length == 8)
                return new DateTime(ediDate.Substring(0,4).CastToInt(),
                    ediDate.Substring(4,2).CastToInt(),
                    ediDate.Substring(6,2).CastToInt());
            if(ediDate.Length == 6)
                return new DateTime(2000 + ediDate.Substring(0,2).CastToInt(),
                    ediDate.Substring(2,2).CastToInt(), 
                    ediDate.Substring(4,2).CastToInt());
            throw new ApplicationException("Invalid date format: " + ediDate);
        }

        public static string SafeReplace(this string str, string searchStr, string replaceStr)
        {
            if (searchStr == "") return str;
            return str.Replace(searchStr, replaceStr);
        }

        public static string EncloseInTag(this string str, string el_tag)
        {
            try
            {
                string closeTag = el_tag.ExtractClosingTag();
                el_tag = el_tag.Replace("/>", ">").Replace("/ >", ">");
                return el_tag + str + closeTag;
            }
            catch (Exception ex)
            {
                throw new Exception("AFPSTStringExtensions error: " + ex.Message);
            }
        }

        public static string FromBase64(this string encoded)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(encoded));
        }

        public static string ToBase64(this string val)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(val));
        }

        public static string TitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }

        public static byte[] ToByteArray(this string str)
        {
            if (string.IsNullOrEmpty(str)) return new byte[] {0};
            return new  ASCIIEncoding().GetBytes(str);
        }

        public static double CastToDouble(this string Amt)
        {
            Amt = initialize_numeric_string(Amt);
            double d;
            double.TryParse(Amt, out d);
            return d;
        }

        public static decimal CastToDecimal(this string Amt)
        {
            Amt = initialize_numeric_string(Amt);
            decimal d;
            decimal.TryParse(Amt, out d);
            return d;
        }


        public static int CastToInt(this string amt)
        {
            amt = initialize_numeric_string(amt);
            double d;
            double.TryParse(amt, out d);
            return (int)d;
        }

        public static DateTime CastToDateTime(this string dte)
        {
            DateTime dt;
            DateTime.TryParse(dte, out dt);
            return dt; 

        }

        public static bool CastToBool(this string condition)
        {
            bool b;
            bool.TryParse(condition, out b);
            return b; 
        }

        private static string initialize_numeric_string(string amt)
        {
            if (amt == null) return "0";
            amt = amt.Trim();
            if(amt == "")
                amt = "0";
            return amt;
        }
        public static string ExtractClosingTag(this string str)
        {
            if (str.IndexOf("<") != 0 || str.IndexOf(">") < 0)
                throw new Exception("Poorly formed XML tag.");
            string temp = str.Replace("<", "");
            temp = temp.Trim();
            string [] arr = temp.Split(" ".ToCharArray());
            return "</" + arr[0].Replace("/", "").Replace(">", "") + ">"; 
        } 

        public static string TruncateTo(this string str, int len)
        {
            return str.Length <= len ? str : str.Substring(0, len);
        }

        public static string SafeTrim(this string str)
        {
            if (str == null) return "";
            return str.Trim();
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}