using Dawnx.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dawnx.AspNetCore.Reflection
{
    public class BasicTypeConverterForModel : DefaultBasicTypeConverter
    {
        public override object ConvertToDateTime(object source, ICustomAttributeProvider provider)
        {
            var value = source.ToString();
            var displayFormatAttrType = provider
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x is DisplayFormatAttribute);

            if (displayFormatAttrType != null)
            {
                var formatString = ((dynamic)displayFormatAttrType).DataFormatString.Replace("{0}", value);
                formatString = new Regex(@"\{0:(.+?)\}").Replace(formatString, "$1");

                return DateTime.ParseExact(value, formatString, null);
            }
            else return base.ConvertToDateTime(source, provider);
        }

    }
}
