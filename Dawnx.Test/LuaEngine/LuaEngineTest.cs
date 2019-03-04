using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.LuaEngine.Test
{
    public class LuaEngineTest
    {
        [Fact]
        public void Test()
        {
            var lua = new Script();
            lua.LCall()
        }

    }
}
