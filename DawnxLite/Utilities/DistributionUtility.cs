using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Utilities
{
    public static class DistributionUtility
    {
        public enum DistributionMethod { FormerPreferred }

        public static int[] Distribute(int amount, DistributionMethod method, out int out_rest, int[] quantities)
        {
            var rest = amount;
            var result = new int[quantities.Length];
            switch (method)
            {
                case DistributionMethod.FormerPreferred:
                    foreach (var vi in quantities.AsVI())
                    {
                        if (rest >= vi.Value)
                        {
                            result[vi.Index] = vi.Value;
                            rest -= vi.Value;
                            if (rest == 0) break;
                        }
                        else
                        {
                            result[vi.Index] = rest;
                            rest = 0;
                            break;
                        }
                    }
                    break;
            }

            out_rest = rest;
            return result;
        }

    }
}
