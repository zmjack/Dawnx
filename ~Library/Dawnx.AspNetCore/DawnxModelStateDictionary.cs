using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnxModelStateDictionary
    {
        public static Dictionary<string, string> ErrorMap(this ModelStateDictionary @this)
        {
            var errors = @this
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors[0].ErrorMessage))
                .ToDictionary(x => x.Key, x => x.Value);
            return errors;
        }
    }
}
