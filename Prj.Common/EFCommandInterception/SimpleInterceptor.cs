using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Common.EFCommandInterception
{
    public class SimpleInterceptor : DbCommandInterceptor
    {
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            var timespan = runCommand(() => base.ScalarExecuting(command, interceptionContext));
            logData(command, interceptionContext.Exception, timespan);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            var timespan = runCommand(() => base.NonQueryExecuting(command, interceptionContext));
            logData(command, interceptionContext.Exception, timespan);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            var timespan = runCommand(() => base.ReaderExecuting(command, interceptionContext));
            logData(command, interceptionContext.Exception, timespan);
        }

        private static Stopwatch runCommand(Action command)
        {
            var timespan = Stopwatch.StartNew();
            command();
            timespan.Stop();
            return timespan;
        }

        private static void logData(DbCommand command, Exception exception, Stopwatch timespan)
        {
            if (exception != null)
            {
                Trace.TraceError(formatException(exception, "Error executing command: {0}", command.CommandText));
            }
            else
            {
                Trace.TraceInformation(string.Concat("Elapsed time: ", timespan.Elapsed, " Command: ", command.CommandText));
            }
        }

        private static string formatException(Exception exception, string fmt, params object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            while (exception.InnerException != null)
            {
                sb.Append(" Inner exception: ");
                sb.Append(exception.InnerException.ToString());
                exception = exception.InnerException;
            }
            return sb.ToString();
        }
    }
}

