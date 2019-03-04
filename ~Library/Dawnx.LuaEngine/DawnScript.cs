using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.LuaEngine
{
    public static class DawnScript
    {
        public static DynValue LCall(this Script @this, string function, params object[] args)
        {
            return @this.Call(@this.Globals[function], args);
        }

        public static DynValue LVar(this Script @this, string var)
        {
            return @this.Globals.Pairs.FirstOrDefault(a => a.Key.String == var).Value;
        }

        public static void Register<TClass>(this Script @this, string name = null)
            where TClass : class, new()
        {
            if (name is null) name = typeof(TClass).Name;

            UserData.RegisterType<TClass>();
            @this.Globals[name] = UserData.Create(new TClass());
        }

    }
}
