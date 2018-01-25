using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deployer2._0.Helpers
{
    public static class ExtensionMethods
    {
        public static IHtmlString PreserveNewLines(this HtmlHelper htmlHelper, string message)
        {
            return message == null ? null : htmlHelper.Raw(htmlHelper.Encode(message).Replace("\n", "<br/>"));
        }
    }
}