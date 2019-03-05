using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.LuaEngine
{
    public class ModelValidatorEngine : Script
    {
        /// <summary>
        /// Inject lua function validator:validate(model) to validate. The simple is <see cref="LuaValidatorValidate"/>.
        /// </summary>
        /// <param name="luaCheckRule">[LuaCode] ex. self:check('age', range(model.age, 18, 22))</param>
        public ModelValidatorEngine()
        {
            Globals["regex"] = (Func<object, string, string>)RegularExpression;
            Globals["stringLength"] = (Func<object, int, string>)StringLength;
            Globals["minLength"] = (Func<object, int, string>)MinLength;
            Globals["maxLength"] = (Func<object, int, string>)MaxLength;
            Globals["range"] = (Func<object, double, double, string>)Range;
            Globals["required"] = (Func<object, string>)Required;

            DoString(LuaValidator);
        }

        public string RegularExpression(object value, string pattern)
        {
            switch (value)
            {
                case string s:
                    var regex = new Regex(pattern, RegexOptions.Singleline);
                    if (regex.Match(s).Success) return null;
                    else goto default;

                default: return $"The field $key must match the regular expression '{pattern}'.";
            }
        }

        public string StringLength(object value, int length)
        {
            switch (value)
            {
                case string s:
                    if (s.Length <= length) return null;
                    else goto default;

                default: return $"The field $key must be a string with a maximum length of '{length}'.";
            }
        }

        public string MinLength(object value, int length)
        {
            switch (value)
            {
                case string s:
                    if (s.Length >= length) return null;
                    else goto default;

                case Array array:
                    if (array.Length >= length) return null;
                    else goto default;

                default: return $"The field $key must be a string or array type with a minimum length of '{length}'.";
            }
        }

        public string MaxLength(object value, int length)
        {
            switch (value)
            {
                case string s:
                    if (s.Length <= length) return null;
                    else goto default;

                case Array array:
                    if (array.Length <= length) return null;
                    else goto default;

                default: return $"The field $key must be a string or array type with a maximum length of '{length}'.";
            }
        }

        public string Range(object value, double min, double max)
        {
            switch (value)
            {
                case double d:
                    if (min <= d && d <= max) return null;
                    else goto default;

                default: return $"The field $key must be between {min} and {max}.";
            }
        }

        public string Required(object value)
        {
            if (value != null) return null;
            else return $"The $key field is required.";
        }

        public JSend ValidateForJsend(IDictionary<string, object> model)
        {
            var result = LValidate(model);
            if (result.Table.Pairs.Any())
                return JSend.Fail.Create(result.Table.ToDictionary());
            else return JSend.Success.Create();
        }

        public DynValue LValidate(IDictionary<string, object> model)
        {
            if (!(model is Dictionary<string, object>))
                return LValidate(new Dictionary<string, object>(model));

            var validator = Globals.Get("validator");
            Call(validator.Table.Get("clear"), validator);

            var validate = validator.Table.Get("validate");
            if (validate != null)
                Call(validator.Table.Get("validate"), validator, model);

            return validator.Table.Get("errors");
        }

        public static string LuaValidator = @"
validator =
{
  errors = { },
}

function validator:check(key, error)
  if error ~= nil then
    if self.errors[key] == nil then
      self.errors[key] = { }
    end
    table.insert(self.errors[key], (string.gsub(error, '$key', key)))
  end
end

function validator:clear()
  self.errors = { }
end

function validator:validate()
  --self:check('name', required(model.name))
end";

        public void LoadFunction_Dump() => DoString(LuaDump);
        public static string LuaDump = @"
function dump(o, leftPad)
  if leftPad == nil then leftPad = 0 end

  if type(o) == 'table' then
    local s = '{\n'
    for k,v in pairs(o) do
      if type(k) ~= 'number' then k = '""'..k..'""' end
      
      for i=1,leftPad do s = s.. ' ' end
     s = s.. '  ['..k..'] = ' .. dump(v, leftPad + 2) .. ',\n'
    end
    
    for i=1,leftPad do s = s.. ' ' end
    return s.. '}'
  else
    return tostring(o)
  end
end";

    }
}
