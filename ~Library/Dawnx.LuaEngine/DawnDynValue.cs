using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.LuaEngine
{
    public static class DawnDynValue
    {
        public static DynValue Get(this DynValue @this, string key) => @this.Table[key] as DynValue;

        public static DynValue Call(this DynValue @this, params object[] args)
        {
            if (new[] { DataType.Function, DataType.ClrFunction }.Contains(@this.Type))
                return @this.Function.Call(args);
            else throw new InvalidOperationException("The DynValue is not a function.");
        }

        public static Dictionary<string, object> ToDictionary(this DynValue @this)
        {
            if (@this.Type == DataType.Table)
                return Convert(@this) as Dictionary<string, object>;
            else return null;
        }

        private static object Convert(this DynValue @this)
        {
            switch (@this.Type)
            {
                case DataType.Boolean: return @this.Boolean;
                case DataType.Number: return @this.Number;
                case DataType.String: return @this.String;
                case DataType.Table:
                    var dict = new Dictionary<string, object>();
                    foreach (var pair in @this.Table.Pairs)
                        dict[pair.Key.ToString()] = Convert(pair.Value);
                    return dict;
                default: return @this.ToString();
            }
        }

    }
}
