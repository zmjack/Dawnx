using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dawnx
{
    public class OnePassScanerBuilder
    {
        public Dictionary<string, Tuple<string, string>> TagDefinitions;

        public OnePassScanerBuilder WithTag(string name, Tuple<string, string> definition)
        {
            if (!TagDefinitions.ContainsKey(name))
            {
                TagDefinitions.Add(name, definition);
                return this;
            }
            else throw new ArgumentException("The definition is exist.");
        }
        public OnePassScanerBuilder WithDefault(Tuple<string, string> definition) => WithTag(null, definition);

        public OnePassScaner Build() => new OnePassScaner(this);

        public virtual string Analysis(string name, string startTag, string endTag, string content)
        {
            return $"{startTag}{content}{endTag}";
        }
    }

    public class OnePassScaner
    {
        public OnePassScanerBuilder Builder { get; private set; }
        public string[] PatternNames { get; private set; }
        public string[] StartPatterns { get; private set; }
        public string[] EndPatterns { get; private set; }

        public OnePassScaner(OnePassScanerBuilder builder)
        {
            Builder = builder;
            PatternNames = builder.TagDefinitions.Keys.ToArray();
            StartPatterns = builder.TagDefinitions.Values.Select(x => x.Item1).ToArray();
            EndPatterns = builder.TagDefinitions.Values.Select(x => x.Item2).ToArray();
        }

        public string Scan(string input)
        {
            foreach (var startPattern in StartPatterns.AsVI())
            {
                var startMatch = new Regex($"^{startPattern.Value}", RegexOptions.Singleline).Match(input);
                if (startMatch.Success)
                {
                    var name = PatternNames[startPattern.Index];
                    var endPattern = EndPatterns[startPattern.Index];
                    var endMatch = new Regex(endPattern, RegexOptions.Singleline).Match(input);
                    if (endMatch.Success)
                    {
                        var content = input.Slice(startMatch.Index + startMatch.Length, endMatch.Index);
                        Builder.Analysis(name, startMatch.Groups[1].Value, endMatch.Groups[1].Value, content);
                    }
                }
            }
        }

    }
}
