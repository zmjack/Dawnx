using Dawnx.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Tools
{
    public interface ICommand
    {
        void Run(ConsoleArgs cargs);
        void PrintUsage();
    }
}
