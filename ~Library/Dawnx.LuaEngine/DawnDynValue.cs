using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.LuaEngine
{
    public static class DawnDynValue
    {
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
                default: return null;
            }
        }

    }
}
