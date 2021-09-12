using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.dotNet.System
{
    public static class StringExtensions
    {
        public static string AddFirst(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toAdd) == false)
                returnValue = (toAdd + item);

            return returnValue;
        }

        public static string AddLast(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toAdd) == false)
                returnValue = (item + toAdd);

            return returnValue;
        }

        public static bool IsEmail(this string item, bool emptyStringOK = true)
        {
            var returnValue = false;

            item = item.Trim();
            if ((emptyStringOK == true & item.Length == 0))
                returnValue = true;
            else
            {
                if ((item.Contains("@") & item.Contains("."))
                        && (item.IndexOf(".", item.IndexOf("@")) > item.IndexOf("@"))
                        && (item.Contains(" ") == false)
                        && (item.SubstringSafe(item.IndexOf("@") + 1).Contains("@") == false))
                    returnValue = true;
            }

            return returnValue;
        }

        public static bool IsFirst(this string item, string firstCharacters)
        {
            var returnValue = false;

            if (item.Length >= firstCharacters.Length)
                if (item.SubstringSafe(0, firstCharacters.Length) == firstCharacters)
                    returnValue = true;
            
            return returnValue;
        }

        public static bool IsInteger(this string item)
        {
            var returnValue = false;

            if (item.ToInt64() != -1)
                returnValue = true;

            return returnValue;
        }

        public static bool IsLast(this string item, string lastCharacters)
        {
            var returnValue = false;

            if (item.Length >= lastCharacters.Length)
                if (item.SubstringRight(lastCharacters.Length) == lastCharacters)
                    returnValue = true;

            return returnValue;
        }

        public static string RemoveFirst(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toRemove))
                returnValue = item.SubstringRight(item.Length - toRemove.Length);

            return returnValue;
        }

        public static string RemoveLast(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toRemove))
                returnValue = item.SubstringLeft(item.Length - toRemove.Length);

            return returnValue;
        }

        public static bool ToBoolean(this string item)
        {
            var returnValue = default(bool);
            if (String.IsNullOrEmpty(item) == false)
            {
                bool convertValue;
                if (item.ToInt16() != -1) // Catch integers, as To only evaluates "true" and "false", not "0".
                    returnValue = item.ToInt16() == 0 ? false : true;
                else if (Boolean.TryParse(item, out convertValue))
                    returnValue = convertValue;
            }

            return returnValue;
        }

        public static short ToInt16(this string item)
        {
            var returnValue = default(short);

            if (String.IsNullOrEmpty(item) == false)
                if (Int16.TryParse(item, out short convertValue))
                    returnValue = convertValue;

            return returnValue;
        }

        public static int ToInt32(this string item)
        {
            var returnValue = default(int);
            if (String.IsNullOrEmpty(item) == false)
                if (int.TryParse(item, out int convertValue))
                    returnValue = convertValue;

            return returnValue;
        }

        public static long ToInt64(this string item)
        {
            var returnValue = default(long);
            if (String.IsNullOrEmpty(item) == false)
                if (Int64.TryParse(item, out long convertValue))
                    returnValue = convertValue;

            return returnValue;
        }

        public static Guid ToGuid(this string item)
        { 
            var returnValue = default(Guid);

            if (String.IsNullOrEmpty(item) == false)
                if(!Guid.TryParse(item, out returnValue))
                    returnValue = default(Guid);

            return returnValue;
        }

        public static decimal ToDecimal(this string item)
        {
            var returnValue = default(decimal);
            if (String.IsNullOrEmpty(item) == false)
                if (Decimal.TryParse(item, out decimal convertValue))
                    returnValue = convertValue;

            return returnValue;
        }

        public static double ToDouble(this string item)
        {
            var returnValue = default(double);
            if (String.IsNullOrEmpty(item) == false)
            {
                double convertValue;
                if (Double.TryParse(item, out convertValue))
                    returnValue = convertValue;
            }

            return returnValue;
        }

        public static TEnum ToEnum<TEnum>(this string item, TEnum notFoundValue)
        {
            var returnValue = default(TEnum);

            try
            {
                if (String.IsNullOrEmpty(item) == false)
                    returnValue = (TEnum)Enum.Parse(typeof(TEnum), item, true);
            }
            catch (ArgumentException)
            {
                returnValue = notFoundValue;
            }
            catch (OverflowException)
            {
                returnValue = notFoundValue;
            }

            return returnValue;
        }

        public static DateTime ToDateTime(this string item)
        {
            var returnValue = default(DateTime);            
            item = item.Trim();
            if (item.IsInteger() == true & item.Length == 8)
                item = item.Substring(0, 2) + "-" + item.Substring(2, 2) + "-" + item.Substring(4, 4);
            if ((!(String.IsNullOrEmpty(item))) & (item.Trim().Length >= 8))
                if (DateTime.TryParse(item, out DateTime convertDate))
                    returnValue = convertDate;

            return returnValue;
        }

        public static Uri ToUri(this string item)
        {
            var returnValue = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute);

            if (String.IsNullOrEmpty(item) == false)
            try
                {
                    returnValue = new Uri(item);
                }
                catch { returnValue = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute); }

            return returnValue;
        }

        public static string SubstringRight(this string item, int rightCharacters)
        {
            return item.SubstringSafe(item.Length - rightCharacters);
        }

        public static string SubstringLeft(this string item, int leftCharacters)
        {
            return item.SubstringSafe(0, leftCharacters);
        }

        public static string SubstringSafe(this string item, int starting, int length = -1)
        {
            var itemLength = item.Length;

            if (length == -1) length = itemLength - starting;
            string returnValue;
            if (itemLength > length - (starting + 1))
                returnValue = length > -1 ? item.Substring(starting, length) : item.Substring(starting);
            else
                returnValue = itemLength == length - (starting + 1) ? item.Substring(starting, length - 1) : item;

            return returnValue;
        }

        public static List<string> Split(this string item, char separator = ',')
        {
            var returnValue = new List<string>();
            if (!string.IsNullOrWhiteSpace(item))
            returnValue = item.TrimEnd(separator).Split(separator).AsEnumerable<string>().Select(s => s.Trim()).ToList();
            return returnValue;
        }
    }
}