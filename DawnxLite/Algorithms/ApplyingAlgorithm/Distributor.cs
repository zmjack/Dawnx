using System;
using System.Linq;

namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public class Distributor
    {
        public DistributionMethod Method { get; }

        public Distributor(DistributionMethod method)
        {
            Method = method;
        }

        public int Distribute(int amount, int[] capacities, out int[] result)
        {
            var rest = amount;
            var _result = new int[capacities.Length];
            switch (Method)
            {
                case DistributionMethod.FormerPreferred:
                    foreach (var vi in capacities.AsVI())
                    {
                        if (rest >= vi.Value)
                        {
                            _result[vi.Index] = vi.Value;
                            rest -= vi.Value;
                            if (rest == 0) break;
                        }
                        else
                        {
                            _result[vi.Index] = rest;
                            rest = 0;
                            break;
                        }
                    }
                    break;
            }

            result = _result;
            return rest;
        }

        public int Distribute<TElement>(int amount, TElement[] elements, Func<TElement, int> capacityMethod, Action<TElement, int> gainAction = null)
        {
            var rest = Distribute(amount, elements.Select(x => capacityMethod(x)).ToArray(), out var result);
            foreach (var vi in elements.AsVI())
                gainAction(vi.Value, result[vi.Index]);
            return rest;
        }
    }

}
