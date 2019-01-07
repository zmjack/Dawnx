#if !USE
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Dawnx.Algorithms.StringAlgorithm;

namespace DawnxDevloping
{
    class Program
    {

        static void Main(string[] args)
        {
            var mm = new MaxMatching(new[] { "计算", "计算语言学", "课程", "有", "意思" });
            var s = mm.GetWords("计算语言学课程有意思");
            Console.WriteLine(s.Join(","));

        }

    }
}
#endif
