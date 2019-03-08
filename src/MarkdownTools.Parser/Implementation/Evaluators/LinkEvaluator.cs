using MarkdownTools.Models;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class LinkEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Link;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("["))
            {
                var line = source.GetLine();

                if (line.Length == 1)
                {
                    return null;
                }

                var end = line.IndexOf("](", StringComparison.Ordinal);

                if (end == -1)
                {
                    return null;
                }

                var content = line.SafeSubstring(1, end - 1);

                var linkEnd = line.IndexOf(')', end + 2);

                if (linkEnd == -1)
                {
                    return null;
                }

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Link,
                        Content = content,
                        MetaData = new Dictionary<string, string>
                                   {
                                       { "Link", line.Substring(end + 2, linkEnd - end - 2) }
                                   }
                    },
                    source.SafeSubstring(linkEnd + 1));
            }

            return null;
        }
    }
}