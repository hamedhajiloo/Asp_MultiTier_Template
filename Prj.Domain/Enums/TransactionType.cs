using System.Runtime.Serialization;

namespace Prj.Domain.Enums
{
    /// <summary>
    /// نوع تراکنش
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// نامشخص
        /// </summary>
        [EnumMember(Value = "نامشخص")]
        NotSet = 0,

        /// <summary>
        /// --ثبت در مناقصه--
        /// </summary>
        [EnumMember(Value = "ثبت مناقصه")]
        TenderRegister = 1,

        /// <summary>
        /// --شرکت در مناقصه--
        /// </summary>
        [EnumMember(Value = "شرکت در مناقصه")]
        TenderBidderBlock = 2,

        /// <summary>
        /// ++رفع مسدودی مناقصه++
        /// </summary>
        [EnumMember(Value = "رفع مسدودی مناقصه")]
        TenderBidderReturn = 3,

        /// <summary>
        /// --برنده شدن مناقصه--
        /// </summary>
        [EnumMember(Value = "برنده شدن مناقصه")]
        TenderBidderSuccess = 4,

        /// <summary>
        /// ++سفارش شارژ کیف پول++
        /// </summary>
        [EnumMember(Value = "سفارش شارژ کیف پول")]
        UserOrder = 5,

        /// <summary>
        /// ++هدیه دعوت شده++
        /// </summary>
        [EnumMember(Value = "هدیه دعوت شده")]
        UserInvited = 6,

        /// <summary>
        /// ++هدیه دعوت کننده++
        /// </summary>
        [EnumMember(Value = "هدیه دعوت کننده")]
        UserInviter = 7,
    }
}
