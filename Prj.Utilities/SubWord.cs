using System;
using System.Text.RegularExpressions;

namespace Prj.Utilities
{
    public static class SubWord
    {
        public static int CountWords(this string test)
        {
            int count = 0;
            bool wasInWord = false;
            bool inWord = false;

            for (int i = 0; i < test.Length; i++)
            {
                if (inWord)
                {
                    wasInWord = true;
                }

                if (Char.IsWhiteSpace(test[i]))
                {
                    if (wasInWord)
                    {
                        count++;
                        wasInWord = false;
                    }
                    inWord = false;
                }
                else
                {
                    inWord = true;
                }
            }
            // Check to see if we got out with seeing a word
            if (wasInWord)
            {
                count++;
            }

            return count;
        }

        public static string[] SubOfWords(this string strInput)
        {
            //حذف اسپیس های اضافی
            string newStrstr = Regex.Replace(strInput, " {2,}", " ");

            char[] commaSeparator = new char[] { ' ' };
            //استفاه از تابع برای شکستن متن
            string[] authors = strInput.Split(commaSeparator);
            //پیمایش آرایه برای نشان دادن در خروجی
            return authors;
        }
    }
}
