using MoonSharp.Interpreter;
using System;
using System.Linq;

namespace Dawnx.LuaEngine
{
    public static class DawnDynValue
    {
        public static DynValue Call(this DynValue @this, params object[] args)
        {
            if (new[] { DataType.Function, DataType.ClrFunction }.Contains(@this.Type))
                return @this.Function.Call(args);
            else throw new InvalidOperationException("The DynValue is not a function.");
        }

    }
}
