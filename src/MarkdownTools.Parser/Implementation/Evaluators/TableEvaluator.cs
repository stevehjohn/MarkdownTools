using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System.Collections.Generic;
using System.Linq;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseChildren]
    [DoNotParseForParagraphs]
    public class TableEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Table;

        public EvaluatorResult Evaluate(string source)
        {
            var next = source.NextLine()?.GetLine();

            if (! string.IsNullOrWhiteSpace(next))
            {
                if (next.All(c => c == ':' || c == '-' || c == '|' || c == ' '))
                {
                    return TryParseTable(source);
                }
            }

            return null;
        }

        private static EvaluatorResult TryParseTable(string source)
        {
            var definitions = source.NextLine().GetLine().Split('|');
            var startIndex = 1;
            var endIndex = definitions.Length - 1;
            var columnAlignments = new List<string>();

            for (var i = 0; i < definitions.Length; i++)
            {
                var definition = definitions[i].Trim();

                if (definition == string.Empty && i == 0)
                {
                    columnAlignments.Add(null);
                    startIndex = 1;
                    continue;
                }
                if (definition == string.Empty && i == definitions.Length - 1)
                {
                    endIndex = definitions.Length - 2;
                    continue;
                }

                if (definition.StartsWith(":") && definition.EndsWith(":"))
                {
                    columnAlignments.Add("Center");
                    definition = definition.Substring(1, definition.Length - 2);
                }
                else if (definition.StartsWith(":"))
                {
                    columnAlignments.Add("Left");
                    definition = definition.Substring(1);
                }
                else if (definition.EndsWith(":"))
                {
                    columnAlignments.Add("Right");
                    definition = definition.Substring(0, definition.Length - 1);
                }
                else
                {
                    columnAlignments.Add(null);
                }

                if (definition.Length < 3 || definition.Any(c => c != '-'))
                {
                    return null;
                }
            }

            var headerStrings = source.GetLine().Split('|');

            if (headerStrings.Length != definitions.Length)
            {
                return null;
            }

            var headers = new List<Node>();
            var rows = new List<Node>();

            var bodyStart = source.NextLine().NextLine();

            for (var i = startIndex; i <= endIndex; i++)
            {
                var header = headerStrings[i];

                if (string.IsNullOrWhiteSpace(header))
                {
                    return null;
                }

                var node = new Node
                           {
                               Type = NodeType.TableCellHead,
                               Content = header.Trim(),
                               RawContent = header
                           };

                if (columnAlignments[i] != null)
                {
                    node.MetaData.Add("Alignment", columnAlignments[i]);
                }

                headers.Add(node);

                var lines = 0;

                if (string.IsNullOrWhiteSpace(bodyStart))
                {
                    continue;
                }

                var next = bodyStart;

                while (true)
                {
                    if (! string.IsNullOrWhiteSpace(bodyStart))
                    {
                        if (string.IsNullOrWhiteSpace(next))
                        {
                            break;
                        }
                        var line = next.GetLine();
                        next = next.NextLine();

                        var cols = line.Split('|');

                        if (cols.Length != definitions.Length)
                        {
                            break;
                        }

                        lines++;

                        Node row;

                        if (rows.Count < lines)
                        {
                            row = new Node
                                  {
                                      Type = NodeType.TableRow
                                  };
                            rows.Add(row);
                        }
                        else
                        {
                            row = rows[lines - 1];
                        }

                        var cell = new Node
                                   {
                                       Type = NodeType.TableCell,
                                       Content = cols[i].Trim(),
                                       RawContent = cols[i]
                                   };

                        if (columnAlignments[i] != null)
                        {
                            cell.MetaData.Add("Alignment", columnAlignments[i]);
                        }

                        row.Children.Add(cell);
                    }
                }
            }

            var table = new Node
                        {
                            Type = NodeType.Table
                        };

            table.Children.Add(new Node
                               {
                                   Type = NodeType.TableRow,
                                   Children = headers
                               });

            rows.ForEach(r => table.Children.Add(r));

            source = source.NextLine().NextLine();

            for (var i = 0; i < rows.Count; i++)
            {
                source = source.NextLine();
            }

            return new EvaluatorResult(
                table,
                source);
        }
    }
}