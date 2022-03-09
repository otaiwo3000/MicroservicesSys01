using System;
using System.Collections.Generic;
using System.Text;

namespace UserMgt.Shared.Common.Utilities
{
    public static class Splitting
    {
        public static String[] SplitStringCommaSeparator(string astring)
        {
            string[] stringArray = new string[] { };

            stringArray = astring.Split(",", StringSplitOptions.RemoveEmptyEntries);

            return stringArray;
        }

        public static String[] SplitStringSemiColonSeparator(string astring)
        {
            string[] stringArray = new string[] { };

            stringArray = astring.Split(";", StringSplitOptions.RemoveEmptyEntries);

            return stringArray;
        }

        public static String[] SplitString(string astring)
        {
            //int[] intArray = new int[] { 1,2,3 };
            //string[] test = new string[] { "2", "7" };

            string[] stringArray = new string[] { };
            char[] spearator = { ',', ';' };

            stringArray = astring.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            return stringArray;
        }

        public static String[] SplitString(string astring, char[] spearator)
        {
            string[] stringArray = new string[] {  };
            //char[] spearator = { ',', ';' };

            stringArray = astring.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            return stringArray;
        }

    }
}
