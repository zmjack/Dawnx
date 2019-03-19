#if !USE
using System.Linq;
using System.Collections.Generic;
using System;
using Dawnx.Diagnostics;

namespace DawnxDevloping
{
    class Program
    {
        public class DiffieHellmanQpPair
        {
            public int Q { get; set; }
            public int P { get; set; }

            public static DiffieHellmanQpPair NewPair()
            {
                // q must be less than p
                return new DiffieHellmanQpPair
                {
                    Q = 7,
                    P = 11,
                };
            }
        }

        public class DiffieHellman
        {
            public int Rnd { get; set; }
            public DiffieHellmanQpPair DiffieHellmanQpPair { get; }

            public DiffieHellman(DiffieHellmanQpPair pair)
            {
                DiffieHellmanQpPair = pair;
            }

            public int RndResult => (int)Math.Pow(DiffieHellmanQpPair.Q, Rnd) % DiffieHellmanQpPair.P;
            public int Key(int targetRndResult) => (int)Math.Pow(targetRndResult, Rnd) % DiffieHellmanQpPair.P;
        }

        public class DiffieHellmanCracker
        {
            public int Q { get; set; }
            public int P { get; set; }
            public int RndResult1 { get; set; }
            public int RndResult2 { get; set; }

            public (List<int> a, List<int> b) Crack()
            {
                var cadicate1 = new List<int>();
                var cadicate2 = new List<int>();
                var tryResults = new List<int>();

                using (var probe = PerformanceProbe.Create())
                {
                    for (int iRnd = 2; iRnd <= int.MaxValue; iRnd++)
                    {
                        var tryResult = (int)Math.Pow(Q, iRnd) % P;
                        if (!tryResults.Contains(tryResult))
                            tryResults.Add(tryResult);
                        else break;

                        if (RndResult1 == tryResult)
                            cadicate1.Add(iRnd);
                        if (RndResult2 == tryResult)
                            cadicate2.Add(iRnd);
                    }
                }

                return (cadicate1, cadicate2);
            }
        }

        static void Main(string[] args)
        {
            var qp = DiffieHellmanQpPair.NewPair();
            var dh1 = new DiffieHellman(qp);
            var dh2 = new DiffieHellman(qp);

            dh1.Rnd = 3;
            dh2.Rnd = 5;

            Console.WriteLine(dh1.Key(dh2.RndResult));
            Console.WriteLine(dh2.Key(dh1.RndResult));

            var craker = new DiffieHellmanCracker()
            {
                Q = qp.Q,
                P = qp.P,
                RndResult1 = dh1.RndResult,
                RndResult2 = dh2.RndResult,
            };
            var s = craker.Crack();
            Console.WriteLine(s.b.First());


            Console.WriteLine();
            var dh = new DiffieHellman(qp);
            for (int i = 0; i < qp.P * 4 - 1; i++)
            {
                dh.Rnd = i;
                Console.WriteLine($"{qp.Q}^{i} mod {qp.P} = {Math.Pow(qp.Q, i) % qp.P}");
            }
        }

    }
}
#endif
