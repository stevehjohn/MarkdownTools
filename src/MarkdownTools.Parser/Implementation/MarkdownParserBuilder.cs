using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkdownTools.Parser.Implementation
{
    public static class MarkdownParserBuilder
    {
        public static IMarkdownParser GetParserWithAllEvaluators()
        {
            var instances = new List<IEvaluator>();

            var evaluators = Assembly.GetAssembly(typeof(IEvaluator))
                                     .GetTypes()
                                     .Where(t => t.GetInterfaces().Contains(typeof(IEvaluator)) && !t.IsInterface)
                                     .ToList();

            evaluators.ForEach(e => instances.Add((IEvaluator) Activator.CreateInstance(e)));

            return new MarkdownParser(instances);
        }
    }
}