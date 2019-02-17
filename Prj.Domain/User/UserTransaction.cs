using Prj.Domain.Enums;
using Prj.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Users
{
    /// <summary>
    /// تراکنش های کاربر
    /// </summary>
    [Table("User_Transactions")]
    public class UserTransaction
    {
        public long Id { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public string UserId { get; set; }
        public virtual User User { get; set; }


        /// <summary>
        /// نوع عملیات تراکنش
        /// </summary>
        public TransactionOperation TransactionOperation { get; set; }

        /// <summary>
        /// نوع تراکنش
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// مبلغ
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime InsertDate { get; set; }
    }
}
