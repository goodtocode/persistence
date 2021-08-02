﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoodToCode.Shared.System.System
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts Stream to string
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted value if success. 
        /// Failure returns string.Empty</returns>
        public static string ToString(this Stream item)
        {
            string returnValue;

            try
            {
                StreamReader reader = new StreamReader(item);
                returnValue = reader.ReadToEnd();
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }
    }
}
