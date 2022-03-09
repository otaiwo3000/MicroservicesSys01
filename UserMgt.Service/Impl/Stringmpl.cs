

namespace UserMgt.Service.Impl
{
    public static class Stringmpl
    {
        public static string PartOfaString(string initialVal)
        {
            //string initialVal = "RE: BALANCE SHEET REPORT ISSUE";
            string transformedVal = initialVal.Trim().ToUpper();
            string newVal = "";

            //The index position of the string to remove
            int stringPosition = transformedVal.IndexOf("RE:");
            int stringPosition_2 = transformedVal.IndexOf("RE-");

            if (stringPosition != -1)
            {
                //Remove the string and return the rest
                newVal = transformedVal.Remove(stringPosition, 3);
            }

            else if (stringPosition_2 != -1)
            {
                //Remove the string and return the rest
                newVal = transformedVal.Remove(stringPosition_2, 3);
            }

            else //return it the way it was initially b4 conversion to upper case
                newVal = initialVal;

            newVal = newVal.Trim();
            return newVal;
        }
    }
}
