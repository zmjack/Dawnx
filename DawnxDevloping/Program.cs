using Dawnx;
using Dawnx.Diagnostics;
using Dawnx.Sequences;
using Sapling;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DawnxDevloping
{
    class Program
    {
        public class Cls
        {
            [Display(Name = "AAA")]
            public int AA { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(LetterSequence.GetNumber("B"));
        }

    }
}
