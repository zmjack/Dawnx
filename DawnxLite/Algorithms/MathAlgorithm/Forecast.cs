using NStandard;
using System;
using System.Collections;
using System.Linq;

namespace Dawnx.Algorithms.MathAlgorithm
{
    public static class Forecast
    {
        public static double Linear(double[] known_x, double[] known_y, double forecast_x)
        {
            if (known_y.Length != known_x.Length)
                throw new ArgumentException($"The `{nameof(known_x)}`'s length must be equal to the `{nameof(known_y)}`'s length.");

            var avg_x = known_x.Average();
            var avg_y = known_y.Average();

            var seq_x = known_x.GetEnumerator();

            var bounds = known_y
                .Aggregate(new { Top = 0.0, Bottom = 0.0 }, (acc, y) =>
                {
                    var x = (double)seq_x.TakeElement();
                    return new
                    {
                        Top = acc.Top + (x - avg_x) * (y - avg_y),
                        Bottom = acc.Bottom + Math.Pow(x - avg_x, 2.0)
                    };
                });

            var level = bounds.Top / bounds.Bottom;

            return (avg_y - level * avg_x) + level * forecast_x;
        }
    }
}
