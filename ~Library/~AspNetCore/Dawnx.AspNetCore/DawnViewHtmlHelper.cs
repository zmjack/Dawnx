using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnViewHtmlHelper
    {
        public static HtmlString JValidate<TModel>(this HtmlHelper @this, VI<TModel> model)
        {
            return new HtmlString("");
        }

    }
}
