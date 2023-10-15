using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QuotesApplivcation.LOGGING_TASK
{
    public class LoggingTask : ActionFilterAttribute
    {
        //
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // creating the message.
            string msg = string.Format("{0}/{1} is executing...", 
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
            //calling the Log method for log activities.
            Logger logger = Logger.GetLogger();
            logger.Log(msg);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // creating the message.
            string msg = string.Format("{0}/{1} is executed...",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
            //calling the Log method for log activities.
            Logger logger = Logger.GetLogger();
            logger.Log(msg);
        }

        //
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // creating the message.
            string msg = string.Format("{0} UI is executed...", filterContext.Controller.ToString());

            //calling the Log method for log activities.
            Logger logger = Logger.GetLogger();
            logger.Log(msg);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // creating the message.
            string msg = string.Format("{0} UI is executed...",filterContext.Controller.ToString());

            //calling the Log method for log activities.
            Logger logger = Logger.GetLogger();
            logger.Log(msg);
        }

    }
}