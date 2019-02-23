using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Prj.Web.Attributes
{
    /// <summary>
    /// بررسی صحت کد ملی
    /// </summary>
    public class ValidNationalCodeAttribute : ValidationAttribute
    {
        /// <summary>
        /// </summary>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ////در صورتی که کد ملی وارد شده تهی باشد

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("لطفا کد ملی را صحیح وارد نمایید");
            }


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (value.ToString().Length != 10)
            {
                return new ValidationResult("طول کد ملی باید ده کاراکتر باشد");
            }

            //در صورتی که کد ملی ده رقم عددی نباشد
            Regex regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(value.ToString()))
            {
                return new ValidationResult("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");
            }

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            string[] allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(value.ToString()))
            {
                return new ValidationResult("کد ملی وارد شده اشتباه است");
            }


            //عملیات شرح داده شده در بالا
            char[] chArray = value.ToString().ToCharArray();
            int num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            int num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            int num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            int num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            int num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            int num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            int num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            int num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            int num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            int a = Convert.ToInt32(chArray[9].ToString());

            int b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            int c = b % 11;

            if ((((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a))) == true)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("کد ملی وارد شده اشتباه است");
            }
        }
    }
}