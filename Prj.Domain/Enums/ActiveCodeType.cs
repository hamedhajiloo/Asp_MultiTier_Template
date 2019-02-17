using System.Runtime.Serialization;

namespace Prj.Domain.Enums
{
    /// <summary>
    /// نوع کد فعال ساز
    /// </summary>
    public enum ActiveCodeType
    {
        /// <summary>
        /// نامشخص
        /// </summary>
        [EnumMember(Value = "نامشخص")]
        NotSet = 0,

        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        [EnumMember(Value = "ثبت نام کاربر")]
        RegisterCustomer = 1,

        /// <summary>
        /// ثبت نام نماینده
        /// </summary>
        [EnumMember(Value = "ثبت نام نماینده")]
        RegisterAgent = 2,

        /// <summary>
        /// بازیابی کلمه عبور
        /// </summary>
        [EnumMember(Value = "بازیابی کلمه عبور")]
        ForgetPassword = 3
    }
}
