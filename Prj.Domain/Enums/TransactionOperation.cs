using System.Runtime.Serialization;

namespace Prj.Domain.Enums
{
    /// <summary>
    /// نوع عملیات تراکنش 
    /// </summary>
    public enum TransactionOperation
    {
        /// <summary>
        /// نامشخص
        /// </summary>
        [EnumMember(Value = "نامشخص")]
        NotSet = 0,

        /// <summary>
        /// اضافه شده
        /// </summary>
        [EnumMember(Value = "اضافه شده")]
        Added = 1,

        /// <summary>
        /// کم شده
        /// </summary>
        [EnumMember(Value = "کم شده")]
        Decreased = 2,

        /// <summary>
        /// مسدود شده
        /// </summary>
        [EnumMember(Value = "مسدود شده")]
        Blocked
    }
}
