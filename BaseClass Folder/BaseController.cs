using QuotesApplivcation.LOGGING_TASK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotesApplivcation.BaseClass_Folder
{
    [Authorize]
    [LoggingTask]
    public class BaseController : Controller
    {
    }
}