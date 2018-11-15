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
            //TODO: Use this method to generate js validation code.
            return new HtmlString("");
        }

    }
}
