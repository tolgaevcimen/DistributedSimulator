using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllNodes
{
    public static class NodeTypeGatherer
    {
        public static IEnumerable<string> GetEnumerableOfType<T>() where T : class
        {
            var objects = new List<string>();
            foreach (Type type in
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add(type.Name);
            }
            return objects;
        }

        public static List<string> AlgorithmTypes()
        {
            var algorithms = GetEnumerableOfType<_Node>();
            return algorithms.SelectMany(a => new List<string> {
                a + "_allIn",
                a + "_allWait",
                a + "_rand"
            }).ToList();
        }
    }
}
