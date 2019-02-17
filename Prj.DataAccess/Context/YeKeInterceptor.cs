using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace Prj.DataAccess.Context
{
    /// <summary>
    /// تبدیل خودکار حروف حروف ی و ک عربی به فارسی
    /// Refrence : https://www.dotnettips.info/post/1745/%db%8c%da%a9-%d8%af%d8%b3%d8%aa-%d8%b3%d8%a7%d8%b2%db%8c-%db%8c-%d9%88-%da%a9-%d8%af%d8%b1-%d8%a8%d8%b1%d9%86%d8%a7%d9%85%d9%87%e2%80%8c%d9%87%d8%a7%db%8c-entity-framework-6
    /// </summary>
    public class YeKeInterceptor : IDbCommandInterceptor
    {
        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }
    }

    public static class YeKe
    {
        public const char ArabicYeChar = (char)1610;
        public const char PersianYeChar = (char)1740;

        public const char ArabicKeChar = (char)1603;
        public const char PersianKeChar = (char)1705;

        public static string ApplyCorrectYeKe(this object data)
        {
            return data == null ? null : ApplyCorrectYeKe(data.ToString());
        }

        public static string ApplyCorrectYeKe(this string data)
        {
            return string.IsNullOrWhiteSpace(data) ?
                        string.Empty :
                        data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();
        }

        public static void ApplyCorrectYeKe(this DbCommand command)
        {
            command.CommandText = command.CommandText.ApplyCorrectYeKe();

            foreach (DbParameter parameter in command.Parameters)
            {
                switch (parameter.DbType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Xml:
                        parameter.Value = parameter.Value is DBNull ? parameter.Value : parameter.Value.ApplyCorrectYeKe();
                        break;
                }
            }
        }
    }
}
