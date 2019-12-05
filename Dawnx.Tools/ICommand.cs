using Dawnx.Data;
using NEcho;

namespace Dawnx.Tools
{
    public interface ICommand
    {
        void Run(ConArgs cargs);
        void PrintUsage();
    }
}
