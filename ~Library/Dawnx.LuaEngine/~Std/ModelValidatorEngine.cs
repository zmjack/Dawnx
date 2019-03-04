using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;

namespace Dawnx.LuaEngine
{
    public class ModelValidatorEngine : Script
    {
        public ModelValidatorEngine()
        {
            Globals["range"] = (Func<object[], Dictionary<string, object>>)Range;
            Globals["required"] = (Func<object[], Dictionary<string, object>>)Required;

            DoString(Lua_Validate);
        }

        public Dictionary<string, object> Range(object[] args)
        {
            var value = (double)args[0];
            var min = (double)args[1];
            var max = (double)args[2];

            if (min <= value && value <= max)
            {
                return new Dictionary<string, object>
                {
                    ["isValid"] = true,
                    ["message"] = "",
                };
            }
            else
            {
                return new Dictionary<string, object>
                {
                    ["isValid"] = false,
                    ["message"] = $"The value of $key must be in range({min},{max})",
                };
            }
        }

        public Dictionary<string, object> Required(object[] args)
        {
            var value = args[0];
            if (value != null)
            {
                return new Dictionary<string, object>
                {
                    ["isValid"] = true,
                    ["message"] = "",
                };
            }
            else
            {
                return new Dictionary<string, object>
                {
                    ["isValid"] = false,
                    ["message"] = $"The value of $key must not be null.",
                };
            }
        }

        public Dictionary<string, object> Validate(Dictionary<string, object> model, Dictionary<string, object[][]> modelValidator)
        {
            return LValidate(model, modelValidator).ToDictionary();
        }

        public DynValue LValidate(Dictionary<string, object> model, Dictionary<string, object[][]> modelValidator)
        {
            return Call(Globals["validate"], model, modelValidator);
        }

        public static string Lua_Validate = @"
function validate(model, modelValidator)
  local data = { }
  local count = 0
  
  for key,value in pairs(model) do
    if modelValidator[key] ~= nil then
      for i,v in pairs(modelValidator[key]) do
        local method = v[1]
        table.remove(v, 1)
        table.insert(v, 1, value)
        
        local check = _G[method](v)
        if not check.isValid then
          data[key] = string.gsub(string.gsub(check.message, '$key', key), '$value', value) 
          count = count + 1
        end
      end
    end   
  end
  
  return { count = count, data = data };
end";

        public void LoadFunction_Dump() => DoString(Lua_Dump);
        public static string Lua_Dump = @"
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
