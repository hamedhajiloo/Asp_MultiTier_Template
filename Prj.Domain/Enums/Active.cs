using System.Runtime.Serialization;

namespace Prj.Domain.Enums
{
    /// <summary>
    /// وضعیت فعال یا غیر فعال
    /// </summary>
    public enum Active
    {
        /// <summary>
        /// نامشخص
        /// </summary>
        [EnumMember(Value = "نامشخص")]
        NotSet = 0,

        /// <summary>
        /// فعال
        /// </summary>
        [EnumMember(Value = "فعال")]
        Active = 1,

        /// <summary>
        /// غیر فعال
        /// </summary>
        [EnumMember(Value = "غیر فعال")]
        InActive = 2
    }
}
