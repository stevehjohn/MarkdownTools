using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [ValidChildNodes]
    public class ImageEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Image;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("!["))
            {
                var line = source.GetLine();

                if (line.Length == 2)
                {
                    return null;
                }

                var end = line.IndexOf("](", StringComparison.Ordinal);

                if (end == -1)
                {
                    return null;
                }

                var altText = line.SafeSubstring(2, end - 2);

                var imageEnd = line.IndexOf(')', end + 2);

                if (imageEnd == -1)
                {
                    return null;
                }

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Image,
                        Content = null,
                        MetaData = new Dictionary<string, string>
                                   {
                                       { "Source", line.Substring(end + 2, imageEnd - end - 2) },
                                       { "AltText", altText }
                                   }
                    },
                    source.SafeSubstring(imageEnd + 1));
            }

            return null;
        }
    }
}