using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CommonFunction
{
    // Json Parser String Trimmer for Webservice
    public static string StringTrimmerWS(string Input)
    {
        try
        {
            return Input.Replace("\"", "").Trim();
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return Input;
        }
    }
}

public static class NumberUtility
{
    #region Currency Conversion

    public static string GetIndianStyledNumbers(int a_NumberToConvert)
    {
        //9,99,999
        return a_NumberToConvert.ToString("0,00,000", System.Globalization.CultureInfo.GetCultureInfo("hi-IN"));
    }

    // 10000 to 10,000
    public static string NumberFormatWithComa(long Balance)
    {
        try
        {
            string a = Balance.ToString("N0");
            return a;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    // 10000 to 10k
    public static string NumberConverter(long Money, int round, int min, string NumberySign)
    {
        string cd = NumberySign + Money;
        try
        {
            double X = Money;
            if (Money > min)
            {
                string[] DefaultSign = { "", "K", "M", "B", "T" };

                int i = 0;
                while (X >= 1000)
                {
                    i++;
                    X /= 1000;
                }
                X = System.Math.Round(X, round);
                cd = NumberySign + X + DefaultSign[i];
            }
            else
                cd = NumberySign + NumberFormatWithComa((long)X);
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return cd;
        }
    }

    #endregion
}

public static class StringUtility
{
    #region String Util

    // "123#" to "123#456#" using stringbuilder parameter
    public static string StringConcateByPattern(StringBuilder sb, string AppendString, bool usePattern = false, string pattern = "##")
    {
        try
        {
            sb.Append(AppendString);
            if (usePattern)
            {
                sb.Append(pattern);
            }
            return sb.ToString();
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return sb.ToString();
        }
    }

    // "123#" to "123#456#" using string parameter
    public static string StringConcateByPattern(String s, string AppendString, bool usePattern = false, string pattern = "##")
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            return StringConcateByPattern(sb, s, usePattern, pattern);
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return s;
        }
    }

    // "AnkurPatel" to "AnkurP..."
    public static string StringTrimmer(string s, int length, bool UseDots = false, int numberofdots = 3)
    {
        string cd = s;
        try
        {
            if (s != null && s != "")
            {
                if (s.Length > length)
                {
                    if (UseDots && numberofdots > 0)
                    {
                        string dots = ".";
                        if (numberofdots > 1)
                        {
                            for (int i = 0; i < numberofdots; i++)
                                dots += ".";
                        }
                        cd = s.Substring(0, length - 1) + dots;
                    }
                    else
                    {
                        cd = s.Substring(0, length - 1);
                    }
                }
            }
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return cd;
        }
    }

    //1##2##3 to array which includes 1,2,3
    public static string[] StringToArray(string input, string pattern, bool DuplicatesAllow = true)
    {
        string[] cd = new string[1];
        try
        {
            if (input.Contains(pattern))
            {
                cd = Regex.Split(input, pattern);
                if (!DuplicatesAllow)
                {
                    cd = cd.Where(x => !string.IsNullOrEmpty(x)).ToArray<string>();
                }
                return cd;
            }
            else
            {
                cd[0] = input;
                return cd;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            cd[0] = input;
            return cd;
        }
    }

    // array of 1,2,3 to 1##2##3
    public static string ConvertStringArrayToStringJoin(string[] array, string Pattern)
    {
        string result = "";
        try
        {
            result = string.Join(Pattern, array);
            return result;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return result;
        }
    }

    // AnkurPatel contains patel return true (case doesn't matter)
    public static bool IsContainsWithOutCase(string From, string ToBeCompared)
    {
        try
        {
            bool contains = From.IndexOf(ToBeCompared, StringComparison.OrdinalIgnoreCase) >= 0;
            return contains;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return false;
        }
    }

    //1#2#3# to list of string includes 1,2,3
    public static List<string> StringToListofString(string input, string pattern)
    {
        try
        {
            string[] stringArray = Regex.Split(input, pattern);
            return ArrayUtility.ArrayToList<string>(stringArray);
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    //1#2#3# to list of int includes 1,2,3
    public static List<int> StringToListofInt(string input, string pattern)
    {
        List<int> cd = new List<int>();
        try
        {
            string[] stringArray = Regex.Split(input, pattern);

            for (int i = 0; i < stringArray.Length; i++)
            {
                cd.Add(int.Parse(stringArray[i]));
            }
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return cd;
        }
    }

    //this method is 50 times faster than int.Parse
    //reference - http://www.dotnetperls.com/int-parse-optimization
    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            result = 10 * result + (value[i] - 48);
        }
        return result;
    }

    #endregion
}

public static class ArrayUtility
{
    #region Array Util

