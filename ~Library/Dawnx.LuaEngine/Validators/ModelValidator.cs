using System;
using System.Collections.Generic;

namespace Dawnx.LuaEngine.Validators
{
    public class ModelValidator
    {
        //public bool Validate(Dictionary<string, object> model, Dictionary<string, object[][]> validators)
        //{
        //    foreach (var item in model)
        //    {
        //        if (validators[item.Key] != null)
        //        {
        //            foreach(var validator in validators[item.Key])
        //            {

        //            }
        //        }

        //    }

        //}

        /*
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
end
         */
    }
}
