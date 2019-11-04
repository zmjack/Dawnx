using NStandard;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dawnx.LuaEngine.Test
{
    public class LuaEngineTest
    {
        [Fact]
        public void Test()
        {
            var lua = new ModelValidatorEngine();
            lua.DoString(@"
function validator:validate(model)
  self:check('name', required(model.name))
  self:check('name', stringLength(model.name, 3))
  self:check('age', stringLength(model.age, 3))
  self:check('age', range(model.age, 18, 22))
  self:check('age', range(model.age, 18, 22))

  if model.name == 'jack' then
    if model.aeg != 28 then
      self:check('age', 'Jack\'s name must be 28.')
    end
  end
end");

            var model = new Dictionary<string, object>
            {
                ["name"] = "jack",
                ["age"] = 27,
            };
            var jsend = lua.ValidateForJsend(model);

            var model2 = new
            {
                name = "jack",
                age = 27,
            };
            var jsend2 = lua.ValidateForJsend(model2.ToExpandoObject());
        }

    }

}
