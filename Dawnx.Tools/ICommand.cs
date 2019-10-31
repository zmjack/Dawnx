using Dawnx.Data;

namespace Dawnx.Tools
{
    public interface ICommand
    {
        void Run(ConsoleArgs cargs);
        void PrintUsage();
    }
}
