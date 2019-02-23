using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseContent]
    [Precedence(2)]
    public class HeadingEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Heading;

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
                    source = source.SafeSubstring(level);

                    var heading = new string('#', level);

                    var eol = source.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);

                    string content;
                    string rawContent;

                    if (eol < 0)
                    {
                        if (source.EndsWith(heading))
                        {
                            source = source.SafeSubstring(0, source.Length - level);
                        }

                        rawContent = source;
                        content = rawContent.Trim();
                        source = null;
                    }
                    else
                    {
                        var line = source.Substring(0, eol);

                        if (line.EndsWith(heading))
                        {
                            line = line.SafeSubstring(0, line.Length - level);

                            source = $"{line}{Environment.NewLine}{source.SafeSubstring(eol + Environment.NewLine.Length)}";
                        }

                        eol = source.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);

                        rawContent = source.SafeSubstring(0, eol);
                        content = rawContent.Trim();
                        source = source.SafeSubstring(eol + Environment.NewLine.Length);
                    }


                    return new EvaluatorResult(
                        new Node
                        {
                            Type = NodeType.Heading,
                            MetaData = new Dictionary<string, string>
                                       {
                                           { "Level", $"{level}" }
                                       },
                            Content = content,
                            RawContent = rawContent
                        },
                        source);
                }
            }

            return null;
        }
    }
}