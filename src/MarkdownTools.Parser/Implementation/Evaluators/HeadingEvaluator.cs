using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Models;
using System;
using System.Collections.Generic;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class HeadingEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            if (source.SafeGetChar(0) == '#')
            {
                var level = 1;

                while (source.SafeGetChar(level) == '#' && level < 7)
                {
                    level++;
                }

                if (level < 7)
                {
                    var heading = new string('#', level);

                    var eol = source.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);

                    if (eol < 0)
                    {
                        if (source.EndsWith(heading))
                        {
                            source = source.SafeSubstring(0, source.Length - level);
                        }
                    }
                    else
                    {
                        var line = source.Substring(0, eol);

                        if (line.EndsWith(heading))
                        {
                            line = line.SafeSubstring(0, line.Length - level);

                            source = $"{line}{Environment.NewLine}{source.SafeSubstring(eol + Environment.NewLine.Length)}";
                        }
                    }

                    source = source.SafeSubstring(level);

                    return new EvaluatorResult(
                        new Node(NodeType.Heading, new Dictionary<string, string>
                                                   {
                                                       { "Level", $"{level}" }
                                                   }),
                        source);
                }
            }

            return null;
        }
    }
}