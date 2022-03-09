using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UserMgt.Shared.Common.Utilities
{
    public static class StringExtraction
    {
        public static string ExtractASubstringBtwTwoXters(string parentstring, string firstxter, string secondxter)
        {
            // Get a substring after or before a character    
            //string mailaddress = "$Olajide Dada$ <olajide.dada@fintraksoftware.com>";
            string mailaddress = parentstring;

            //// Get a substring between two strings     
            //int firstStringPosition = mailaddress.IndexOf("<");
            //int secondStringPosition = mailaddress.IndexOf(">");
            int firstStringPosition = mailaddress.IndexOf(firstxter);
            int secondStringPosition = mailaddress.IndexOf(secondxter);

            string stringBetweenTwoCharacters = mailaddress.Substring(firstStringPosition + 1, mailaddress.Length - (firstStringPosition + 1));

            return stringBetweenTwoCharacters;
        }

        public static string ExtractSubstringsEachBtwTwoXters(string parentstrings, string firstxter, string secondxter)
        {
            parentstrings = parentstrings.Replace('"', ' ');
            //string[] stringArrayofEmailAddresses = Splitting.SplitString(parentstrings);
            char[] spearator = { ',', ';', '<', '>', '"', '\n' };
            string[] stringArrayofEmailAddresses = Splitting.SplitString(parentstrings, spearator);
            List<string> stringArrayofEmailAddresses_2 = stringArrayofEmailAddresses.Where(x => x.Contains("@")).ToList();
            List<string> stringArrayofEmailAddresses_3 = new List<string>();

            //string[] stringArrayEmailAddresses_2 = new string[] { };

            foreach (var v in stringArrayofEmailAddresses_2)
            {
                stringArrayofEmailAddresses_3.Add(ExtractASubstringBtwTwoXters(v, "<", ">"));
            }

            string stringAddresses = string.Join(", ", stringArrayofEmailAddresses_3);

            return stringAddresses;
        }

    }
}
