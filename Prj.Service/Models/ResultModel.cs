using Prj.Domain.Enums;
using System.Collections.Generic;

namespace Prj.Services.Models
{
    public class ResultModel
    {
        public ResultModel(bool succeeded, List<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
        
        public bool Succeeded { get; set; }

        public List<string> Errors { get; set; }
    }

    public class ActionResultModel
    {
        public ActionResultModel()
        {
        }

        /// <summary>
        /// عملیات ناموفق
        /// </summary>
        /// <param name="message">پیام</param>
        public ActionResultModel(string message)
        {
            Message = message;
            StatusCode = ActionStatusCode.UnSuccessful;
        }

        /// <summary>
        /// عملیات موفق
        /// </summary>
        /// <param name="message">پیام</param>
        /// <param name="id">شناسه</param>
        public ActionResultModel(string message, long? id)
        {
            Message = message;
            Id = id;
            StatusCode = ActionStatusCode.Successful;
        }

        public ActionStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public long? Id { get; set; }
        public dynamic Data { get; set; }
    }
}
