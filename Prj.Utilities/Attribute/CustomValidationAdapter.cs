using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prj.Utilities.Attributes
{
    public class CustomRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public CustomRequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (attribute.ErrorMessage == null)
            {
                attribute.ErrorMessage = "{0} را وارد نمایید";
            }
        }
    }

    public class CustomStringLengthAttributeAdapter : StringLengthAttributeAdapter
    {
        public CustomStringLengthAttributeAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (attribute.ErrorMessage == null)
            {
                var message = "";
                if (attribute.MinimumLength != 0)
                    message += " باید حداقل {2} حرف";
                if (attribute.MaximumLength != int.MaxValue)
                    message += (message == "" ? "باید حداکثر " : " و حداکثر ") + " {1} حرف";
                message += " باشد";
                attribute.ErrorMessage = "{0} " + message;
            }
        }
    }

    public class CustomRangeAttributeAdapter : RangeAttributeAdapter
    {
        public CustomRangeAttributeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (attribute.ErrorMessage == null)
            {
                var message = "";
                if ((int)attribute.Minimum != 0)
                    message += " باید حداقل {2} حرف";
                if ((int)attribute.Maximum != int.MaxValue)
                    message += (message == "" ? "باید حداکثر " : " و حداکثر ") + " {1} حرف";
                message += " باشد";
                attribute.ErrorMessage = "{0} " + message;
            }
        }
    }
}