    // 1,2,3,2,4 to string array of 1,2,3,4
    public static T[] RemoveDuplicateEntryFromArray<T>(this T[] array)
    {
        try
        {
            T[] cd = array.Distinct().ToArray<T>();
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    //Add Item to an array at end
    public static T[] AddItemToArray<T>(this T[] original, T itemToAdd)
    {
        try
        {
            T[] finalArray = new T[original.Length + 1];
            for (int i = 0; i < original.Length; i++)
            {
                finalArray[i] = original[i];
            }

            finalArray[finalArray.Length - 1] = itemToAdd;

            return finalArray;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    // for 2d array check if both numbers are in range ([4,5] then a, b should be less than 4 and 5 respectively)
    public static bool IsIndexInArrayRange<T>(T[,] original, int a, int b)
    {
        try
        {
            if ((a >= 0 && b >= 0) && (a < original.GetLength(0) && b < original.GetLength(1)))
            {
                return true;
            }
            else
                return false;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return false;
        }
    }

    //Convert array to text. Output: count:2 0: abc   1: def
    public static string ToText<T>(this T[] original)
    {
        try
        {
            string l_text = string.Empty;
            if (original != null)
            {
                l_text += "Count :" + original.Length + "\n";
            }
            for (int i = 0; i < original.Length; i++)
            {
                l_text += i + ":" + original[i].ToString();

                if (i != 0 && i % 3 == 0)
                    l_text += "\n";
                else
                    l_text += "\t";
            }
            return l_text;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return "";
        }
    }

    //Array of Any Type to List of that Type
    public static List<T> ArrayToList<T>(T[] Array)
    {
        try
        {
            if (Array != null)
            {
                List<T> NewList = new List<T>(Array);
                return NewList;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    #endregion
}

public static class ListUtility
{
    #region List Util

    // 1,2,3,2,4 to List of String of 1,2,3,4
    public static List<T> RemoveDuplicateEntryFromList<T>(List<T> m_List)
    {
        try
        {
            m_List = m_List.Distinct().ToList();
            return m_List;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    // Create Copy of List
    public static List<T> CloneOfList<T>(List<T> Master)
    {
        try
        {
            List<T> Peer = new List<T>();
            for (int i = 0; i < Master.Count; i++)
            {
                Peer.Add(Master[i]);
            }
            return Peer;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    // Add element at the end of list
    public static List<T> AddElement<T>(this List<T> list, T item)
    {
        try
        {
            list.Add(item);
            return list;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    //Add List to end of another List
    public static List<T> AddElementRange<T>(this List<T> list, IEnumerable<T> items)
    {
        try
        {
            list.AddRange(items);
            return list;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    #endregion
}

public static class EmailPasswordUtility
{
    #region Email Password Util

    //Check Whether string is Email or not
    public static bool IsEmail(string email)
    {
        try
        {
            string MatchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (email != null)
            {
                return Regex.IsMatch(email, MatchEmailPattern);
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return false;
        }
    }

    //Check password validation by (8 to 15 Char,one upper,one lower,one number,one special char)
    public static bool PasswordValidation(string Password)
    {
        try
        {
            string matchpattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            if (Password != null)
            {
                return Regex.IsMatch(Password, matchpattern);
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return false;
        }
    }

    //Check password validation by Length Only
    public static bool PasswordValidation(string Password, int minimumlength = 6)
    {
        try
        {
            if (Password.Length < minimumlength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return false;
        }
    }

    #endregion
}

public static class HashTableUtility
{
    #region HashTable Util

    //Convert Hashtable to Dictionary
    public static Dictionary<K, V> HashtableToDictionary<K, V>(Hashtable table)
    {
        try
        {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            foreach (DictionaryEntry entry in table)
            {
                dict.Add((K)entry.Key, (V)entry.Value);
            }
            return dict;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    #endregion
}

public static class CUtility
{
    #region Util

    // Release Memory
    public static void ReleaseMemory()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    public static string RoundToOneDecimalPlace(float a_Value)
    {
        string a_returnVal = a_Value.ToString("0.0");
        if (a_returnVal.Contains(".0"))
        {
            return ((int)a_Value).ToString();
        }
        return a_returnVal;
    }

    public static string RoundToTwoDecimalPlaces(float a_Value)
    {
        string a_returnVal = a_Value.ToString("0.00");
        if (a_returnVal.Contains(".00"))
        {
            return a_Value.ToString();
        }
        return a_returnVal;
    }

    //Rounding float to particular points
    public static float RoundingTofloat(float a, int digits = 2)
    {
        return (float)Math.Round(a, digits);
    }

    #endregion
}

public static class EnumUtility
{
    #region Enum Util

    //Convert String to Enum (i.e. "cow","dog" to cow,dog enum)
    public static T StringToEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string EnumToString<T>(T enumvalue)
    {
        return enumvalue.ToString();
    }

    #endregion
}
