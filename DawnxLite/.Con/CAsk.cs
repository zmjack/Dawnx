﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Con
{
    public sealed class CAsk
    {
        public Cout Cout { get; }

        public string Question { get; }
        public ResolveDelegate Resolver { get; private set; }
        public delegate string ResolveDelegate(string answer);

        internal CAsk(Cout cout, string question, ResolveDelegate resolver)
        {
            Cout = cout;
            Question = question;
            Resolver = resolver;
        }

        public void Resolve()
        {
            while (true)
            {
                Cout.Write("? ", new ConColor { ForegroundColor = ConsoleColor.Green });
                Cout.Write($"{Question}: ");

                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                var answer = Console.ReadLine();
                var result = Resolver(answer);

                if (result is null) continue;

                Console.SetCursorPosition(left, top);
                Con.Out.Write(result, new ConColor { ForegroundColor = ConsoleColor.Cyan });
                if (result.GetLengthA() < answer.GetLengthA())
                    Console.Write(" ".Repeat(answer.GetLengthA() - result.GetLengthA()));
                Console.WriteLine();
                return;
            }
        }
    }

}