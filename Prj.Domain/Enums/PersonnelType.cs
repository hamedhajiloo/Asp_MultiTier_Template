using System.Runtime.Serialization;

namespace Prj.Domain.Enums
{
    /// <summary>
    /// نوع پرسنل
    /// </summary>
    public enum PersonnelType
    {
        /// <summary>
        /// نامشخص
        /// </summary>
        [EnumMember(Value = "نامشخص")]
        NotSet = 0,

        /// <summary>
        /// مدیر اصلی
        /// </summary>
        [EnumMember(Value = "مدیر اصلی")]
        Administrator = 1,

        /// <summary>
        /// مدیر داخلی
        /// </summary>
        [EnumMember(Value = "مدیر داخلی")]
        Manager = 2,

        /// <summary>
        /// مدیر مالی
        /// </summary>
        [EnumMember(Value = "مدیر مالی")]
        FinancialManager = 3,

        /// <summary>
        /// مدیر محتوا
        /// </summary>
        [EnumMember(Value = "مدیر محتوا")]
        ContactManager = 4
    }
}
