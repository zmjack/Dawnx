using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Analysises
{
    /// <summary>
    /// This class provides a method to decompose console arguments into <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public class ConsoleArgs : Dictionary<string, string>
    {
        private string[] Args;

        public ConsoleArgs(string[] args, params string[] keyStarts)
        {
            Args = args;

            string key = string.Empty;
            string value = string.Empty;

            foreach (var arg in args)
            {
                if (!keyStarts.Any(keyStart => arg.StartsWith(keyStart)))
                {
                    if (!key.IsNullOrEmpty())
                    {
                        this[key] = arg;
                        key = string.Empty;
                    }
                    else this[string.Empty] = arg;
                }
                else key = arg;
            }

            if (!key.IsNullOrEmpty())
                this[key] = string.Empty;
        }

        public new string this[string key]
        {
            get
            {
                if (ContainsKey(key))
                    return (this as Dictionary<string, string>)[key];
                else return null;
            }
            set => (this as Dictionary<string, string>)[key] = value;
        }

        public string SimpleLine => Args.Join(" ");

        public string Line => Args.Select(_ => $@"""{_}""").Join(" ");

    }
}
