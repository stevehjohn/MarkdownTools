using CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace MarkdownTools.Console.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class Options
    {
        [Option('i', "InputFile", Required = true, HelpText = "Markdown file to parse.")]
        public string InputFile { get; set; }
        [Option('o', "OutputFile", Required = false, HelpText = "Output file name.")]
        public string OutputFile { get; set; }
        [Option('t', "Theme", Required = false, HelpText = "Theme to use. Dark, Light or Custom.")]
        public string Theme { get; set; }
        [Option('c', "CustomTheme", Required = false, HelpText = "Path to theme file is Custom specified.")]
        public string CustomTheme { get; set; }
    }
}