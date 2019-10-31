using System;

namespace Dawnx.Tools
{
    public class CommandAttribute : Attribute
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public CommandAttribute(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }
    }
}
