using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Prj.Utilities.Attributes
{
    public class NationalIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string strErrorMessage;
            if (value==null)
            {
                return ValidationResult.Success;
            }
            if (value.ToString().Trim().Length != 10)
            {
                strErrorMessage = "کد ملی را درست وارد نمایید";
                return new ValidationResult(strErrorMessage);
            }
            try
            {
                value = value.ToString().PadLeft(10, '0');

                if (!Regex.IsMatch(value.ToString(), @"^\d{10}$"))
                {
                    strErrorMessage = "کد ملی را درست وارد نمایید";
                    return new ValidationResult(strErrorMessage);
                }

                var check = Convert.ToInt32(value.ToString().Substring(9, 1));
                var sum = Enumerable.Range(0, 9)
                    .Select(x => Convert.ToInt32(value.ToString().Substring(x, 1)) * (10 - x))
                    .Sum() % 11;

                if( sum < 2 && check == sum || sum >= 2 && check + sum == 11)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    strErrorMessage = "کد ملی را درست وارد نمایید";
                    return new ValidationResult(strErrorMessage);
                }
            }
            catch (Exception)
            {
                strErrorMessage = "کد ملی را درست وارد نمایید";
                return new ValidationResult(strErrorMessage);
            }
                
        }
    }
}
