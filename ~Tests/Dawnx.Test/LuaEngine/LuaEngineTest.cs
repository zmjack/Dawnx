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
            var lua = new ModelValidatorEngine();
            lua.LoadFunction_Dump();            
            lua.DoString(@"
function default(model)
  
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
end");

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
                    new object[] { "range", 18, 25 },
                },
            };



            var ret = lua.LValidate(model, modelValidator);
            var s = lua.Globals.Get("dump").Call(ret);

        }

    }

}
