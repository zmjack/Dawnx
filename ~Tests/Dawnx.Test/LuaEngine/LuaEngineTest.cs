using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.LuaEngine.Test
{
    public class LuaEngineTest
    {
        public static class LuaValidator
        {
            public static object RangeEx(object[] args)
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
        }

        [Fact]
        public void Test()
        {
            var lua = new Script();
            var validators = new Dictionary<string, Func<object[], object>>();
            validators.Add("RangeEx", LuaValidator.RangeEx);

            lua.Globals["validators"] = validators;

            var ss = lua.DoString(@"
function stringLength(...)
  local args = ...
  local value = args[1]
  local length = args[2]

  if string.len(value) <= length then
    return { isValid = true, message = '' }
  else
    return { isValid = false, message = 'The value of $key must be less than or equal to (' .. length .. ')' }
  end
end

validators['stringLength'] = stringLength
return validators['RangeEx']
");

            //lua.DoString(baseString);

            var model = new Dictionary<string, object>
            {
                ["name"] = "jack",
                ["age"] = 28,
            };
            var modelValidator = new Dictionary<string, object[][]>
            {
                ["name"] = new[]
                {
                    new object[] { "stringLength", 3 },
                },
                ["age"] = new[]
                {
                    new object[] { "rangeEx", 18, 25 },
                },
            };

            var ret = lua.LCall("validate", model, modelValidator).ToDictionary();
            var s = lua.LCall("dump", ret);
        }

        public string baseString = @"
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
end

function range(...)
  local args = ...
  local value = args[1]
  local min = args[2]
  local max = args[3]

  if min <= value and value <= max then
    return { isValid = true, message = '' }
  else 
    return { isValid = false, message = 'The value of $key must be in range(' .. min .. ',' .. max .. ')' }
  end
end

function stringLength(...)
  local args = ...
  local value = args[1]
  local length = args[2]

  if string.len(value) <= length then
    return { isValid = true, message = '' }
  else
    return { isValid = false, message = 'The value of $key must be less than or equal to (' .. length .. ')' }
  end
end

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

    }

}
