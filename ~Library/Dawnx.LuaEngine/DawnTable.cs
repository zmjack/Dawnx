using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.LuaEngine
{
    public static class DawnTable
    {
        public static Dictionary<string, object> ToDictionary(this Table @this)
        {
            var dict = new Dictionary<string, object>();
            foreach (var pair in @this.Pairs)
                dict[pair.Key.String] = Convert(pair.Value);
            return dict;
        }

        private static object Convert(DynValue dynValue)
        {
            switch (dynValue.Type)
            {
                case DataType.Boolean: return dynValue.Boolean;
                case DataType.Number: return dynValue.Number;
                case DataType.String: return dynValue.String;
                case DataType.Table:
                    var pairs = dynValue.Table.Pairs;

                    // If the index list of lua table is an sequence, then returns an array. 
                    if (pairs.All(x => x.Key.Number != 0))
                    {
                        if (pairs.Min(x => x.Key.Number) == 1 && pairs.Max(x => x.Key.Number) == pairs.Count())
                            return dynValue.Table.Values.Select(x => Convert(x)).ToArray();
                    }

                    var dict = new Dictionary<string, object>();
                    foreach (var pair in dynValue.Table.Pairs)
                    {
                        switch (pair.Key.Type)
                        {
                            case DataType.String:
                                dict[pair.Key.String] = Convert(pair.Value);
                                break;

                            case DataType.Number:
                                dict[pair.Key.Number.ToString()] = Convert(pair.Value);
                                break;

                            default:
                                dict[pair.Key.ToString()] = Convert(pair.Value);
                                break;
                        }
                    }
                    return dict;
                default: return dynValue.ToString();
            }
        }

    }
}
