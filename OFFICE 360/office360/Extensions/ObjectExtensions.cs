using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace office360.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsValidGuid(string guid)
        {
            return Guid.TryParse(guid, out _);
        }
        public static string ToSafeString(this object input)
        {
            return (input ?? string.Empty).ToString();
        }
        public static string EnumToString(this object input)
        {
            return (input ?? string.Empty).ToString();
        }
        public static string ToGUIDString(this Guid? guid)
        {
            return (guid ?? Guid.Empty).ToString();
        }
        public static int ToInt(this object input)
        {
            int output = 0;
            if ((input == null) || (input == DBNull.Value))
                return output;
            try
            {
                if (string.Empty != input.ToString())
                    output = Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Conversion" + ex.Message);
            }

            return output;
        }
        public static void Main()
        {
            Guid myGuid = Guid.NewGuid(); // Generate a random GUID
            string guidString = myGuid.ToSafeString(); // Convert GUID to string
            Console.WriteLine("GUID as string: " + guidString);
        }
    }
}