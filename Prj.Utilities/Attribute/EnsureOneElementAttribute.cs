using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Prj.Utilities.Attribute
{
    /// <summary>
    /// ولیدیشن مخصوص لیست
    /// لیست نباید خالی یا نال باشد
    /// </summary>
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
