using CommandLine;
using MarkdownTools.Console.Infrastructure;
using MarkdownTools.Parser.Implementation;
using MarkdownTools.TreeToHtml.Implementation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;

namespace MarkdownTools.Console
{
    [ExcludeFromCodeCoverage]
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            CommandLine.Parser.Default
                  .ParseArguments<Options>(args)
                  .WithParsed(Parse);
        }

        private static void Parse(Options options)
        {
            var markdown = File.ReadAllText(options.InputFile);

            var parser = MarkdownParserBuilder.GetParserWithAllEvaluators();

            var nodeTree = parser.Parse(markdown);

            var htmlBuilder = new NodeTreeWalker();

            htmlBuilder.LoadTree(nodeTree);

            var theme = Theme.Dark;

            if (! string.IsNullOrWhiteSpace(options.Theme))
            {
                theme = (Theme) Enum.Parse(typeof(Theme), options.Theme);
            }

            var html = htmlBuilder.ToHtml(theme, options.CustomTheme);

            string outputFile;

            if (! string.IsNullOrWhiteSpace(options.OutputFile))
            {
                outputFile = options.OutputFile;
            }
            else
            {
                outputFile = options.InputFile.EndsWith(".md", StringComparison.InvariantCultureIgnoreCase)
                    ? Regex.Replace(options.InputFile, ".md", ".html", RegexOptions.IgnoreCase)
                    : $"{options.InputFile}.html";
            }

            File.WriteAllText(outputFile, html);
        }
    }
}
