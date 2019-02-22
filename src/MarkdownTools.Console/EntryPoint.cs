using CommandLine;
using MarkdownTools.Console.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace MarkdownTools.Console
{
    [ExcludeFromCodeCoverage]
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            Parser.Default
                  .ParseArguments<Options>(args)
                  .WithParsed(o =>
                  {

                  });
        }
    }
}
