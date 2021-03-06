﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinqSharp;
using NStandard;
using System;
using System.Linq;

namespace Dawnx.AspNetCore
{
    public static class EnumUtility
    {
        public static SelectList GetSelectList<TEnum>()
            where TEnum : struct
        {
            var fields = typeof(TEnum).GetFields().Where(x => !x.IsSpecialName);
            return new SelectList(fields.Select(field => new
            {
                Value = field.Name,
                Text = DataAnnotationEx.GetDisplayName(field),
            }), "Value", "Text");
        }

        public static SelectList GetSelectList(Enum @enum)
        {
            var fields = @enum.GetType().GetFields().Where(x => !x.IsSpecialName);
            return new SelectList(fields.Select(field => new
            {
                Value = field.Name,
                Text = DataAnnotationEx.GetDisplayName(field),
            }), "Value", "Text", @enum.ToString());
        }

        public static HtmlString GetOptionTags(Enum @enum)
        {
            return new HtmlString(GetSelectList(@enum).Select(
                x => $@"<option value=""{x.Value}"" {(@enum.ToString() == x.Value ? "selected" : "")}>{x.Text}</option>")
                .Join(Environment.NewLine));
        }

    }
}
