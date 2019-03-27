using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dawnx.Analysises
{
    /// <summary>
    /// This class provides some methods to
    ///     separate the console arguments to content or property.
    /// </summary>
    public class ConsoleArgs
    {
        public string[] Args { get; private set; }
        public string[] Contents { get; private set; }
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public ConsoleArgs(string line, params string[] keyStarts)
        {
            var regex = new Regex(@" *(?:""(.+?)""(?: +|$)|([^ ]+)(?: +|$))*$");
            var match = regex.Match(line);
            var args = match.Groups.OfType<Group>()
                .Skip(1).Take(2)
                .SelectMany(x => x.Captures.OfType<Capture>())
                .OrderBy(x => x.Index)
                .Select(x => x.Value)
                .ToArray();

            Constructor(args, keyStarts);
        }
        public ConsoleArgs(string[] args, params string[] keyStarts) => Constructor(args, keyStarts);

        private void Constructor(string[] args, params string[] keyStarts)
        {
            Args = args;
            var contents = new List<string>();

            string key = string.Empty;
            string value = string.Empty;

            foreach (var arg in args)
            {
                if (!keyStarts.Any(x => arg.StartsWith(x)))
                {
                    if (!key.IsNullOrEmpty())
                    {
                        this[key] = arg;
                        key = string.Empty;
                    }
                    else contents.Add(arg);
                }
                else key = arg;
            }

            if (!key.IsNullOrEmpty())
                this[key] = string.Empty;

            Contents = contents.ToArray();
        }

        public string this[string key]
        {
            get
            {
                if (Properties.ContainsKey(key))
                    return (Properties as Dictionary<string, string>)[key];
                else return null;
            }
            set => (Properties as Dictionary<string, string>)[key] = value;
        }

    }
}
