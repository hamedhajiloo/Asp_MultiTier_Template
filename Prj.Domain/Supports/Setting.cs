using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Prj.Domain.Supports
{
    /// <summary>
    /// تنظیمات
    /// </summary>
    [Table("Settings")]
    public class Setting
    {
        public byte Id { get; set; }

        /// <summary>
        /// کد یکتای نماینده
        /// </summary>
        public long AgentCode { get; set; } = 10000001;

        /// <summary>
        /// کد یکتای مشتری
        /// </summary>
        public long CustomerCode { get; set; } = 20000001;

        /// <summary>
        /// قوانین ثبت نام مشتری
        /// </summary>
        [AllowHtml]
        public string CustomerRegisterRules { get; set; }

        /// <summary>
        /// قوانین ثبت نام نماینده
        /// </summary>
        [AllowHtml]
        public string AgentRegisterRules { get; set; }

        /// <summary>
        /// درصد کارمزد مناقصه
        /// </summary>
        public float WagePercectTender { get; set; }

        /// <summary>
        /// حداکثر مبلغ کارمزد مناقصه
        /// </summary>
        public long MaxWageAmountTender { get; set; }

        /// <summary>
        /// مهلت صدور گواهی نامه به روز
        /// </summary>
        public int DeadlineCertificate { get; set; }

        /// <summary>
        /// حداکثر تعداد روز منقضی شدن مناقصه
        /// </summary>
        public int MaxDayTenderExpired { get; set; }

        /// <summary>
        /// حداقل تعداد روز منقضی شدن مناقصه
        /// </summary>
        public int MinDayTenderExpired { get; set; }

        /// <summary>
        /// هزینه برگذاری مناقصه
        /// </summary>
        public long CostTender { get; set; }

        /// <summary>
        /// حداقل مبلغ شارژ
        /// </summary>
        public long MinChargeValue { get; set; }

        /// <summary>
        /// حداکثر مبلغ شارژ
        /// </summary>
        public long MaxChargeValue { get; set; }

    }
}
