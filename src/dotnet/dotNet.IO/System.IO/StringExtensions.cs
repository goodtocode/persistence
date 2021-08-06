using System;
using System.IO;
using System.Text;

namespace GoodToCode.Shared.System
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Converts string to Stream
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted value if success. 
        /// Failure returns empty stream</returns>
        public static Stream ToStream(this string item)
        {
            var returnValue = new MemoryStream();

            if (String.IsNullOrEmpty(item) == false)
            {
                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(item);
                    returnValue = new MemoryStream(bytes);
                }
                catch
                {
                    returnValue = new MemoryStream();
                }
            }

            return returnValue;
        }
    }
}